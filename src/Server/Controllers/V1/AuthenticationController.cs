namespace Server.Controllers.V1;

public class AuthenticationController : BaseController
{
    private readonly IAuthenticationService _authenticationService;

    public AuthenticationController(IAuthenticationService authenticationService) : base()
    {
        _authenticationService = authenticationService;
    }

    [HttpPost("Login")]
    public async Task<ActionResult<Result<string>>>
        Login([FromBody] LoginUserViewModel viewModel)
    {
        var response =
            await _authenticationService.LoginAsync(viewModel);

        return response;
    }
}
