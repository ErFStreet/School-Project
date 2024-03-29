namespace Domain.ViewModels.Lesson;

public class CreateLessonViewModel : object
{
    public CreateLessonViewModel() : base()
    {
    }

    public required string LessonName { get; set; }

    public required string LessonDescription { get; set; }
}
