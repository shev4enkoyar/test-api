using System.Reflection;
using AutoMapper;
using LTA.Application.Common.Attributes;
using LTA.Application.Common.Exceptions;
using LTA.Application.Common.Interfaces;
using LTA.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace LTA.Application.Common.Behaviours;

public class DatabaseContentComplementBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : notnull
{
    private readonly IJsonPlaceholderApiClient _apiClient;
    private readonly IApplicationDbContext _dbContext;
    private readonly ILoggerAdapter<TRequest> _logger;
    private readonly IMapper _mapper;

    public DatabaseContentComplementBehaviour(ILoggerAdapter<TRequest> logger, IApplicationDbContext dbContext,
        IMapper mapper, IJsonPlaceholderApiClient apiClient)
    {
        _logger = logger;
        _dbContext = dbContext;
        _mapper = mapper;
        _apiClient = apiClient;
    }

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken)
    {
        var complementAttributes = request.GetType().GetCustomAttributes<DbContentComplementAttribute>().ToArray();

        if (!complementAttributes.Any())
            return await next();

        var attribute = complementAttributes.FirstOrDefault();

        if (attribute?.DatabaseTableType == typeof(User) || attribute?.DatabaseTableType == typeof(Album))
            if (!await _dbContext.Users.AnyAsync(cancellationToken))
            {
                var jsonUsers = await _apiClient.GetUsers();

                if (jsonUsers == null)
                    throw new NotFoundException();

                await _dbContext.Users
                    .AddRangeAsync(_mapper.Map<List<User>>(jsonUsers), cancellationToken);
                await _dbContext.SaveChangesAsync(cancellationToken);
            }

        if (attribute?.DatabaseTableType == typeof(Album))
            if (!await _dbContext.Albums.AnyAsync(cancellationToken))
            {
                var jsonAlbums = await _apiClient.GetAlbums();

                if (jsonAlbums == null)
                    throw new NotFoundException();

                await _dbContext.Albums
                    .AddRangeAsync(_mapper.Map<List<Album>>(jsonAlbums), cancellationToken);
                await _dbContext.SaveChangesAsync(cancellationToken);
            }

        return await next();
    }
}