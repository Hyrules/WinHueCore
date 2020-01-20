using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace WinHue_Core.Philips_Hue.BaseObjects
{
    public partial class Startup
    {
        [JsonPropertyName("mode")]
        public string Mode { get; set; }

        [JsonPropertyName("configured")]
        public bool Configured { get; set; }
    }
}
