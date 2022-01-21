using System.Reflection;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.SpaServices.AngularCli;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Music_Portal.Domain.Interfaces;
using Music_Portal.Infrastructure.Data;
using Music_Portal.Services.Services;
using Music_Portal.Services.Interfaces;

namespace Portal_Application;

public class Startup
{
    public Startup(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    private IConfiguration Configuration { get; }

    public void ConfigureServices(IServiceCollection services)
    {
        services.AddDbContext<Music_Portal.Domain.Core.ApplicationContext>(options => options
            .UseLazyLoadingProxies()
            .UseSqlite(Configuration.GetConnectionString("SQLiteConnection")));
        services.AddSpaStaticFiles(configuration => { configuration.RootPath = "ClientApp/dist"; });
        services.AddControllers();

        services.AddAutoMapper(Assembly.GetAssembly(typeof(Music_Portal.Services.Services.MappingProfile)));
        services.AddAutoMapper(Assembly.GetAssembly(typeof(MappingProfile)));

        services.AddSingleton<ILastFmService, LastFmService>();
        services.AddTransient<IArtistService, ArtistService>();
        services.AddTransient<IAlbumService, AlbumService>();
        services.AddTransient<ITrackService, TrackService>();
        services.AddTransient<IArtistRepository, ArtistRepository>();
        services.AddTransient<IAlbumRepository, AlbumRepository>();
        services.AddTransient<ITrackRepository, TrackRepository>();
        services.AddTransient<ISimilarArtistRepository, SimilarArtistRepository>();
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
        }
        else
        {
            app.UseExceptionHandler("/Error");
            app.UseHsts();
        }

        app.UseHttpsRedirection();
        app.UseStaticFiles();
        if (!env.IsDevelopment())
        {
            app.UseSpaStaticFiles();
        }

        app.UseRouting();

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllerRoute(
                name: "default",
                pattern: "{controller}/{action=Index}/{id?}");
        });

        app.UseSpa(spa =>
        {
            spa.Options.SourcePath = "ClientApp";

            if (env.IsDevelopment())
            {
                spa.UseAngularCliServer(npmScript: "start");
            }
        });
    }
}