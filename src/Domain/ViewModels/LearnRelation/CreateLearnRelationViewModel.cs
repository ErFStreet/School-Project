namespace Domain.ViewModels.LearnRelation;

public class CreateLearnRelationViewModel : object
{
    public CreateLearnRelationViewModel() : base()
    {
    }

    public int LessonId { get; set; }

    public int ClassId { get; set; }
}
