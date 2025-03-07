using Application.Services.Common.Interfaces;
using Newtonsoft.Json.Linq;

namespace Application.Services.Common
{
    public class ApiService : IApiService
    {
        private const string priceApiUrl = "https://api.coingecko.com/api/v3/simple/price?ids=ethereum&vs_currencies=brl";

        public async Task<decimal> GetCurrentEthPrice()
        {
            decimal ethPrice;

            using HttpClient client = new();

            try
            {
                HttpResponseMessage response = await client.GetAsync(priceApiUrl);

                response.EnsureSuccessStatusCode();

                var responseBody = await response.Content.ReadAsStringAsync();

                JObject json = JObject.Parse(responseBody);

                ethPrice = json["ethereum"]["brl"].Value<decimal>();
            }
            catch (HttpRequestException)
            {
                throw new Exception("Falha ao conectar com a API.");
            }

            return ethPrice;
        }
    }
}
