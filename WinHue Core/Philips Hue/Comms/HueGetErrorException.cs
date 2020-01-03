using System;
using System.Collections.Generic;
using System.Text;
using RestSharp;
using RestSharp.Serialization.Json;
using WinHue_Core.Philips_Hue.Messages;

namespace WinHue_Core.Philips_Hue.Comms
{
    public class HueGetErrorException : Exception
    {
        public HueGetErrorException() : base()
        {
            Response = new HueResponse();
        }

        public HueGetErrorException(string message, string errors) : base(message)
        {
            Response = HueResponse.Parse(errors);
        }

        public HueGetErrorException(string message, string errors, Exception inner) : base(message,inner) 
        {
            Response = HueResponse.Parse(errors);
        }

        public HueResponse Response { get; }

    }
}
