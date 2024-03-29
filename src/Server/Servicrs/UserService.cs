namespace Server.Servicrs;

public class UserService : IUserService
{
    #region Fields
    private readonly UserManager<User> _userManager;

    private readonly RoleManager<Role> _roleManager;

    private readonly IUnitOfWork _unitOfWork;

    private readonly IClassService _classService;
    #endregion /Fields

    #region Constructure
    public UserService(UserManager<User> userManager, RoleManager<Role> roleManager
        , IUnitOfWork unitOfWork, IClassService classService)
    {
        _userManager = userManager;

        _roleManager = roleManager;

        _unitOfWork = unitOfWork;

        _classService = classService;
    }
    #endregion /Constructure

    #region Methods
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

        if (!userCreateResult.Succeeded)
        {
            response.AddMessage(message: ResponseMessages.ServerError);

            response.ChangeStatusCode(statusCodeEnum: HttpStatusCodeEnum.ServerError);

            return response;
        }

        var roleCreateResult =
            await _userManager.AddToRoleAsync(user: user, role: viewModel.RoleName);

        if (!roleCreateResult.Succeeded)
        {
            response.AddMessage(message: ResponseMessages.ServerError);

            response.ChangeStatusCode(statusCodeEnum: HttpStatusCodeEnum.ServerError);

            return response;
        }

        response.AddMessage(message: ResponseMessages.Success);

        response.ChangeStatusCode(statusCodeEnum: HttpStatusCodeEnum.Success);

        response.Value = new DetailUserViewModel
        {
            FirstName = viewModel.FirstName,
            LastName = viewModel.LastName,
            UserName = viewModel.UserName,
            Password = viewModel.Password,
        };

        return response;
    }

    public async Task<Result<DetailUserViewModel>> GetUserByIdAsync(int userId)
    {
        var response = new Result<DetailUserViewModel>();

        var result =
            await _userManager.Users!
            .AsQueryable()
            .Include(current => current.Class)
            .Where(current => current.Id == userId)
            .Select(current => new DetailUserViewModel
            {
                Id = current.Id,
                FirstName = current.FirstName,
                LastName = current.LastName,
                UserName = current.UserName!,
                ClassCode = current.Class!.ClassCode!,
                ClassId = current.ClassId,
            })
            .FirstOrDefaultAsync();

        response.AddMessage(message: ResponseMessages.Success);

        response.ChangeStatusCode(statusCodeEnum: HttpStatusCodeEnum.Success);

        response.Value = result;

        return response;
    }

    public async Task<Response> EditAsync(EditUserViewModel viewModel)
    {
        var response = new Response();

        var user =
            await _userManager.FindByIdAsync(viewModel.Id.ToString());

        if (user is null)
        {
            response.AddMessage(message: ResponseMessages.BadRequest);

            response.ChangeStatusCode(statusCodeEnum: HttpStatusCodeEnum.BadRequest);

            return response;
        }

        user.FirstName = viewModel.FirstName;

        user.LastName = viewModel.LastName;

        user.UserName = viewModel.UserName;

        user.ClassId = viewModel.ClassId;

        var result =
             await _userManager.UpdateAsync(user);

        if (!result.Succeeded)
        {
            response.AddMessage(message: ResponseMessages.ServerError);

            response.ChangeStatusCode(statusCodeEnum: HttpStatusCodeEnum.ServerError);

            return response;
        }

        response.AddMessage(message: ResponseMessages.Success);

        response.ChangeStatusCode(statusCodeEnum: HttpStatusCodeEnum.Success);

        return response;
    }

    public async Task<Response> DeleteAsync(int userId)
    {
        var response = new Response();

        var user =
            await _userManager.FindByIdAsync(userId.ToString());

        if (user is null)
        {
            response.AddMessage(message: ResponseMessages.BadRequest);

            response.ChangeStatusCode(statusCodeEnum: HttpStatusCodeEnum.BadRequest);

            return response;
        }

        var result =
            await _userManager.DeleteAsync(user);

        if (!result.Succeeded)
        {
            response.AddMessage(message: ResponseMessages.ServerError);

            response.ChangeStatusCode(statusCodeEnum: HttpStatusCodeEnum.ServerError);

            return response;
        }

        response.AddMessage(message: ResponseMessages.Success);

        response.ChangeStatusCode(statusCodeEnum: HttpStatusCodeEnum.Success);

        return response;
    }

    public async Task<Result<List<ListUserViewModel>>> GetUsersAsync(string roleName)
    {
        var response = new Result<List<ListUserViewModel>>();

        var getUsers =
            await _userManager.GetUsersInRoleAsync(roleName);

        var classes =
             await _classService.GetAllClassAsync();

        // Because we using Identity , We can't use include(current=> current.Class).
        // We need GetUsersInRoleAsync for search users with role 
        var result =
             getUsers
                    .AsQueryable()
                    .Include(current => current.Class)
                    .Select(current => new ListUserViewModel
                    {
                        FirstName = current.FirstName,
                        LastName = current.LastName,
                        ClassCode = current.ClassId == null ? null : classes
                                    .Where(c => c.Id == current.ClassId)
                                    .Select(c => c.ClassCode)
                                    .FirstOrDefault(),
                        RoleName = roleName,
                    })
                     .ToList();

        response.AddMessage(message: ResponseMessages.Success);

        response.ChangeStatusCode(statusCodeEnum: HttpStatusCodeEnum.Success);

        response.Value = result;

        return response;
    }

    public async Task<Result<List<ListRoleViewModel>>> GetAllRolesAsync()
    {
        var response = new Result<List<ListRoleViewModel>>();

        var result =
            await _roleManager.Roles!
            .AsQueryable()
            .Where(current => !current.IsDeleted)
            .Select(current => new ListRoleViewModel
            {
                Id = current.Id,
                RoleName = current.Name!,
            })
            .ToListAsync();

        response.AddMessage(message: ResponseMessages.Success);

        response.ChangeStatusCode(statusCodeEnum: HttpStatusCodeEnum.Success);

        response.Value = result;

        return response;
    }

    public async Task<Response> ChangeUserRoleAsync(int userId, string roleName)
    {
        var response = new Response();

        var user =
            await _userManager.FindByIdAsync(userId.ToString());

        if (user is null)
        {
            response.AddMessage(message: ResponseMessages.BadRequest);

            response.ChangeStatusCode(statusCodeEnum: HttpStatusCodeEnum.BadRequest);

            return response;
        }

        var roles =
            await _userManager.GetRolesAsync(user);

        var removeRolesResult =
            await _userManager.RemoveFromRolesAsync(user, roles);

        if (!removeRolesResult.Succeeded)
        {
            response.AddMessage(message: ResponseMessages.ServerError);

            response.ChangeStatusCode(statusCodeEnum: HttpStatusCodeEnum.ServerError);

            return response;
        }

        var result =
            await _userManager.AddToRoleAsync(user, roleName);

        if (!result.Succeeded)
        {
            response.AddMessage(message: ResponseMessages.ServerError);

            response.ChangeStatusCode(statusCodeEnum: HttpStatusCodeEnum.ServerError);

            return response;
        }

        response.AddMessage(message: ResponseMessages.Success);

        response.ChangeStatusCode(statusCodeEnum: HttpStatusCodeEnum.Success);

        return response;
    }
    #endregion /Methods
}
