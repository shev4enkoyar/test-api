using LTA.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace LTA.Application.Common.Interfaces;

public interface IApplicationDbContext
{
    DbSet<User> Users { get; }
    
    DbSet<Album> Albums { get; }
    
    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}