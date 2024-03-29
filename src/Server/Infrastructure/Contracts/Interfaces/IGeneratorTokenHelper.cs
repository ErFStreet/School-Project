namespace Server.Infrastructure.Contracts.Interfaces;

public interface IGeneratorTokenHelper
{
    string GenerateToken(TokenUserViewModel viewModel);
}