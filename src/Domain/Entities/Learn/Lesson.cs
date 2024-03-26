namespace Domain.Entities.Learn;

public class Lesson : BaseEntity<int>
{
    public Lesson() : base()
    {
    }

    public required string LessonName { get; set; }

    public required string LessonDescription { get; set; }
}
