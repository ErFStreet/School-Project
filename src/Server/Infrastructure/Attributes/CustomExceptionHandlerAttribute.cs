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
        if (context.Exception != null)
        {
            var actionName =
                context.RouteData.Values["action"]?.ToString();

            _logger.LogCritical
                (exception: context.Exception,
                    message: context.Exception.Message,
                    methodName: actionName, classType: context.Controller.GetType());

            var result = new Response();

            result.AddMessage(message: ResponseMessages.UnKnowError);

            result.ChangeStatusCode(statusCodeEnum: HttpStatusCodeEnum.UnKnowError);

            context.Result = result.ApiResult();

            context.ExceptionHandled = true;
        }

        base.OnActionExecuted(context);
    }
}
