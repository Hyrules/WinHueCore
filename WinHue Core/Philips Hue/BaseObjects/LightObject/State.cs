using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;
using WinHue_Core.MVVM;

namespace WinHue_Core.Philips_Hue.BaseObjects
{
    public partial class State : ViewModelBase
    {
        private bool on;
        private long bri;
        private long hue;
        private long sat;
        private string effect;
        private List<double> xy;
        private string alert;
        private string colormode;
        private string mode;
        private bool reachable;
        private bool ct;

        [JsonPropertyName("on")]
        public bool On { get => on; set => SetProperty(ref on,value); }

        [JsonPropertyName("bri")]
        public long Bri { get => bri; set => bri = value; }

        [JsonPropertyName("hue")]
        public long Hue { get => hue; set => hue = value; }

        [JsonPropertyName("sat")]
        public long Sat { get => sat; set => sat = value; }

        [JsonPropertyName("effect")]
        public string Effect { get => effect; set => effect = value; }

        [JsonPropertyName("xy")]
        public List<double> Xy { get => xy; set => xy = value; }

        [JsonPropertyName("alert")]
        public string Alert { get => alert; set => alert = value; }

        [JsonPropertyName("colormode")]
        public string Colormode { get => colormode; set => colormode = value; }

        [JsonPropertyName("mode")]
        public string Mode { get => mode; set => mode = value; }

        [JsonPropertyName("reachable")]
        public bool Reachable { get => reachable; set => reachable = value; }

        [JsonPropertyName("ct")]
        public bool Ct { get => ct; set => ct = value; }



    }
}
