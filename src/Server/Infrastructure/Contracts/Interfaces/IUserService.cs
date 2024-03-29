namespace Server.Infrastructure.Contracts.Interfaces;

public interface IUserService
{
    Task<Response> ChangeUserRoleAsync(int userId, string roleName);

    Task<Result<DetailUserViewModel>> CreateAsync(CreateUserViewModel viewModel);

    Task<Response> DeleteAsync(int userId);

    Task<Response> EditAsync(EditUserViewModel viewModel);

    Task<Result<List<ListUserViewModel>>> GetUsersAsync(string roleName);

    Task<Result<List<ListRoleViewModel>>> GetAllRolesAsync();

    Task<Result<DetailUserViewModel>> GetUserByIdAsync(int userId);
}