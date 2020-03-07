namespace Logging
{
    using System;
    using Serilog;
    using Microsoft.Extensions.Configuration;
    using Serilog.Sinks.SystemConsole.Themes;

    public static class SerilogHelper
    {
        public static void LoggingTestAppConfiguration(this LoggerConfiguration loggerConfig, IServiceProvider provider, IConfiguration config)
        {
            var rollingFileName = config["Logging:RollingFileName"];
            var consoleOutputTemplate = "[{Timestamp:HH:mm:ss} {Level}] {SourceContext}{NewLine}{Message:lj}{NewLine}{Exception}{NewLine}";
            var consoleTheme = AnsiConsoleTheme.Literate;

            loggerConfig
                .ReadFrom.Configuration(config) // minimum levels defined per project in json files
                .Enrich.FromLogContext()
                //.WriteTo.Console(outputTemplate: consoleOutputTemplate, theme: consoleTheme)
                .WriteTo.File(rollingFileName);
                
        }
    }
}
