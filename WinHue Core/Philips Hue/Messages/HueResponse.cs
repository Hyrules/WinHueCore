using RestSharp;
using RestSharp.Deserializers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using System.Text;
using System.Text.Json;
using WinHue_Core.Philips_Hue.Messages.Success;

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
                        PutSuccess put = new PutSuccess(element[0].GetString(),element[1].GetString());
                        response.Success.Add(put);
                        // *test
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
