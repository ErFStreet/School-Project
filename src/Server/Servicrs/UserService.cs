using Domain.ViewModels;

namespace Server.Servicrs;

public class UserService : object
{
    private readonly UserManager<User> _userManager;

    public UserService(UserManager<User> userManager)
    {
        _userManager = userManager;
    }

    public async Task<Result<DetailUserViewModel>> CreateAsync
            (CreateUserViewModel viewModel)
    {
        var response = new Result<DetailUserViewModel>();

        var user =
            new User
            {
                FirstName = viewModel.FirstName,
                LastName = viewModel.LastName,
                UserName = viewModel.UserName,
                ClassId = viewModel.ClassId,
            };

        var userCreateResult =
            await _userManager.CreateAsync(user: user, password: viewModel.Password);

        var userRoleCreateResult =
            await _userManager.AddToRoleAsync(user: user, role: viewModel.RoleName);

        if (!userCreateResult.Succeeded || !userRoleCreateResult.Succeeded)
        {
            response.AddMessage(message: ResponseMessages.ServerError);

            response.ChangeStatusCode(statusCodeEnum: HttpStatusCodeEnum.ServerError);

            return response;
        }

        response.AddMessage(message: ResponseMessages.Success);

        response.ChangeStatusCode(statusCodeEnum: HttpStatusCodeEnum.Success);

        response.Value = new DetailUserViewModel
        {
            FullName = $"{viewModel.FirstName} {viewModel.LastName}",
        };

        return response;
    }
}
