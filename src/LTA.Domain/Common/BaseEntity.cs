using System.ComponentModel.DataAnnotations.Schema;

namespace LTA.Domain.Common;

public abstract class BaseEntity<T>
{
    [DatabaseGenerated(DatabaseGeneratedOption.None)]
    public int Id { get; set; } = default!;
}