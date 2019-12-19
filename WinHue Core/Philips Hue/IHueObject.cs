using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace WinHue_Core.Philips_Hue
{
    public interface IHueObject
    {
        [DataMember(Name = "name")]
        public string Name { get; set; }

    }
}
