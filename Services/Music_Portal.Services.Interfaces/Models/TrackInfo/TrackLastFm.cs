﻿namespace Music_Portal.Services.Interfaces.Models.TrackInfo
{
    public class TrackLastFm
    {
        public string Name { get; set; }
        public int Listeners { get; set; }
        public int Playcount { get; set; }
        public TrackWikiLastFm Wiki { get; set; }
    }
}