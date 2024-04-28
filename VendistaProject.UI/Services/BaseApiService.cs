using Newtonsoft.Json;
using System.Text;
using VendistaProject.Dto.Models;
using VendistaProject.Dto.Models.Interfaces;
using VendistaProject.UI.Models;
using VendistaProject.UI.Services.Interfaces;

namespace VendistaProject.UI.Services
{
   
        public enum TypeAuth
        {
            Client,
            Partner
        }

        
        public class BaseApiService : IApiService
        {
            private readonly HttpClient _client;
            public BaseApiService()
            {
                _client = PreparedClient();
            }

            public static HttpClient PreparedClient()
            {

                HttpClient client = new HttpClient();
                return client;
            }
            public async Task<IEnumerable<IHistoryModel>?> GetHistories()
            {
                HttpResponseMessage response = await _client.GetAsync(AppConfiguration.ApiUrl + $@"/api/GetHistories");

                IEnumerable<HistoryDto> histories = JsonConvert.DeserializeObject<IEnumerable<HistoryDto>>(await response.Content.ReadAsStringAsync());
                return histories;
            }
            public async Task<CommandTerminal?> SendCommand(TypeAuth type, int idTerminal, MultyCommand command,string lastcommand)
            {
                TerminalInfo terminalInfo = new TerminalInfo(idTerminal,command, lastcommand);
                HttpResponseMessage response = new HttpResponseMessage();
                string content = JsonConvert.SerializeObject(terminalInfo);
                switch (type)
                {
                    case TypeAuth.Client: 
                        response = await _client.GetAsync(AppConfiguration.ApiUrl + @$"/api/SendCommandByClient/{content}");
                    break;
                    case TypeAuth.Partner:
                        response = await _client.GetAsync(AppConfiguration.ApiUrl + $@"/api/SendCommandByPartner/{content}");
                    break; 
                    default:break;
                }
                CommandTerminal commandTerminal = JsonConvert.DeserializeObject<CommandTerminal>(await response.Content.ReadAsStringAsync());
            
                return commandTerminal;
            }
            public async Task<Terminal?> GetTerminal(TypeAuth type)
            {
                HttpResponseMessage response = new HttpResponseMessage();
                switch (type)
                {
                    case TypeAuth.Client:
                        response = await _client.GetAsync(AppConfiguration.ApiUrl + @"/api/GetTerminalsByClient");
                    break;
                    case TypeAuth.Partner:
                        response = await _client.GetAsync(AppConfiguration.ApiUrl + @"/api/GetTerminalsByPartner");
                    break;
                    default:break;
                }
                if (response.IsSuccessStatusCode)
                {
                    return JsonConvert.DeserializeObject<Terminal>(await response.Content.ReadAsStringAsync());
                }
            return null;
            }
            public async Task<Command?> GetCommand(TypeAuth type)
            {
                HttpResponseMessage response = new HttpResponseMessage();
                switch (type)
                {
                    case TypeAuth.Client:
                        response = await _client.GetAsync(AppConfiguration.ApiUrl + @"/api/GetCommandByClient");
                    break;
                    case TypeAuth.Partner:
                        response = await _client.GetAsync(AppConfiguration.ApiUrl + @"/api/GetCommandByPartner");
                    break;
                    default:
                        break;
                }
                if (response.IsSuccessStatusCode)
                {

                    return JsonConvert.DeserializeObject<Command>(await response.Content.ReadAsStringAsync());
                }
            return null;
            }



        }
        public class Token
        {
            public string token { get; set; }
        }
     
}
