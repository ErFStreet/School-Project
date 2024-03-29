namespace Server.Controllers.V1;

public class LearnController : BaseController
{
    #region Fields
    private readonly ILearnRelationService _learnRelationService;
    #endregion /Fields

    #region Constructure
    public LearnController(ILearnRelationService learnRelationService) : base()
    {
        _learnRelationService = learnRelationService;
    }
    #endregion /Constructure

    #region Methods
    [HttpGet("Learns")]
    public async Task<ActionResult<Result<List<ListLearnRelationViewModel>>>> Learns()
    {
        var response =
            await _learnRelationService.GetAllAsync();

        return response;
    }

    [HttpPost("CreateLearn")]
    public async Task<Response> CreateLearn
        ([FromBody] CreateLearnRelationViewModel viewModel)
    {
        var response =
            await _learnRelationService.CreateAsync(viewModel);

        return response;
    }

    [HttpDelete("Delete")]
    public async Task<Response> Delete(int learnRelationId)
    {
        var response =
            await _learnRelationService.DeleteAsync(learnRelationId);

        return response;
    }
    #endregion /Methods
}
