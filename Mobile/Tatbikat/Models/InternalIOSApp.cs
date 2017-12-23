using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Tatbikat.Models
{
    public class InternalIOSApp
    {
        [JsonProperty("resultCount")]
        public long ResultCount { get; set; }

        [JsonProperty("results")]
        public Result[] Results { get; set; }
    }
    public partial class Result
    {
        public static explicit operator TatbikatApp(Result b)  // explicit byte to digit conversion operator
        {
            return new TatbikatApp();
        }
        [JsonProperty("artworkUrl100")]
       // public string ArtworkUrl100 { get; set; }
        private string _artworkUrl100;
        public string ArtworkUrl100
        {
            get
            {
                if (_artworkUrl100 != null)
                {
                    _artworkUrl100.Replace(".jpg", ".png");
                }
                return _artworkUrl100;
            }
            set
            {
                _artworkUrl100 = value; 
            }
        }
        [JsonProperty("trackCensoredName")]
        public string TrackCensoredName { get; set; }
     
        [JsonProperty("trackViewUrl")]
        public string TrackViewUrl { get; set; }
      
        [JsonProperty("description")]
        public string Description { get; set; } 
    }
}
