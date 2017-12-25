using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Tatbikat.Models
{
    public class InternalIOSApp : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        [JsonProperty("resultCount")]
        public long ResultCount { get; set; }

        //[JsonProperty("results")]
        //public Result[] Results { get; set; }
        [JsonProperty("results")] 
        private Result[] _result;
        public Result[] Result
        {
            get
            { 
                return _result;
            }
            set
            {
                Result = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Result)));
            }
        }
    }
    public partial class Result : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
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
                
                return _artworkUrl100;
            }
            set
            {
                _artworkUrl100 = value;
                if (_artworkUrl100 != null)
                {
                    _artworkUrl100 = _artworkUrl100.Replace(".jpg", ".png");
                }
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(ArtworkUrl100));
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
