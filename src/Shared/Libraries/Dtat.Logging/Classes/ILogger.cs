﻿namespace Dtat.Logging.Classes;

public interface ILogger<T> where T : class
{
    IHttpContextAccessor? HttpContextAccessor { get; }

    bool IsTraceEnabled { get; set; }

    bool IsDebugEnabled { get; set; }

    bool IsInformationEnabled { get; set; }

    bool IsErrorEnabled { get; set; }

    bool IsCriticalEnabled { get; set; }

    bool IsWarningEnabled { get; set; }

    bool LogCritical(Exception exception, string? message = null, [CallerMemberName] string? methodName = null, List<object?>? parameters = null);

    bool LogDebug(string message, [CallerMemberName] string? methodName = null, List<object?>? parameters = null);

    bool LogError(Exception exception, string? message = null, [CallerMemberName] string? methodName = null, List<object?>? parameters = null);

    bool LogInformation(string message, [CallerMemberName] string? methodName = null, List<object?>? parameters = null);

    bool LogTrace(string message, [CallerMemberName] string? methodName = null, List<object?>? parameters = null);

    bool LogWarning(string message, [CallerMemberName] string? methodName = null, List<object?>? parameters = null);
}