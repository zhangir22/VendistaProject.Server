using VendistaProject.UI.Services.Interfaces;
using VendistaProject.Domain.Dto;
using VendistaProject.UI.Models;
using System.Net.Http;
using System.Threading;
using Newtonsoft.Json;

namespace VendistaProject.UI.Services
{
    public enum ApiQueryType
    {
        GET,
        POST,
    }
    public class BaseApiService: IBaseApiService
    {
        private readonly HttpClient _client;
        public BaseApiService()
        {
            _client = PreparedClient();
        }
        
        public static HttpClient PreparedClient()
        {
            HttpClientHandler handler = new HttpClientHandler();
            handler.ServerCertificateCustomValidationCallback += (sender, cert, chain, sslPolicyErrors) => { return true; };

            HttpClient client = new HttpClient(handler);
            return client;
        } 
   
        public async Task<Token?>GetToken(string path)
        {
            HttpResponseMessage response = await _client.GetAsync(path);
            if (response.IsSuccessStatusCode)
            {
                Token token = JsonConvert.DeserializeObject<Token>(await response.Content.ReadAsStringAsync());
                if(string.IsNullOrEmpty(token.token))
                {
                    return token;
                }
            }
            return null;
        }
    }
    public class Token
    {
        public string token { get; set; }
    }
}
