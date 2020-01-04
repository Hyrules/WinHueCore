using RestSharp;
using RestSharp.Deserializers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using System.Text;
using System.Text.Json;
using WinHue_Core.Philips_Hue.Messages.Success;
using System.Linq;
using System.Text.Json.Serialization;

namespace WinHue_Core.Philips_Hue.Messages
{
    public class HueResponse
    {
        public HueResponse()
        {
            Errors = new List<Error>();
            Success = new List<ISuccess>();
        }

        [DataMember(Name = "error")]
        public List<Error> Errors { get; set; }

        [DataMember(Name = "success")]
        public List<ISuccess> Success { get; set; }

        public int Count => Errors.Count + Success.Count;

        public bool hasErrors => Errors.Count > 0 ? true : false;

        public bool hasSuccess => Success.Count > 0 ? true : false;

        public bool hasMessages => hasErrors || hasSuccess;

        public static HueResponse Parse(string json)
        {
            HueResponse response = new HueResponse();
            string content = json;
            JsonDocument doc = JsonDocument.Parse(json);
            foreach(JsonElement e in doc.RootElement.EnumerateArray())
            {
                JsonElement element;
                if(e.TryGetProperty("error", out element))
                {
                    string value = element.GetRawText();
                    Error err = JsonSerializer.Deserialize<Error>(value, new JsonSerializerOptions() { IgnoreNullValues = true } );
                    response.Errors.Add(err);
                }
                else if (e.TryGetProperty("success", out element))
                {
                    string value = element.GetRawText();
                    if (value.Contains("deleted"))
                    {
                        DeleteSuccess delete = new DeleteSuccess(element.GetString());
                        response.Success.Add(delete);
                    }
                    else if(element.TryGetProperty("id",out JsonElement id))
                    {
                        PostSuccess post = new PostSuccess(id.GetString());
                        response.Success.Add(post);
                    }
                    else
                    {
                        string elstr = element.GetRawText();
                        elstr = elstr.Replace("{", "");
                        elstr = elstr.Replace("}", "");
                        elstr = elstr.Replace("\"", "");
                        string[] arr = elstr.Split(':');

                        if (arr.Length != 2) continue;
                        PutSuccess put = new PutSuccess(arr[0],arr[1]);
                        response.Success.Add(put);
                        
                    }

                }
                else
                {

                }
                
            }
            return response;

        }
    }
}
