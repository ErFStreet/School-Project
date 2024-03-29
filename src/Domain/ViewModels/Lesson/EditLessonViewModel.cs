namespace Domain.ViewModels.Lesson;

public class EditLessonViewModel : object
{
    public EditLessonViewModel() : base()
    {
    }

    public int Id { get; set; }

    public required string LessonName { get; set; }

    public required string LessonDescription { get; set; }
}
