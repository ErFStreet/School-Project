namespace Dtat.Logging.Classes;

public abstract class LoggerBase
{
    #region Constructure
    protected LoggerBase(IHttpContextAccessor? httpContextAccessor = null)
    {
        HttpContextAccessor = httpContextAccessor;
    }
    #endregion /Constructure

    #region Properties
    public IHttpContextAccessor? HttpContextAccessor { get; }
    public bool IsTraceEnabled { get; set; } = false;
    public bool IsDebugEnabled { get; set; } = false;
    public bool IsInformationEnabled { get; set; } = false;
    public bool IsErrorEnabled { get; set; } = false;
    public bool IsCriticalEnabled { get; set; } = false;
    public bool IsWarningEnabled { get; set; } = false;
    #endregion /Properties

    #region GetException Method
    protected virtual List<ExceptionModel>? GetExceptions(Exception? exception)
    {
        if (exception is null)
            return null;

        var exceptions = new List<ExceptionModel>();

        var currentException = exception;

        var index = 1;

        while (currentException is not null)
        {
            exceptions.Add(new ExceptionModel(
                $"{currentException.Message} - (Message Level : {index})")
            {
                StackTrace = currentException.StackTrace,
            });

            currentException =
                currentException.InnerException;

            index++;
        }

        return exceptions;
    }
    #endregion /GetException Method

    #region GetParameters Method

    protected virtual List<object>? GetParameters(List<object>? parameters)
    {
        List<object>? result = null;

        if (parameters is not null && parameters.Count > 0)
        {
            result = new List<object>();

            parameters.ForEach(current =>
            {
                if (current is not null)
                {
                    result.Add(current);
                }
            });
        }

        return result;
    }
    #endregion /GetParameters Method

    #region LogByFavoriteLibrary
    protected abstract void LogByFavoriteLibrary(LogModel logModel, Exception? exception);
    #endregion /LogByFavoriteLibrary
}

public abstract class Logger<T> : LoggerBase, ILogger<T> where T : class
{
    #region Constructure
    protected Logger(IHttpContextAccessor? httpContextAccessor = null)
        : base(httpContextAccessor)
    {
    }
    #endregion /Constructure

    #region Log
    protected bool Log(string logLevel,
        string? message,
        Exception? exception = null,
        List<object>? parameters = null,
        string? methodName = null)
    {
        try
        {
            //****************************************************
            string currentCultureName =
                Thread.CurrentThread.CurrentCulture.Name;

            var newCultureInfo =
                new CultureInfo(name: "en-US");

            var currentCultureInfo =
                new CultureInfo(name: currentCultureName);

            Thread.CurrentThread.CurrentCulture = newCultureInfo;
            //****************************************************

            var connection =
                HttpContextAccessor?.HttpContext?.Connection;

            var httpRequest =
                HttpContextAccessor?.HttpContext?.Request;

            var logModel = new LogModel(logLevel: logLevel)
            {
                Message = message,
                Namespace = typeof(T).Namespace,
                RemoteIP = connection?.RemoteIpAddress?.ToString(),
                LocalIP = connection?.LocalIpAddress?.ToString(),
                LocalPort = connection?.LocalPort.ToString(),
                UserName = HttpContextAccessor?.HttpContext?.User?.Identity?.Name,
                RequestPath = httpRequest?.Path,
                HttpReferrer = httpRequest?.Headers["Referer"],
                ApplicationName = typeof(T).GetTypeInfo()?.Assembly?.FullName?.ToString(),
                ClassName = typeof(T).Name,
                Parameters = parameters,
                Exceptions = GetExceptions(exception: exception),
                MethodName = methodName,
            };

            LogByFavoriteLibrary(logModel: logModel, exception: exception);

            // **************************************************
            Thread.CurrentThread.CurrentCulture = currentCultureInfo;
            // **************************************************

            return true;

        }
        catch (Exception)
        {
            return false;
        }
    }
    #endregion /Log

    #region LogTrace
    public virtual bool LogTrace
        (string message,
        [CallerMemberName] string? methodName = null,
        List<object?>? parameters = null)
    {
        if (string.IsNullOrWhiteSpace(message))
            return false;

        if (!IsTraceEnabled)
            return false;

        bool result =
            Log(methodName: methodName,
                logLevel: nameof(LogLevelEnum.Trace),
                message: message,
                exception: null,
                parameters: parameters!);

        return result;
    }
    #endregion /LogTrace

    #region LogDebug
    public virtual bool LogDebug
        (string message,
        [CallerMemberName] string? methodName = null,
        List<object?>? parameters = null)
    {
        if (string.IsNullOrWhiteSpace(message))
            return false;

        if (!IsDebugEnabled)
            return false;

        bool result =
            Log(methodName: methodName,
                logLevel: nameof(LogLevelEnum.Debug),
                message: message,
                exception: null,
                parameters: parameters!);

        return result;
    }
    #endregion /LogDebug

    #region LogInformation
    public virtual bool LogInformation
        (string message,
        [CallerMemberName] string? methodName = null,
        List<object?>? parameters = null)
    {
        if (string.IsNullOrWhiteSpace(message))
            return false;

        if (!IsInformationEnabled)
            return false;

        bool result =
            Log(methodName: methodName,
                logLevel: nameof(LogLevelEnum.Information),
                message: message,
                exception: null,
                parameters: parameters!);

        return result;
    }
    #endregion /LogInformation

    #region LogWarning
    public virtual bool LogWarning
        (string message,
        [CallerMemberName] string? methodName = null,
        List<object?>? parameters = null)
    {
        if (string.IsNullOrWhiteSpace(message))
            return false;

        if (!IsWarningEnabled)
            return false;

        bool result =
            Log(methodName: methodName,
                logLevel: nameof(LogLevelEnum.Warning),
                message: message,
                exception: null,
                parameters: parameters!);

        return result;
    }
    #endregion /LogWarning

    #region LogError
    public virtual bool LogError
        (Exception exception,
        string? message = null,
        [CallerMemberName] string? methodName = null,
        List<object?>? parameters = null)
    {
        if (exception == null)
            return false;

        if (!IsErrorEnabled)
            return false;

        bool result =
            Log(methodName: methodName,
                logLevel: nameof(LogLevelEnum.Error),
                message: message,
                exception: exception,
                parameters: parameters!);

        return result;
    }
    #endregion /LogError

    #region LogCritical
    public virtual bool LogCritical
        (Exception exception,
        string? message = null,
        [CallerMemberName] string? methodName = null,
        List<object?>? parameters = null)
    {
        if (exception == null)
            return false;

        if (!IsCriticalEnabled)
            return false;

        bool result =
            Log(methodName: methodName,
                logLevel: nameof(LogLevelEnum.Critical),
                message: message,
                exception: exception,
                parameters: parameters!);

        return result;
    }
    #endregion /LogCritical
}

public abstract class Logger : LoggerBase, ILogger
{
    #region Constructor
    protected Logger(IHttpContextAccessor? httpContextAccessor = null) : base(httpContextAccessor)
    {
    }
    #endregion /Constructor

    #region Log
    protected bool Log(string logLevel,
        string? message,
        Type classType,
        Exception? exception = null,
        List<object?>? parameters = null,
        string? methodName = null)
    {
        try
        {
            // **************************************************
            string currentCultureName =
                Thread.CurrentThread.CurrentCulture.Name;

            var newCultureInfo =
                new CultureInfo(name: "en-US");

            var currentCultureInfo =
                new CultureInfo(currentCultureName);

            Thread.CurrentThread.CurrentCulture = newCultureInfo;
            // **************************************************

            var connection =
                HttpContextAccessor?.HttpContext?.Connection;

            var httpRequest =
                HttpContextAccessor?.HttpContext?.Request;

            var logModel = new LogModel(logLevel: logLevel)
            {
                Message = message,
                Namespace = classType?.Namespace,
                RemoteIP = connection?.RemoteIpAddress?.ToString(),
                LocalIP = connection?.LocalIpAddress?.ToString(),
                LocalPort = connection?.LocalPort.ToString(),
                UserName = HttpContextAccessor?.HttpContext?.User?.Identity?.Name,
                RequestPath = httpRequest?.Path,
                HttpReferrer = httpRequest?.Headers["Referer"],
                ApplicationName = classType?.Assembly?.FullName?.ToString(),
                ClassName = classType?.Name,
                Parameters = parameters!,
                Exceptions = GetExceptions(exception: exception),
                MethodName = methodName,
            };

            LogByFavoriteLibrary(logModel: logModel, exception: exception);

            // **************************************************
            Thread.CurrentThread.CurrentCulture = currentCultureInfo;
            // **************************************************

            return true;
        }
        catch
        {
            return false;
        }
    }
    #endregion /Log

    #region LogTrace
    public virtual bool LogTrace
        (string message,
        Type classType,
        [CallerMemberName] string? methodName = null,
        List<object?>? parameters = null)
    {
        if (string.IsNullOrWhiteSpace(message))
            return false;

        if (!IsTraceEnabled)
            return false;

        bool result =
            Log(methodName: methodName,
                logLevel: nameof(LogLevelEnum.Trace),
                classType: classType,
                message: message,
                exception: null,
                parameters: parameters);

        return result;
    }
    #endregion /LogTrace

    #region LogDebug
    public virtual bool LogDebug
        (string message,
        Type classType,
        [CallerMemberName] string? methodName = null,
        List<object?>? parameters = null)
    {
        if (string.IsNullOrWhiteSpace(message))
            return false;

        if (!IsDebugEnabled)
            return false;

        bool result =
            Log(methodName: methodName,
                logLevel: nameof(LogLevelEnum.Debug),
                message: message,
                classType: classType,
                exception: null,
                parameters: parameters);

        return result;
    }
    #endregion /LogDebug

    #region LogInformation
    public virtual bool LogInformation
        (string message,
        Type classType,
        [CallerMemberName] string? methodName = null,
        List<object?>? parameters = null)
    {
        if (string.IsNullOrWhiteSpace(message))
            return false;

        if (!IsInformationEnabled)
            return false;

        bool result =
            Log(methodName: methodName,
                logLevel: nameof(LogLevelEnum.Information),
                message: message,
                classType: classType,
                exception: null,
                parameters: parameters);

        return result;
    }
    #endregion /LogInformation

    #region LogWarning
    public virtual bool LogWarning
        (string message,
        Type classType,
        [CallerMemberName] string? methodName = null,
        List<object?>? parameters = null)
    {
        if (string.IsNullOrWhiteSpace(message))
            return false;

        if (!IsWarningEnabled)
            return false;

        bool result =
            Log(methodName: methodName,
                logLevel: nameof(LogLevelEnum.Warning),
                message: message,
                classType: classType,
                exception: null,
                parameters: parameters);

        return result;
    }
    #endregion /LogWarning

    #region LogError
    public virtual bool LogError
        (Exception exception,
        Type classType,
        string? message = null,
        [CallerMemberName] string? methodName = null,
        List<object?>? parameters = null)
    {
        if (exception == null)
            return false;

        if (!IsErrorEnabled)
            return false;

        bool result =
            Log(methodName: methodName,
                logLevel: nameof(LogLevelEnum.Error),
                message: message,
                classType: classType,
                exception: exception,
                parameters: parameters);

        return result;
    }
    #endregion /LogError

    #region LogCritical
    public virtual bool LogCritical
        (Exception exception,
        Type classType,
        string? message = null,
        [CallerMemberName] string? methodName = null,
        List<object?>? parameters = null)
    {
        if (exception == null)
            return false;

        if (!IsCriticalEnabled)
            return false;

        bool result =
            Log(methodName: methodName,
                logLevel: nameof(LogLevelEnum.Critical),
                message: message,
                classType: classType,
                exception: exception,
                parameters: parameters);

        return result;
    }
    #endregion /LogCritical
}