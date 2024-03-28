namespace Data.SeedWork.Contracts
{
    public interface IUnitOfWork
    {
        Task<bool> SaveChangesAsync();
    }
}