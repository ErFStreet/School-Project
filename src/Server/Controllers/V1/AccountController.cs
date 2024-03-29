namespace Server.Controllers.V1;

public class AccountController : BaseController
{
    #region Fields
    private readonly IUserService _userService;
    #endregion /Fields

    #region Constructure
    public AccountController(IUserService userService) : base()
    {
        _userService = userService;
    }
    #endregion /Constructure

    #region Methods
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

    [HttpGet("InformationUser")]
    public async Task<ActionResult<Result<DetailUserViewModel>>>
        InformationUser(int userId)
    {
        var response =
            await _userService.GetUserByIdAsync(userId);

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

    [HttpPut("Edit")]
    public async Task<ActionResult<Response>> Edit([FromBody] EditUserViewModel viewModel)
    {
        var response =
            await _userService.EditAsync(viewModel);

        return response;
    }

    [HttpPut("ChangeUserRole")]
    public async Task<ActionResult<Response>> ChangeUserRole(int userId, string roleName)
    {
        var response =
            await _userService.ChangeUserRoleAsync(userId, roleName);

        return response;
    }

    [HttpDelete("Delete")]
    public async Task<ActionResult<Response>> Delete(int userId)
    {
        var response =
            await _userService.DeleteAsync(userId);

        return response;
    }
    #endregion /Methods
}
