﻿using System.Collections.Generic;

namespace Interfaces.Models
{
    public class ArtistLastFm
    {
        public string Name { get; set; }
        public int Playcount { get; set; }
        public int Listeners { get; set; }
        public string Url { get; set; }
        public List<ImageLastFm> Image { get; set; }
    }
}