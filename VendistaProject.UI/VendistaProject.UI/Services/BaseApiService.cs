using VendistaProject.UI.Services.Interfaces;
using VendistaProject.Domain.Dto;
using VendistaProject.UI.Models;
using System.Net.Http;
using System.Threading;
using Newtonsoft.Json;
using VendistaProject.Domain.Dto.Models;
using Microsoft.AspNetCore.Authentication;
using System.Text;
using VendistaProject.Domain.Dto.Models.Interfaces;
using System.Collections.Generic;

namespace VendistaProject.UI.Services
{ 
    public enum TypeAuth
    {
        Client,
        Partner
    }
    public class BaseApiService : IBaseApiService
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
        public async Task<IEnumerable<IHistoryModel>?> GetHistories()
        {
            HttpResponseMessage response = await _client.GetAsync(AppConfiguration.ApiUrl + $@"/api/GetHistories");
            
            IEnumerable<IHistoryModel> histories = JsonConvert.DeserializeObject<IEnumerable<IHistoryModel>>( await response.Content.ReadAsStringAsync());
            return histories;
        }
        public async Task<CommandTerminal?>SendCommand(TypeAuth type, int idTerminal, MultyCommand command)
        {
            HttpResponseMessage response = new HttpResponseMessage();
            HttpContent content = new StringContent(JsonConvert.SerializeObject(command), Encoding.UTF8, "application/json");
            switch (type)
            {
                case TypeAuth.Client:
                    response = await _client.PostAsync(AppConfiguration.ApiUrl + @$"/api/SendCommandByClient/{idTerminal}/",content);
                    if (response.IsSuccessStatusCode)
                    {
                        return JsonConvert.DeserializeObject<CommandTerminal>(await response.Content.ReadAsStringAsync());
                    }
                    return null;
                case TypeAuth.Partner:
                    response = await _client.PostAsync(AppConfiguration.ApiUrl + $@"/api/SendCommandByClient/{idTerminal}/", content);
                    if (response.IsSuccessStatusCode)
                    {
                        return JsonConvert.DeserializeObject<CommandTerminal>(await response.Content.ReadAsStringAsync());
                    }
                    return null;
                default:
                    return null;
            }
        }
        public async Task<Terminal?>GetTerminal(TypeAuth type)
        {
            HttpResponseMessage response = new HttpResponseMessage();
            switch (type)
            {
                case TypeAuth.Client:
                    response = await _client.GetAsync(AppConfiguration.ApiUrl + @"/api/GetTerminalsByClient");
                    return JsonConvert.DeserializeObject<Terminal>(await response.Content.ReadAsStringAsync());
                    case TypeAuth.Partner:
                    response = await _client.GetAsync(AppConfiguration.ApiUrl + @"/api/GetTerminalsByPartner");
                    return JsonConvert.DeserializeObject<Terminal>(await response.Content.ReadAsStringAsync());
                    default:
                    return null;
            }
        }
        public async Task<Command?>GetCommand(TypeAuth type)
        {
            HttpResponseMessage response = new HttpResponseMessage(); 
            switch (type)
            {
                case TypeAuth.Client:
                    response = await _client.GetAsync(AppConfiguration.ApiUrl + @"/api/GetCommandByClient");
                    return JsonConvert.DeserializeObject<Command>(await response.Content.ReadAsStringAsync());
                    case TypeAuth.Partner:
                    response = await _client.GetAsync(AppConfiguration.ApiUrl + @"/api/GetCommandByPartner");
                    return JsonConvert.DeserializeObject<Command>(await response.Content.ReadAsStringAsync());
                default:
                    return null;
            }
        }
       

       
    }
    public class Token
    {
        public string token { get; set; }
    }
}
