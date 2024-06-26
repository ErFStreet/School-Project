﻿namespace Dtat.Logging;

public class LogModel : object
{
    public LogModel(string logLevel) : base()
    {
        LogLevel = logLevel;

    }

    public string LogLevel { get; set; }

    public string? ApplicationName { get; set; }

    public string? Namespace { get; set; }

    public string? ClassName { get; set; }

    public string? MethodName { get; set; }

    public string? LocalIP { get; set; }

    public string? LocalPort { get; set; }

    public string? RemoteIP { get; set; }

    public string? UserName { get; set; }

    public string? RequestPath { get; set; }

    public string? HttpReferrer { get; set; }



    public string? Message { get; set; }

    public List<object>? Parameters { get; set; }

    public List<ExceptionModel>? Exceptions { get; set; }
}

public class ExceptionModel : object
{
    public ExceptionModel(string message) : base()
    {
        Message = message;
    }

    public string Message { get; set; }

    public string? StackTrace { get; set; }
}
