using log4net.Appender;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;
using VendistaProject.Application.Services;
using VendistaProject.Application.Services.Interfaces;
using VendistaProject.Dto.Models;
using VendistaProject.Infrastructure.Migrations;
using VendistaProject.Infrastructure.Models;

namespace VendistaProject.Server.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class BaseController : ControllerBase
    {
        protected HttpClient _client = new HttpClient();
        protected readonly TokenInit tokens= new TokenInit();  
        protected readonly IHistoryService historyService;
        public BaseController(IHistoryService historyService) 
        {
            this.historyService = historyService;
        }
 


        #region ClientRequests
        [Route("api/SendCommandByClient/{idTerminal}/{content}")]
        [HttpPost]
        public async Task<CommandTerminal?> SendCommandByClient(int idTerminal, HttpContent content)
        { 
            HttpResponseMessage response = await _client.PostAsync("http://178.57.218.210:398/terminals/" + idTerminal + "/commands?token=" + tokens.GetToken(_client, TypeToken.Client).Result, content);
            CommandTerminal commandTerminal = JsonConvert.DeserializeObject<CommandTerminal>(await response.Content.ReadAsStringAsync());
            if (response.IsSuccessStatusCode)
            {
                await historyService.CreateAsync(
                new HistoryModel
                {
                    dataTime = DateTime.UtcNow,
                    param1 = commandTerminal.parameter1.ToString(),
                    param2 = commandTerminal.parameter2.ToString(),
                    param3 = commandTerminal.parameter3.ToString(),
                    status = commandTerminal.state_name

                });
                return commandTerminal;
            }
            return null;
        }
        [Route("api/GetTerminalsByClient")]
        [HttpGet]
        public async Task<Terminal> GetTerminalByClient()
        {
            HttpResponseMessage response = await _client.GetAsync("http://178.57.218.210:398/terminals?token=" + tokens.GetToken(_client, TypeToken.Client).Result);
            return JsonConvert.DeserializeObject<Terminal>(await response.Content.ReadAsStringAsync());
        }
        [Route("api/GetCommandByClient")]
        [HttpGet]
        public async Task<Command> GetCommandByClient()
        {
            HttpResponseMessage response = await _client.GetAsync("http://178.57.218.210:398/commands/types?token=" + tokens.GetToken(_client, TypeToken.Client).Result);
            return JsonConvert.DeserializeObject<Command>(await response.Content.ReadAsStringAsync());
        }
        #endregion
        #region PartnerRequests

        [Route("api/SendCommandByPartner/{idTerminal}/{token}")]
        [HttpPost]
        public async Task<CommandTerminal> SendCommandByPartner(int idTerminal, MultyCommand multyCommand)
        {
            HttpContent content = new StringContent(JsonConvert.SerializeObject(multyCommand), Encoding.UTF8, "application/json");
            HttpResponseMessage response = await _client.PostAsync("http://178.57.218.210:398/terminals/" + idTerminal + "/commands?token=" + tokens.GetToken(_client, TypeToken.Partner).Result, content);
            CommandTerminal commandTerminal = JsonConvert.DeserializeObject<CommandTerminal>(await response.Content.ReadAsStringAsync());
            if (response.IsSuccessStatusCode)
            {
                await historyService.CreateAsync(
                new HistoryModel
                {
                    dataTime = DateTime.UtcNow,
                    param1 = commandTerminal.parameter1.ToString(),
                    param2 = commandTerminal.parameter2.ToString(),
                    param3 = commandTerminal.parameter3.ToString(),
                    status = commandTerminal.state_name

                });
                return commandTerminal;
            }
            return null;
        }

        [Route("api/GetTerminalsByPartner")]
        [HttpGet]
        public async Task<Terminal> GetTerminalByPartner()
        {
            HttpResponseMessage response = await _client.GetAsync("http://178.57.218.210:398/terminals?token=" + tokens.GetToken(_client, TypeToken.Partner).Result);
            return JsonConvert.DeserializeObject<Terminal>(await response.Content.ReadAsStringAsync());
        }
        [Route("api/GetCommandByPartner")]
        [HttpGet]
        public async Task<Command> GetCommandByPartner()
        {
            HttpResponseMessage response = await _client.GetAsync("http://178.57.218.210:398/commands/types?token=" +  tokens.GetToken(_client, TypeToken.Partner).Result);
            return JsonConvert.DeserializeObject<Command>(await response.Content.ReadAsStringAsync());
        }
        #endregion

    }
   
    public enum TypeToken
    {
        Partner = 0,
        Client
    }
    
    public class Token
    {
        public string token { get; set; }
    }
    public class  TokenInit
    {
 
        private readonly string __logP = "user2";
        private readonly string __pasP = "password2";

        private readonly string __logC = "user1";
        private readonly string __pasC = "password1";
        private readonly string __baseUrl = "http://178.57.218.210:398/token?";
        public async Task<string?> GetToken(HttpClient client, TypeToken type)
        {
            HttpResponseMessage response;
            Token token;
            switch (type)
            {
                case TypeToken.Client:
                    response = await client.GetAsync(__baseUrl + $"login={__logC}&password={__pasC}");
                    token = JsonConvert.DeserializeObject<Token>(await response.Content.ReadAsStringAsync());
                    return token.token;
                case TypeToken.Partner:
                    response = await client.GetAsync(__baseUrl + $"login={__logP}&password={__pasP}");
                    token = JsonConvert.DeserializeObject<Token>(await response.Content.ReadAsStringAsync());
                    return token.token;
                default:
                    return null;
            }
        }
   
    }
}
