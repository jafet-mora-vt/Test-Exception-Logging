using Microsoft.ApplicationInsights;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;

public class CustomException : ExceptionFilterAttribute
{
    private readonly TelemetryClient telemetryClient;

    public CustomException(TelemetryClient telemetryClient)
    {
        this.telemetryClient = telemetryClient ?? throw new ArgumentNullException(nameof(telemetryClient));
    }

    public override void OnException(ExceptionContext context)
    {
        // Log exception to Application Insights
        telemetryClient.TrackException(context.Exception);

        // Optionally, log additional information like request details
        telemetryClient.TrackTrace($"Request: {context.HttpContext.Request.Path}, Exception: {context.Exception.Message}");

        // Allow the original exception handling to continue
        base.OnException(context);
    }
}