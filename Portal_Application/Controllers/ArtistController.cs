using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using AutoMapper;
using Core;
using Interfaces.Models;
using Services;

namespace Portal_Application.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ArtistController : ControllerBase
    {
        //private readonly ApplicationContext _db;

        // public ArtistController(ApplicationContext context)
        // {
        //     _db = context;
        //     
        //     if (!_db.Artists.Any())
        //     {
        //         foreach (var artist in _artists)
        //         {
        //             _db.Artists.Add(new Artist
        //             {
        //                 Name = artist.Name,
        //                 Url = artist.Url,
        //                 Playcount = artist.Playcount,
        //                 Listeners = artist.Listeners
        //             });
        //         }
        //         _db.SaveChanges();
        //     }
        //     else
        //     {
        //         foreach (var artist in _artists)
        //         {
        //             var curArtist = _db.Artists.First(a => a.Name == artist.Name);
        //             curArtist.Playcount = artist.Playcount;
        //             curArtist.Listeners = artist.Listeners;
        //         }
        //     
        //         _db.SaveChanges();
        //     }
        // }

        [HttpGet("top_artists")]
        public List<Artist> Get()
        {
            ApiRequest lastFmRequest = new ApiRequest();
            
            var config = new MapperConfiguration(cfg => cfg.CreateMap<ArtistLastFm, Artist>());
            var mapper = new Mapper(config);
            var users = mapper.Map<List<Artist>>(lastFmRequest.GetTopArtists());
            
            return users;
        }
    }
}