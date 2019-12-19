using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using RestSharp;

namespace WinHue_Core.Philips_Hue
{
    public static class Communication
    {
        private static RestClient _client;

        static Communication()
        {
            _client = new RestClient();
        }

        public static async Task<T> SendRequestAsyncTask<T>(string url, Method requesttype) where T : IHueObject
        {
            RestRequest request = new RestRequest(url,requesttype);
            request.AddHeader("Content-Type", "application/json");

            IRestResponse<T> result = await _client.ExecuteTaskAsync<T>(request);

            return result.Data;
        }

    }
}
