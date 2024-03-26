namespace Helper.Response;

public class Result<TValue> : Response
{
    public TValue? Value { get; set; }
}
