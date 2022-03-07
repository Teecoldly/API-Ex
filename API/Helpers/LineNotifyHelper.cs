using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;


namespace API.Helpers
{
    public static class LineNotifyHelper
    {
        private static void PostLine(string version, string baseUrl, string payload, HttpClient client, bool isFullError = false)
        {
            var message = string.Empty;
            var machineName = Environment.MachineName;
            if (!string.IsNullOrEmpty(payload))
                message = string.Format("Name: {0} Version: {1}, Message: {2}", machineName, version, payload);
            else
                message = string.Format("Name: {0} Version: {1}", machineName, version);

            try
            {
                if (isFullError)
                {
                    var messages = message.Split((char)1000);
                    foreach (var msg in messages)
                    {
                        var pairs = new List<KeyValuePair<string, string>>
                    {
                        new KeyValuePair<string, string>("message", msg)
                    };
                        var content = new FormUrlEncodedContent(pairs);
                        var response = client.PostAsync("", content).Result;
                    }
                }
                else
                {
                    var pairs = new List<KeyValuePair<string, string>>
                    {
                        new KeyValuePair<string, string>("message", message)
                    };
                    var content = new FormUrlEncodedContent(pairs);
                    var response = client.PostAsync("", content).Result;
                }
            }
            catch (Exception)
            {
            }
        }

        public static void SendWait(string message, bool isFullError = false)
        {
            var client = new HttpClient
            {
                BaseAddress = new Uri("https://notify-api.line.me/api/notify")
            };
            client.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", "nQ8j4C1PjHp6rGSzrHBnZpqe8SgIFsRi88URPC0HTxO");

            Task.Run(() =>
            {
                PostLine("1", "https://notify-api.line.me/api/notify", message, client, isFullError);
            });
        }
    }

}
