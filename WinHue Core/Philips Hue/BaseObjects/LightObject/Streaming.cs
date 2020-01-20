using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace WinHue_Core.Philips_Hue.BaseObjects
{
    public partial class Streaming
    {
        [JsonPropertyName("renderer")]
        public bool Renderer { get; set; }

        [JsonPropertyName("proxy")]
        public bool Proxy { get; set; }
    }
}
