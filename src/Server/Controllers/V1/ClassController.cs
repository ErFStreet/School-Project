namespace Server.Controllers.V1;

public class ClassController : BaseController
{
    #region Fields
    private readonly IClassService _classService;
    #endregion /Fields

    #region Constructure
    public ClassController(IClassService classService) : base()
    {
        _classService = classService;
    }
    #endregion /Constructure

    #region Methods
    [HttpGet("Classes")]
    public async Task<ActionResult<Result<List<ListClassViewModel>>>> Classes()
    {
        var response =
            await _classService.GetAllAsync();

        return response;
    }

    [HttpPost("CreateClass")]
    public async Task<ActionResult<Response>> Create(CreateClassViewModel viewModel)
    {
        var response =
            await _classService.CreateAsync(viewModel: viewModel);

        return response;
    }

    [HttpDelete("DeleteClass")]
    public async Task<ActionResult<Response>> Delete(int classId)
    {
        var response =
            await _classService.DeleteAsync(classId: classId);

        return response;
    }
    #endregion /Methods
}
