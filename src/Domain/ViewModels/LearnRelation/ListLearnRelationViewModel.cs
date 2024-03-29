namespace Domain.ViewModels.LearnRelation;

public class ListLearnRelationViewModel : object
{
    public ListLearnRelationViewModel() : base()
    {
    }

    public int Id { get; set; }

    public required string LessonName { get; set; }

    public required string ClassCode { get; set; }
}
