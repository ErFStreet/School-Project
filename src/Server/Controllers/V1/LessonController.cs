namespace Server.Controllers.V1;

public class LessonController : BaseController
{
    #region Fields
    private readonly ILessonService _lessonService;
    #endregion /Fields

    #region Constructure
    public LessonController(ILessonService lessonService) : base()
    {
        _lessonService = lessonService;
    }
    #endregion /Constructure

    #region Methods
    [HttpGet("InformationLesson")]
    public async Task<ActionResult<Result<DetailLessonViewModel>>> 
        InformationLesson(int lessonId)
    {
        var response = 
            await _lessonService.GetLessonByIdAsync(lessonId);

        return response;
    }

    [HttpGet("Lessons")]
    public async Task<ActionResult<Result<List<ListLessonViewModel>>>> Lessons()
    {
        var response =
            await _lessonService.GetAllAsync();

        return response;
    }

    [HttpPost("CreateLesson")]
    public async Task<ActionResult<Response>> 
        CreateLesson([FromBody] CreateLessonViewModel viewModel)
    {
        var response =
            await _lessonService.CreateAsync(viewModel);

        return response;
    }

    [HttpPut("Edit")]
    public async Task<ActionResult<Response>> Edit([FromBody] EditLessonViewModel viewModel)
    {
        var response = 
            await _lessonService.EditAsync(viewModel);

        return response;
    }

    [HttpDelete("Delete")]
    public async Task<ActionResult<Response>> Delete(int lessonId)
    {
        var response =
            await _lessonService.DeleteAsync(lessonId);

        return response;
    }
    #endregion /Methods
}
