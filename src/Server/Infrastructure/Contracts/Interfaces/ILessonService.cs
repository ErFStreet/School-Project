namespace Server.Infrastructure.Contracts.Interfaces;

public interface ILessonService
{
    Task<Response> CreateAsync(CreateLessonViewModel viewModel);

    Task<Response> DeleteAsync(int lessonId);

    Task<Response> EditAsync(EditLessonViewModel viewModel);

    Task<Result<List<ListLessonViewModel>>> GetAllAsync();

    Task<Result<DetailLessonViewModel>> GetLessonByIdAsync(int lessonId);

    Task<Result<List<ListLessonViewModel>>> GetLessonsAsync();
}