using System;
using System.Collections.Generic;
using System.Text;

namespace WinHue_Core.Philips_Hue.Messages.Success
{

    public class PutSuccess : ISuccess
    {
        public PutSuccess(string resource, string value)
        {
            this.resource = resource;
            this.value = value;
        }
        string resource { get; }
        string value { get; }
    }
}
