namespace Helper.Response;

public class Response : object
{
    public Response() : base()
    {
        Messages = new List<string>();
    }

    public int StatusCode { get; set; }

    public List<string> Messages { get; set; }


    // Methods
    public void AddMessage(string message)
    {
        Messages.Add(message);
    }

    public void RemoveMessage(string message)
    {
        Messages.Remove(message);
    }

    public void ChangeStatusCode(HttpStatusCodeEnum statusCodeEnum)
    {
        StatusCode = (int)statusCodeEnum;
    }
}
