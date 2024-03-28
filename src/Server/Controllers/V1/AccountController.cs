namespace Server.Controllers.V1;

public class AccountController : BaseController
{
    private readonly IUserService _userService;

    public AccountController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpGet("Roles")]
    public async Task<Result<List<ListRoleViewModel>>> Roles()
    {
        var response =
            await _userService.GetAllRolesAsync();

        return response;
    }

    [HttpGet("Users")]
    public async Task<Result<List<ListUserViewModel>>> Users(string roleName)
    {
        var response =
            await _userService.GetUsersAsync(roleName);

        return response;
    }

    [HttpPost("CreateUser")]
    public async Task<ActionResult<Response>>
        CreateUser([FromBody] CreateUserViewModel viewModel)
    {
        var response =
            await _userService.CreateAsync(viewModel);

        return response;
    }

}
