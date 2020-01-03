using System;
using System.Collections.Generic;
using System.Text;

namespace WinHue_Core.Philips_Hue.Messages.Success
{
    public class DeleteSuccess : ISuccess
    {
        public DeleteSuccess(string message)
        {
            this.message = message;
        }

        public string message { get;}

        public override string ToString()
        {
            return message;
        }
    }
}
