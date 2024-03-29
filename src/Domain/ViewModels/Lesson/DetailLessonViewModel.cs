namespace Domain.ViewModels.Lesson;

public class DetailLessonViewModel : object
{
    public DetailLessonViewModel() : base()
    {
    }

    public int Id { get; set; }

    public required string LessonName { get; set; }

    public required string LessonDescription { get; set; }
}
