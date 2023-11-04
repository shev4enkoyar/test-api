using LTA.Domain.Common;

namespace LTA.Domain.Entities;

public class Album : BaseEntity<int>
{
    public int UserId { get; set; }

    public string Title { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}