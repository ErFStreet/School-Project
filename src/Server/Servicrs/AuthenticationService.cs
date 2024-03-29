namespace Server.Servicrs;

public class AuthenticationService : IAuthenticationService
{
    #region Fields
    private readonly SignInManager<User> _signInManager;

    private readonly UserManager<User> _userManager;

    private readonly IGeneratorTokenHelper _generatorTokenHelper;
    #endregion /Fields

    #region Construture
    public AuthenticationService(SignInManager<User> signInManager,
        UserManager<User> userManager, IGeneratorTokenHelper generatorTokenHelper) : base()
    {
        _signInManager = signInManager;

        _userManager = userManager;

        _generatorTokenHelper = generatorTokenHelper;
    }
    #endregion /Construture

    #region Methods
    public async Task<Result<string>> LoginAsync(LoginUserViewModel viewModel)
    {
        var response = new Result<string>();

        var user =
            await _userManager.FindByNameAsync(viewModel.UserName);

        if (user == null)
        {
            response.AddMessage(message: ResponseMessages.NotFound);

            response.ChangeStatusCode(statusCodeEnum: HttpStatusCodeEnum.NotFound);

            return response;
        }

        var signInResult =
            await _signInManager.PasswordSignInAsync(user, viewModel.Password, false, false);

        var roles =
            await _userManager.GetRolesAsync(user);

        if (signInResult.Succeeded)
        {
            var userToken = new TokenUserViewModel
            {
                FullName = $"{user.FirstName} {user.LastName}",
                UserName = viewModel.UserName,
                Role = roles[0],
            };

            var token =
                _generatorTokenHelper.GenerateToken(userToken);

            response.AddMessage(message: ResponseMessages.Success);

            response.ChangeStatusCode(statusCodeEnum: HttpStatusCodeEnum.Success);

            response.Value = token;
        }

        response.AddMessage(message: ResponseMessages.NotFound);

        response.ChangeStatusCode(statusCodeEnum: HttpStatusCodeEnum.NotFound);

        return response;
    }
    #endregion /Methods
}
