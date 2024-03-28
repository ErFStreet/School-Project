using Data.SeedWork.Contracts;

namespace Data.SeedWork;

public class UnitOfWork : IUnitOfWork
{
    #region Fields
    private readonly ApplicationDbContext _applicationDbContext;
    #endregion /Fields

    #region Constructure
    public UnitOfWork(ApplicationDbContext applicationDbContext)
    {
        _applicationDbContext = applicationDbContext;
    }
    #endregion /Constructure

    #region Methods
    public async Task<bool> SaveChangesAsync()
    {
        var result =
            await _applicationDbContext.SaveChangesAsync();

        if (result > 0)
        {
            return true;
        }

        return false;
    }
    #endregion /Methods
}
