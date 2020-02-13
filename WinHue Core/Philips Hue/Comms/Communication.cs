using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections;
using RestSharp;
using WinHue_Core.Philips_Hue.Comms;
using WinHue_Core.Philips_Hue.Messages;
using WinHue_Core.Utils;

namespace WinHue_Core.Philips_Hue.Comms
{
    public static class Communication
    {
        private static RestClient _client;

        static Communication()
        {
            _client = new RestClient();

            //*** REMOVE HANDLERS TO AVOID DESERIALIZING ANYTHING OTHER THAN JSON OTHERWISE IT WILL STILL RETURN AN OBJECT
            _client.RemoveHandler("text/html");
            _client.RemoveHandler("text/xml");
            _client.RemoveHandler("*+xml");
            _client.RemoveHandler("application/xml");
            _client.RemoveHandler("*");
            
        }

        public static async Task<IRestResponse> SendRequest(string url, Method requesttype, int timeout = 5000)
        {
            RestRequest request = new RestRequest(url, requesttype)
            {
                RequestFormat = DataFormat.Json,
                Timeout = timeout
            };

            request.AddHeader("Content-Type", "application/json");
            IRestResponse response = await _client.ExecuteTaskAsync(request);
            return response;
        }

        public static async Task<IRestResponse<T>> SendRequestAsyncTask<T>(string url, Method requesttype, int timeout = 5000, CancellationToken token = new CancellationToken()) 
        {
            RestRequest request = new RestRequest(url, requesttype)
            {
                RequestFormat = DataFormat.Json,
                Timeout = timeout
            };

            request.AddHeader("Content-Type", "application/json");
            IRestResponse<T> result = await _client.ExecuteTaskAsync<T>(request,token);

            return result;
        }

        public static async Task<IRestResponse> SendRequestAsyncTask(string url, Method requesttype, int timeout = 5000, CancellationToken token = new CancellationToken())
        {
            RestRequest request = new RestRequest(url, requesttype)
            {
                RequestFormat = DataFormat.Json,
                Timeout = timeout
            };

            request.AddHeader("Content-Type", "application/json");
            IRestResponse result = await _client.ExecuteTaskAsync(request, token);

            return result;
        }

        public static IRestResponse<T> SendRequest<T>(string url, Method requesttype, int timeout = 5000) where T: new()
        {
            RestRequest request = new RestRequest(url, requesttype)
            {
                RequestFormat = DataFormat.Json,
                Timeout = timeout
            };

            request.AddHeader("Content-Type", "application/json");
            IRestResponse<T> result = _client.Execute<T>(request);
            
            return result;

        }

        public async static Task<HueObject> GetObject(string url, int timeout = 5000)
        {
            IRestResponse response = await SendRequestAsyncTask(url, Method.GET, timeout);
            try
            {
                HueObject result = JsonConvert.DeserializeObject<HueObject>(response.Content);
                    //JObject.Parse(response.Content);

                return result;
            }
            catch(Exception e)
            {
                throw new HueGetErrorException("Error parsing object. Most likely received an error message from the bridge.", response.Content, e.InnerException);
            }
            

        }

        public async static Task<HueResponse> PutObject(string url, int timeout = 5000)
        {
            IRestResponse response = await SendRequestAsyncTask(url, Method.PUT, timeout);
            return HueResponse.Parse(response.Content);
        }

        public async static Task<HueResponse> PostObject(string url, int timeout = 5000)
        {
            IRestResponse response = await SendRequestAsyncTask(url, Method.POST, timeout);
            return HueResponse.Parse(response.Content);
        }

        public async static Task<HueResponse> DeleteObject(string url, int timeout = 5000)
        {
            IRestResponse response = await SendRequestAsyncTask(url, Method.DELETE, timeout);
            return HueResponse.Parse(response.Content);

        }

        public async static Task<List<dynamic>> GetListObjects(string url, int timeout = 5000)
        {
            IRestResponse response = await SendRequestAsyncTask(url, Method.GET, timeout);
            try
            {
                Dictionary<string,HueObject> results = JsonConvert.DeserializeObject<Dictionary<string,HueObject>>(response.Content);
                List<dynamic> listobj = new List<dynamic>();
                foreach(KeyValuePair<string,HueObject> o in results)
                {
                    HueObject u = o.Value;
                    u.Id = o.Key;
                    listobj.Add(u);
                }
                return listobj;
            }
            catch(Exception e)
            {
                throw new HueGetErrorException("Error parsing object. Most likely received an error message from the bridge.", response.Content, e.InnerException);
            }
        }

    }
}
