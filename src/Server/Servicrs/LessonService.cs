namespace Server.Servicrs;

public class LessonService : ILessonService
{
    #region Fields
    private readonly ApplicationDbContext _applicationDbContext;
    #endregion /Fields

    #region Constructure
    public LessonService(ApplicationDbContext applicationDbContext) : base()
    {
        _applicationDbContext = applicationDbContext;
    }
    #endregion /Constructure

    #region Methods
    public async Task<Response> CreateAsync(CreateLessonViewModel viewModel)
    {
        var response = new Response();

        var lesson = new Lesson
        {
            LessonName = viewModel.LessonName,
            LessonDescription = viewModel.LessonDescription,
        };

        await _applicationDbContext.AddAsync(lesson);

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

    public async Task<Response> EditAsync(EditLessonViewModel viewModel)
    {
        var response = new Response();

        var lesson =
            await _applicationDbContext.Lessons!.FindAsync(viewModel.Id);

        if (lesson is null)
        {
            response.AddMessage(message: ResponseMessages.BadRequest);

            response.ChangeStatusCode(statusCodeEnum: HttpStatusCodeEnum.BadRequest);

            return response;
        }

        lesson.LessonName = viewModel.LessonName;

        lesson.LessonDescription = viewModel.LessonDescription;

        _applicationDbContext.Update(lesson);

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

    public async Task<Response> DeleteAsync(int lessonId)
    {
        var response = new Response();

        var lesson =
            await _applicationDbContext.Lessons!.FindAsync(lessonId);

        if (lesson is null)
        {
            response.AddMessage(message: ResponseMessages.BadRequest);

            response.ChangeStatusCode(statusCodeEnum: HttpStatusCodeEnum.BadRequest);

            return response;
        }

        _applicationDbContext.Remove(lesson);

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

    public async Task<Result<DetailLessonViewModel>> GetLessonByIdAsync(int lessonId)
    {
        var response = new Result<DetailLessonViewModel>();

        var result =
            await _applicationDbContext.Lessons!
            .AsQueryable()
            .Where(current => current.Id == lessonId)
            .Select(current => new DetailLessonViewModel
            {
                Id = current.Id,
                LessonName = current.LessonName,
                LessonDescription = current.LessonDescription,
            })
            .FirstOrDefaultAsync();

        response.AddMessage(message: ResponseMessages.Success);

        response.ChangeStatusCode(statusCodeEnum: HttpStatusCodeEnum.Success);

        response.Value = result;

        return response;
    }

    public async Task<Result<List<ListLessonViewModel>>> GetAllAsync()
    {
        var response = new Result<List<ListLessonViewModel>>();

        var result =
            await _applicationDbContext.Lessons!
            .AsQueryable()
            .Select(current => new ListLessonViewModel
            {
                Id = current.Id,
                LessonName = current.LessonName,
                LessonDescription = current.LessonDescription,
            })
            .ToListAsync();

        response.AddMessage(message: ResponseMessages.Success);

        response.ChangeStatusCode(statusCodeEnum: HttpStatusCodeEnum.Success);

        response.Value = result;

        return response;
    }

    public async Task<Result<List<ListLessonViewModel>>> GetLessonsAsync()
    {
        var response = new Result<List<ListLessonViewModel>>();

        var result =
            await _applicationDbContext.Lessons!
            .AsQueryable()
            .Select(current => new ListLessonViewModel
            {
                Id = current.Id,
                LessonName = current.LessonName,
            })
            .ToListAsync();

        response.AddMessage(message: ResponseMessages.Success);

        response.ChangeStatusCode(statusCodeEnum: HttpStatusCodeEnum.Success);

        response.Value = result;

        return response;
    }
    #endregion /Methods
}
