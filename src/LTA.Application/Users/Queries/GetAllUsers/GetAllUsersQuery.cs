using AutoMapper;
using AutoMapper.QueryableExtensions;
using LTA.Application.Common.Attributes;
using LTA.Application.Common.Interfaces;
using LTA.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace LTA.Application.Users.Queries.GetAllUsers;

[DbContentComplement(typeof(User))]
public class GetAllUsersQuery : IRequest<List<UserDto>>
{
}

public class GetAllUsersQueryHandler : IRequestHandler<GetAllUsersQuery, List<UserDto>>
{
    private readonly IApplicationDbContext _dbContext;
    private readonly IMapper _mapper;

    public GetAllUsersQueryHandler(IApplicationDbContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }

    public async Task<List<UserDto>> Handle(GetAllUsersQuery request, CancellationToken cancellationToken)
    {
        return await _dbContext.Users
            .ProjectTo<UserDto>(_mapper.ConfigurationProvider)
            .ToListAsync(cancellationToken);
    }
}