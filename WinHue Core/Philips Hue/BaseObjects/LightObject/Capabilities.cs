using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace WinHue_Core.Philips_Hue.BaseObjects
{
    public partial class Capabilities
    {
        [JsonPropertyName("certified")]
        public bool Certified { get; set; }

        [JsonPropertyName("control")]
        public Control Control { get; set; }

        [JsonPropertyName("streaming")]
        public Streaming Streaming { get; set; }
    }
}
