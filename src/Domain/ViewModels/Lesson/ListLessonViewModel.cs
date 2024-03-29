namespace Domain.ViewModels.Lesson;

public class ListLessonViewModel : object
{
    public ListLessonViewModel() : base()
    {
    }

    public int Id { get; set; }

    public required string LessonName { get; set; }

    public string? LessonDescription { get; set;}
}
