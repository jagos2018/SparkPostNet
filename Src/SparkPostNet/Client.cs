using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Serialization;
using SparkPostNet.Exceptions;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace SparkPostNet {
    public partial class Client : IDisposable {
        private string _baseAddress = "https://api.sparkpost.com/api/v1/";
        private string _apiKey = "";
        private TimeSpan _timeout = TimeSpan.FromMilliseconds(1000 * 60 * 5); // 5 minutes
        private JsonSerializerSettings _jsonSettings = null;

        private HttpClient _client = null;

        public Client(string apiKey) : this(apiKey, null, 0) { }

        public Client(string apiKey, string baseAddress, int timeoutMilliseconds) {
            _apiKey = apiKey ?? _apiKey;
            _baseAddress = baseAddress ?? _baseAddress;
            _timeout = (timeoutMilliseconds > 0) ? TimeSpan.FromMilliseconds(timeoutMilliseconds) : _timeout;

            _jsonSettings = new JsonSerializerSettings() {
                ContractResolver = new DefaultContractResolver() {
                    NamingStrategy = new SnakeCaseNamingStrategy()
                }
            };

            _client = GetHttpClient();
        }

        public void Dispose() {
            if (_client != null) {
                _client.Dispose();
            }
        }

        private async Task<T> MakeRequest<T>(string action, HttpMethod httpMethod, string data = null) {
            var req = new HttpRequestMessage(httpMethod, action);
            if(!String.IsNullOrEmpty(data)) {
                req.Content = new StringContent(data);
            }
            HttpResponseMessage resp = _client.SendAsync(req).GetAwaiter().GetResult();
            string respString = await resp.Content.ReadAsStringAsync();
            if (resp.IsSuccessStatusCode) {
                string resultsObj = JObject.Parse(respString).GetValue("results").ToString();
                return JsonConvert.DeserializeObject<T>(resultsObj, _jsonSettings);
            }
            throw new SparkPostNetException($"SparkPost API responded with an error. Json response: {respString}");
        }

        private HttpClient GetHttpClient() {
            var client = new HttpClient();
            client.BaseAddress = new Uri(_baseAddress);
            client.Timeout = _timeout;
            client.DefaultRequestHeaders.Add("Authorization", _apiKey);
            client.DefaultRequestHeaders.Add("ContentType", "application/json");

            return client;
        }
    }
}
