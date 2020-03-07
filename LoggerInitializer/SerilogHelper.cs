namespace LoggerInitializer
{
    using System;
    using Serilog;
    using Serilog.Events;
    using System.Reflection;
    using Microsoft.AspNetCore.Http;
    using Serilog.Sinks.SystemConsole.Themes;
    using Serilog.Enrichers.AspnetcoreHttpcontext;

    public static class SerilogHelper
    {
        public static void LoggerInitialization(this LoggerConfiguration loggerConfig, IServiceProvider provider)
        {
            var logFilePath = "C:/Users/stmih/Desktop/temp/logs/flatlog-api.json";
            var outputTemplateContent = "[{Timestamp:HH:mm:ss} {Level}] {SourceContext}{NewLine}{Message:lj}{NewLine}{Exception}{NewLine}";
            var themeType = AnsiConsoleTheme.Literate;
            var assemblyName = Assembly.GetEntryAssembly()?.GetName();

            loggerConfig
                .MinimumLevel.Debug()
                .MinimumLevel.Override("Default", LogEventLevel.Information)
                .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
                .MinimumLevel.Override("System", LogEventLevel.Warning)
                .Enrich.FromLogContext()
                .Enrich.WithMachineName()
                .Enrich.WithProperty("Assembly", assemblyName)
                .Enrich.WithAspnetcoreHttpcontext(provider, GetContextInfo)
                .WriteTo.Console(outputTemplate: outputTemplateContent, theme: themeType)
                .WriteTo.File(logFilePath);
        }

        private static ContextInformation GetContextInfo(IHttpContextAccessor hca)
        {
            var ctx = hca.HttpContext;
            if (ctx == null) return null;

            return new ContextInformation
            {
                RemoteIpAddress = ctx.Connection.RemoteIpAddress.ToString(),
                Host = ctx.Request.Host.ToString(),
                Method = ctx.Request.Method,
                Protocol = ctx.Request.Protocol,
            };
        }
    }
}
