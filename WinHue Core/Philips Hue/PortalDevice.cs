using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace WinHue_Core.Philips_Hue
{
    public class PortalDevice
    {
        [DataMember(Name="id")]
        public string Id { get; set; }
        [DataMember(Name = "internalipaddress")]
        public string InternalIpAddress { get; set; }
    }
}
