using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using RestSharp;
using WinHue_Core.Philips_Hue.Comms;
using WinHue_Core.Philips_Hue.Messages;

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

        public async static Task<T> Get<T>(string url, int timeout = 5000)
        {
            IRestResponse<T> response = await SendRequestAsyncTask<T>(url, Method.GET, timeout);
            if (response.Data is null)
                throw new HueGetErrorException("Error in get method", response.Content);
            return response.Data;

        }

        public async static Task<HueResponse> Put(string url, int timeout = 5000)
        {
            IRestResponse response = await SendRequestAsyncTask(url, Method.PUT, timeout);
            return HueResponse.Parse(response.Content);
        }

        public async static Task<HueResponse> Post(string url, int timeout = 5000)
        {
            IRestResponse response = await SendRequestAsyncTask(url, Method.POST, timeout);
            return HueResponse.Parse(response.Content);
        }

        public async static Task<HueResponse> Delete(string url, int timeout = 5000)
        {
            IRestResponse response = await SendRequestAsyncTask(url, Method.DELETE, timeout);
            return HueResponse.Parse(response.Content);

        }

    }
}
