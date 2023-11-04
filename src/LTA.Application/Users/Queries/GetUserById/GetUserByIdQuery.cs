using AutoMapper;
using LTA.Application.Common.Attributes;
using LTA.Application.Common.Exceptions;
using LTA.Application.Common.Interfaces;
using LTA.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace LTA.Application.Users.Queries.GetUserById;

[DbContentComplement(typeof(User))]
public class GetUserByIdQuery : IRequest<UserDto>
{
    public GetUserByIdQuery(int userId)
    {
        UserId = userId;
    }

    public int UserId { get; }
}

public class GetUserByIdQueryHandler : IRequestHandler<GetUserByIdQuery, UserDto>
{
    private readonly IJsonPlaceholderApiClient _apiClient;
    private readonly IApplicationDbContext _dbContext;
    private readonly IMapper _mapper;

    public GetUserByIdQueryHandler(IApplicationDbContext dbContext, IJsonPlaceholderApiClient apiClient, IMapper mapper)
    {
        _dbContext = dbContext;
        _apiClient = apiClient;
        _mapper = mapper;
    }

    public async Task<UserDto> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
    {
        var user = await _dbContext.Users.FirstOrDefaultAsync(x => x.Id.Equals(request.UserId), cancellationToken);

        if (user != null)
            return _mapper.Map<UserDto>(user);

        var apiUser = await _apiClient.GetUserById(request.UserId);
        if (apiUser == null)
            throw new NotFoundException();

        user = _mapper.Map<User>(apiUser);

        await _dbContext.Users.AddAsync(user, cancellationToken);

        await _dbContext.SaveChangesAsync(cancellationToken);

        return _mapper.Map<UserDto>(user);
    }
}