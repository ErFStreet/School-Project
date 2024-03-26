namespace Domain.Entities.Learn;

public class Class : BaseEntity<int>
{
    public required string ClassCode { get; set; }

    #region Relation
    public List<LearnRelation>? LearnRelations { get; set; }
    #endregion /Relation
}
