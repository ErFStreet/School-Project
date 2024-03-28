namespace Domain.ViewModels.Class;

public class ListClassViewModel : object
{
    public ListClassViewModel() : base()
    {
    }

    public int Id { get;set; }

    public required string ClassCode { get; set; }
}
