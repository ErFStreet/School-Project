namespace Server.Servicrs;

public class LearnRelationService : ILearnRelationService
{
    #region Fields
    private readonly ApplicationDbContext _applicationDbContext;
    #endregion /Fields

    #region Constructure
    public LearnRelationService(ApplicationDbContext applicationDbContext) : base()
    {
        _applicationDbContext = applicationDbContext;
    }
    #endregion /Constructure

    #region Methods
    public async Task<Response> CreateAsync(CreateLearnRelationViewModel viewModel)
    {
        var response = new Response();

        var learnRelation = new LearnRelation
        {
            LeassonId = viewModel.LessonId,
            ClassId = viewModel.ClassId,
        };

        await _applicationDbContext.AddAsync(learnRelation);

        var result =
            await _applicationDbContext.SaveChangesAsync();

        if (result > 0)
        {
            response.AddMessage(message: ResponseMessages.Success);

            response.ChangeStatusCode(statusCodeEnum: HttpStatusCodeEnum.Success);

            return response;
        }

        response.AddMessage(message: ResponseMessages.Success);

        response.ChangeStatusCode(statusCodeEnum: HttpStatusCodeEnum.Success);

        return response;
    }

    public async Task<Response> DeleteAsync(int learnRelationId)
    {
        var response = new Response();

        var learnRelation =
            await _applicationDbContext.LearnRelations!.FindAsync(learnRelationId);

        if (learnRelation is null)
        {
            response.AddMessage(message: ResponseMessages.BadRequest);

            response.ChangeStatusCode(statusCodeEnum: HttpStatusCodeEnum.BadRequest);

            return response;
        }

        _applicationDbContext.Remove(learnRelation);

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

    public async Task<Result<List<ListLearnRelationViewModel>>> GetAllAsync()
    {
        var response = new Result<List<ListLearnRelationViewModel>>();

        var result =
            await _applicationDbContext.LearnRelations!
            .AsQueryable()
            .Include(current => current.Class)
            .Include(current => current.Lesson)
            .Select(current => new ListLearnRelationViewModel
            {
                Id = current.Id,
                LessonName = current.Lesson!.LessonName,
                ClassCode = current.Class!.ClassCode,
            })
            .ToListAsync();

        response.AddMessage(message: ResponseMessages.Success);

        response.ChangeStatusCode(statusCodeEnum: HttpStatusCodeEnum.Success);

        response.Value = result;

        return response;
    }
    #endregion /Methods
}
