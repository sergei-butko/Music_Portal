using Microsoft.Extensions.DependencyInjection;
using Serilog;
using SerilogLogger = Serilog.Core.Logger;

namespace Music_Portal.MusicSearcher;

public static class LoggerConfig
{
    public static IServiceCollection AddSerilogServices(this IServiceCollection services)
    {
        Log.Logger = new LoggerConfiguration()
            .WriteTo.Console(outputTemplate: "[{Timestamp:HH:mm:ss} - {Level:u3}] - {Message:lj}{NewLine}")
            .CreateLogger();
        AppDomain.CurrentDomain.ProcessExit += (s, e) => Log.CloseAndFlush();
        return services.AddSingleton(Log.Logger);
    }
}