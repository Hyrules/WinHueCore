using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace WinHue_Core.Philips_Hue.BaseObjects
{
    public partial class Swupdate
    {
        [JsonPropertyName("state")]
        public string State { get; set; }

        [JsonPropertyName("lastinstall")]
        public DateTimeOffset Lastinstall { get; set; }
    }
}
