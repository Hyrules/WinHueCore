using System;
using System.Collections.Generic;
using System.Text;

namespace WinHue_Core.Philips_Hue.Messages.Success
{
    public class PostSuccess : ISuccess
    {
        public PostSuccess(string id)
        {
            this.id = id;
        }
        string id { get; }

        public override string ToString()
        {
            return id;
        }
    }
}
