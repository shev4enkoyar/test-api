using AutoMapper;
using LTA.Application.Common.Attributes;
using LTA.Application.Common.Exceptions;
using LTA.Application.Common.Interfaces;
using LTA.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace LTA.Application.Albums.Queries.GetAlbumsByUserId;

[DbContentComplement(typeof(Album))]
public class GetAlbumsByUserIdQuery : IRequest<List<AlbumDto>>
{
    public GetAlbumsByUserIdQuery(int userId)
    {
        UserId = userId;
    }

    public int UserId { get; }
}

public class GetAlbumsByUserIdQueryHandler : IRequestHandler<GetAlbumsByUserIdQuery, List<AlbumDto>>
{
    private readonly IJsonPlaceholderApiClient _apiClient;
    private readonly IApplicationDbContext _dbContext;
    private readonly IMapper _mapper;

    public GetAlbumsByUserIdQueryHandler(IJsonPlaceholderApiClient apiClient, IApplicationDbContext dbContext,
        IMapper mapper)
    {
        _apiClient = apiClient;
        _dbContext = dbContext;
        _mapper = mapper;
    }

    public async Task<List<AlbumDto>> Handle(GetAlbumsByUserIdQuery request, CancellationToken cancellationToken)
    {
        var albums = await _dbContext.Albums.Where(x => x.UserId.Equals(request.UserId)).ToListAsync(cancellationToken);

        if (albums.Any())
            return _mapper.Map<List<AlbumDto>>(albums);

        var apiAlbums = await _apiClient.GetAlbumsByUserId(request.UserId);
        if (apiAlbums == null || !apiAlbums.Any())
            throw new NotFoundException();

        albums = _mapper.Map<List<Album>>(apiAlbums);
        await _dbContext.Albums.AddRangeAsync(albums, cancellationToken);

        await _dbContext.SaveChangesAsync(cancellationToken);

        return _mapper.Map<List<AlbumDto>>(albums);
    }
}