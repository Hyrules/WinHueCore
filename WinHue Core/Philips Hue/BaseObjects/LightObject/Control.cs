using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace WinHue_Core.Philips_Hue.BaseObjects
{
    public partial class Control
    {
        [JsonPropertyName("mindimlevel")]
        public long Mindimlevel { get; set; }

        [JsonPropertyName("maxlumen")]
        public long Maxlumen { get; set; }

        [JsonPropertyName("colorgamuttype")]
        public string Colorgamuttype { get; set; }

        [JsonPropertyName("colorgamut")]
        public List<List<double>> Colorgamut { get; set; }
    }
}
