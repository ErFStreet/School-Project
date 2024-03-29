namespace Server.Infrastructure.Contracts.Interfaces;

public interface IAuthenticationService
{
    Task<Result<string>> LoginAsync(LoginUserViewModel viewModel);
}