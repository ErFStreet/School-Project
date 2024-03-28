namespace Server.Infrastructure.Contracts.Interfaces;

public interface IClassService
{
    Task<Response> CreateAsync(CreateClassViewModel viewModel);

    Task<Response> DeleteAsync(int classId);

    Task<Result<List<ListClassViewModel>>> GetAllAsync();

    Task<List<ListClassViewModel>> GetAllClassAsync();
}
