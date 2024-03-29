namespace Server.Infrastructure.Attributes;

public class CustomExceptionHandlerAttribute : ActionFilterAttribute
{
    private readonly ILogger _logger;

    public CustomExceptionHandlerAttribute(ILogger logger)
    {
        _logger = logger;
    }

    public override void OnActionExecuted(ActionExecutedContext context)
    {
        if (context.Exception is not null)
        {
            var actionName =
                context.RouteData.Values["action"]?.ToString();

            _logger.LogCritical
                (exception: context.Exception,
                actionName, context.Controller.GetType());

            var response = new Response();

            response.AddMessage(message: ResponseMessages.UnKnowError);

            response.ChangeStatusCode(statusCodeEnum: HttpStatusCodeEnum.UnKnowError);

            context.Result = response.ApiResult();

            context.ExceptionHandled = true;
        }

        base.OnActionExecuted(context);
    }
}
