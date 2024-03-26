namespace Domain.Entities.Learn;

public class LearnRelation : BaseEntity<int>
{
    public LearnRelation():base()
    {
    }

    [ForeignKey("Class")]
    public int ClassId { get; set; }

    [ForeignKey("Lesson")]
    public int LeassonId { get; set; }

    #region Relation
    public Class? Class { get; set; }

    public Lesson? Lesson { get; set; }
    #endregion /Relation
}
