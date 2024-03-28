namespace Domain.Infrastructure.SeedWork.Base;

public abstract class BaseEntity<TKey>
{
    protected BaseEntity() : base()
    {
    }

    [Key]
    public TKey? Id { get; set; }
}
