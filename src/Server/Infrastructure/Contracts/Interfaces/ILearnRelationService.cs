namespace Server.Infrastructure.Contracts.Interfaces
{
    public interface ILearnRelationService
    {
        Task<Response> CreateAsync(CreateLearnRelationViewModel viewModel);
        Task<Response> DeleteAsync(int learnRelationId);
        Task<Result<List<ListLearnRelationViewModel>>> GetAllAsync();
    }
}