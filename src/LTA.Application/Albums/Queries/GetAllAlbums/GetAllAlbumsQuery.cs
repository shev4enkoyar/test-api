using AutoMapper;
using AutoMapper.QueryableExtensions;
using LTA.Application.Common.Attributes;
using LTA.Application.Common.Interfaces;
using LTA.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace LTA.Application.Albums.Queries.GetAllAlbums;

[DbContentComplement(typeof(Album))]
public class GetAllAlbumsQuery : IRequest<List<AlbumDto>>
{
}

public class GetAllAlbumsQueryHandler : IRequestHandler<GetAllAlbumsQuery, List<AlbumDto>>
{
    private readonly IApplicationDbContext _dbContext;
    private readonly IMapper _mapper;

    public GetAllAlbumsQueryHandler(IApplicationDbContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }

    public async Task<List<AlbumDto>> Handle(GetAllAlbumsQuery request, CancellationToken cancellationToken)
    {
        return await _dbContext.Albums
            .ProjectTo<AlbumDto>(_mapper.ConfigurationProvider)
            .ToListAsync(cancellationToken);
    }
}