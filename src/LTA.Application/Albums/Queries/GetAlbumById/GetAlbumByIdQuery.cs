using AutoMapper;
using LTA.Application.Common.Attributes;
using LTA.Application.Common.Exceptions;
using LTA.Application.Common.Interfaces;
using LTA.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace LTA.Application.Albums.Queries.GetAlbumById;

[DbContentComplement(typeof(Album))]
public class GetAlbumByIdQuery : IRequest<AlbumDto>
{
    public GetAlbumByIdQuery(int albumId)
    {
        AlbumId = albumId;
    }

    public int AlbumId { get; }
}

public class GetAlbumByIdQueryHandler : IRequestHandler<GetAlbumByIdQuery, AlbumDto>
{
    private readonly IJsonPlaceholderApiClient _apiClient;
    private readonly IApplicationDbContext _dbContext;
    private readonly IMapper _mapper;

    public GetAlbumByIdQueryHandler(IApplicationDbContext dbContext, IJsonPlaceholderApiClient apiClient,
        IMapper mapper)
    {
        _dbContext = dbContext;
        _apiClient = apiClient;
        _mapper = mapper;
    }

    public async Task<AlbumDto> Handle(GetAlbumByIdQuery request, CancellationToken cancellationToken)
    {
        var album = await _dbContext.Albums.FirstOrDefaultAsync(x => x.Id.Equals(request.AlbumId), cancellationToken);

        if (album != null)
            return _mapper.Map<AlbumDto>(album);

        var apiAlbum = await _apiClient.GetAlbumById(request.AlbumId);
        if (apiAlbum == null)
            throw new NotFoundException();

        album = _mapper.Map<Album>(apiAlbum);
        await _dbContext.Albums.AddAsync(album, cancellationToken);

        await _dbContext.SaveChangesAsync(cancellationToken);

        return _mapper.Map<AlbumDto>(album);
    }
}