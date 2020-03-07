namespace LoggingTestApp
{
    using System;
    using Serilog;
    using Serilog.Events;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.Extensions.Hosting;
    using Serilog.Sinks.SystemConsole.Themes;
    using Serilog.Enrichers.AspnetcoreHttpcontext;

    public class Program
    {
        public static void Main(string[] args)
        {
            SerilogConfig();

            try
            {
                var host = CreateHostBuilder(args).Build();

                Log.Information("Starting host..." + Environment.NewLine);

                host.Run();
            }
            catch (Exception ex)
            {
                Log.Fatal(ex, "Host terminated unexpectedly." + Environment.NewLine);
            }
            finally
            {
                Log.CloseAndFlush();
            }
            
        }

        private static void SerilogConfig()
        {
            var logFilePath = "C:/Users/stmih/Desktop/temp/logs/flatlog-api.json";
            var outputTemplateContent = 
                "[{Timestamp:HH:mm:ss} {Level}] {SourceContext}{NewLine}{Message:lj}{NewLine}{Exception}{NewLine}";
            var themeType = AnsiConsoleTheme.Literate;

            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Debug()
                .MinimumLevel.Override("Default", LogEventLevel.Information)
                .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
                .MinimumLevel.Override("System", LogEventLevel.Warning)
                .WriteTo.File(logFilePath)
                .WriteTo.Console(outputTemplate: outputTemplateContent, theme: themeType)
                .CreateLogger();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                    webBuilder.UseSerilog();
                });
    }
}
