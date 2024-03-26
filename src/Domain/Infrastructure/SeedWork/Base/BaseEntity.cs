namespace Domain.Infrastructure.SeedWork.Base;

public abstract class BaseEntity<TKey>
{
    protected BaseEntity() : base()
    {
    }

    public TKey? Id { get; set; }
}
