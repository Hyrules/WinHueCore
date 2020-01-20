using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace WinHue_Core.Philips_Hue.BaseObjects
{
    public partial class LightConfig
    {
        [JsonPropertyName("archetype")]
        public string Archetype { get; set; }

        [JsonPropertyName("function")]
        public string Function { get; set; }

        [JsonPropertyName("direction")]
        public string Direction { get; set; }

        [JsonPropertyName("startup")]
        public Startup Startup { get; set; }
    }
}
