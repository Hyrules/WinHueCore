using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace WinHue_Core.Philips_Hue.Messages
{
    public class Error
    {

        [JsonPropertyName("type")]
        public uint Type { get; set; }
        [JsonPropertyName("address")]
        public string Address { get; set; }
        [JsonPropertyName("description")]
        public string description { get; set; }
    }
}
