using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;
using System.Text.Json.Serialization;
using System.Windows.Media;

namespace WinHue_Core.Philips_Hue
{
    public interface IHueObject
    {
        public string Name { get; set; }

        public string Id { get; set; }

        public ImageSource Image { get; set; }


    }
}
