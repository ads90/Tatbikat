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

        public static explicit operator List<TatbikatApp>(InternalIOSApp b)
        {
            if (b.ResultCount == 0)
            {
                return null;
            }
            List<TatbikatApp> apps = new List<TatbikatApp>();
            foreach (var app in b.Results)
            {
                apps.Add(new TatbikatApp()
                {
                    ImageSource = app.AppIcon,
                    IDForIOSApp = app.IDForIOSApp,
                    IOSStoreLink = app.AppUrl,
                    Name = app.AppName,
                    AppDescription=app.AppDescription
                });

            }
            return apps;
        }
    }
    public partial class Result
    {
        [JsonProperty("trackId")]
        public int IDForIOSApp { get; set; }
        [JsonProperty("artworkUrl100")]
        // public string ArtworkUrl100 { get; set; }
        private string _AppIcon;
        public string AppIcon
        {
            get
            {
                if (_AppIcon != null)
                {
                    _AppIcon.Replace(".jpg", ".png");
                }
                return _AppIcon;
            }
            set
            {
                _AppIcon = value;
            }
        }
        [JsonProperty("trackCensoredName")]
        public string AppName { get; set; }

        [JsonProperty("trackViewUrl")]
        public string AppUrl { get; set; }

        [JsonProperty("description")]
        public string AppDescription { get; set; }
    }
}
