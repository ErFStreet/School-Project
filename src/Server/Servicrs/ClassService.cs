namespace Server.Servicrs;

public class ClassService : IClassService
{
    #region Fields
    private readonly ApplicationDbContext _applicationDbContext;
    #endregion /Fields

    #region Constructure
    public ClassService(ApplicationDbContext applicationDbContext) : base()
    {
        _applicationDbContext = applicationDbContext;
    }
    #endregion /Constructure

    #region Methods
    public async Task<Response> CreateAsync(CreateClassViewModel viewModel)
    {
        var response = new Response();

        var classModel = new Class
        {
            ClassCode = viewModel.ClassCode,
        };

        await _applicationDbContext.AddAsync(classModel);

        var result =
            await _applicationDbContext.SaveChangesAsync();

        if (result > 0)
        {
            response.AddMessage(message: ResponseMessages.Success);

            response.ChangeStatusCode(statusCodeEnum: HttpStatusCodeEnum.Success);

            return response;
        }

        response.AddMessage(message: ResponseMessages.ServerError);

        response.ChangeStatusCode(statusCodeEnum: HttpStatusCodeEnum.ServerError);

        return response;
    }


    public async Task<Response> DeleteAsync(int classId)
    {
        var response = new Response();

        var classModel =
            await _applicationDbContext.Classes!.FindAsync(keyValues: classId);

        if (classModel is null)
        {
            response.AddMessage(message: ResponseMessages.BadRequest);

            response.ChangeStatusCode(statusCodeEnum: HttpStatusCodeEnum.BadRequest);

            return response;
        }

        _applicationDbContext.Remove(classModel);

        var result =
            await _applicationDbContext.SaveChangesAsync();

        if (result > 0)
        {
            response.AddMessage(message: ResponseMessages.Success);

            response.ChangeStatusCode(statusCodeEnum: HttpStatusCodeEnum.Success);

            return response;
        }

        response.AddMessage(message: ResponseMessages.ServerError);

        response.ChangeStatusCode(statusCodeEnum: HttpStatusCodeEnum.ServerError);

        return response;
    }

    public async Task<Result<List<ListClassViewModel>>> GetAllAsync()
    {
        var response = new Result<List<ListClassViewModel>>();

        var result =
            await _applicationDbContext.Classes!
            .AsQueryable()
            .Where(current => !current.IsDeleted)
            .Select(current => new ListClassViewModel
            {
                Id = current.Id,
                ClassCode = current.ClassCode,
            })
            .ToListAsync();

        response.AddMessage(message: ResponseMessages.Success);

        response.ChangeStatusCode(statusCodeEnum: HttpStatusCodeEnum.Success);

        response.Value = result;

        return response;
    }

    public async Task<List<ListClassViewModel>> GetAllClassAsync()
    {
        var result =
            await _applicationDbContext.Classes!
            .Where(current=>!current.IsDeleted)
            .Select(current=>new ListClassViewModel
            {
                Id = current.Id,
                ClassCode = current.ClassCode,
            })
            .ToListAsync();

        return result;
    }
    #endregion /Methods
}
