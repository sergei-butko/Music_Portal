using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Music_Portal.Domain.Interfaces;
using Music_Portal.Infrastructure.Data;

namespace Music_Portal.MusicSearcher;

public class Program
{
    private static void Main(string[] args)
    {
        var serviceProvider = ServiceRegistrationExtension.RegisterServices();
        var searcherService = serviceProvider.GetRequiredService<ISearcher>();

        var folderPath = args.Any() ? args[0] : throw new ArgumentException();
        const string fileExtension = "*.mp3";

        var songList = searcherService.FindSongs(folderPath, fileExtension);
        searcherService.SetSongPaths(songList);
    }
}

public static class ServiceRegistrationExtension
{
    private static readonly IConfigurationRoot Configuration = new ConfigurationBuilder()
        .AddJsonFile("appsettings.json")
        .Build();

    public static ServiceProvider RegisterServices()
    {
        return new ServiceCollection()
            .AddSerilogServices()
            .AddSingleton<IArtistRepository, ArtistRepository>()
            .AddSingleton<ITrackRepository, TrackRepository>()
            .AddSingleton<ISearcher, Searcher>()
            .AddDbContext<Domain.Core.ApplicationContext>(options => options
                .UseLazyLoadingProxies()
                .UseSqlite(Configuration.GetConnectionString("SQLiteConnection")))
            // .UseSqlServer(Configuration.GetConnectionString("DefaultConnection")))
            .BuildServiceProvider();
    }
}