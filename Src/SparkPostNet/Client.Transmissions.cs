using Newtonsoft.Json;
using SparkPostNet.Models;
using SparkPostNet.ResponseModels;
using System.Net.Http;
using System.Threading.Tasks;

namespace SparkPostNet {
    public partial class Client {
        public async Task<TransmissionResponse> SendTransmission(Transmission trm) {
            string data = JsonConvert.SerializeObject(trm, _jsonSettings);
            return await this.MakeRequest<TransmissionResponse>("transmissions", HttpMethod.Post, data);
        }
    }
}
