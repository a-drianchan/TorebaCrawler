using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace TorebaCrawlerCore
{
    public class Replay
    {
        [JsonProperty(PropertyName = "ReplayID")]
        [Display(Name = "Replay ID")]
        public int ReplayID { get; set; } = -1;

        [JsonProperty(PropertyName = "MachineID")]
        [Display(Name = "Machine ID")]
        public int MachineID { get; set; }

        [JsonProperty(PropertyName = "ReplayLink")]
        [Display(Name = "Replay Link")]
        public string ReplayLink { get; set; }

        [JsonProperty(PropertyName = "PrizeName")]
        [Display(Name = "Prize Name")]
        public string PrizeName { get; set; }

        [JsonProperty(PropertyName = "Time")]
        [Display(Name = "Win Time")]
        public DateTime Time { get; set; }

    }
}
