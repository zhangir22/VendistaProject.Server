using VendistaProject.UI.Services.Interfaces;
using VendistaProject.Domain.Dto;
using VendistaProject.UI.Models;
using System.Net.Http;
using System.Threading;
using Newtonsoft.Json;
using VendistaProject.Domain.Dto.Models;
using Microsoft.AspNetCore.Authentication;

namespace VendistaProject.UI.Services
{ 
    public class BaseApiService: IBaseApiService
    {
        protected readonly string BaseUrl = AppConfiguration.ApiUrl;
        private static string nameMethod = "";
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
   
        
       
    }
    public class Token
    {
        public string token { get; set; }
    }
}
