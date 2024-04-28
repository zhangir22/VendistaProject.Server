using log4net.Appender;
using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Writers;
using Newtonsoft.Json;
using System.Text;
using VendistaProject.Application.Services;
using VendistaProject.Application.Services.Interfaces;
using VendistaProject.Dto.Models;
using VendistaProject.Dto.Models.Interfaces;
using VendistaProject.Infrastructure.Migrations;
using VendistaProject.Infrastructure.Models;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;
using Command = VendistaProject.Dto.Models.Command;

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

        [Route("api/Delete/{id}")]
        [HttpGet]
        public async Task<IHistoryModel> Delete(int id)
        {
            return await historyService.DeleteAsync(id);
        }
        [Route("api/GetBodyCommandByIndexPartner/{index}")]
        [HttpGet]
        public async Task<string> GetBodyCommandByIndexPartner(int index)
        {
            return JsonConvert.SerializeObject(GetCommandByPartner().Result.items[index]);
        }
        [Route("api/GetBodyCommandByIndexClient/{index}")]
        [HttpGet]
        public async Task<string> GetBodyCommandByIndexClient(int index)
        {
            return JsonConvert.SerializeObject(GetCommandByClient().Result.items[index]);
        }
        [Route("api/GetHistories")]
        [HttpGet]
        public async Task<IEnumerable<IHistoryModel>?> GetHistories()
        {
            return await historyService.GetAllAsync();
        }
        #region ClientRequests
        [Route("api/SendCommandByClient/{idTerminal}")]
        [HttpGet]
        public async Task<CommandTerminal?> SendCommandByClient(int idTerminal)
        { 
            HttpResponseMessage response = await _client.GetAsync("http://178.57.218.210:398/terminals/" + idTerminal + "/commands?token=" + tokens.GetToken(_client, TypeToken.Client).Result);
            CommandTerminal commandTerminal = JsonConvert.DeserializeObject<CommandTerminal>(await response.Content.ReadAsStringAsync());
            if (response.IsSuccessStatusCode)
            {
                HistoryDto history = new HistoryDto
                {
                    id = historyService.GetAllAsync().Result.LastOrDefault().id + 1,
                    dataTime = DateTime.UtcNow,
                    param1 = commandTerminal.parameter1.ToString(),
                    param2 = commandTerminal.parameter2.ToString(),
                    param3 = commandTerminal.parameter3.ToString(),
                    status = commandTerminal.state_name

                };
                var result = await historyService.CreateAsync(history);
                
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
        [Route("api/SendCommandByPartner/{content}")]
        [HttpGet]
        public async Task<CommandTerminal> SendCommandByPartner(string content)
        {

            TerminalInfo commandTerminal = JsonConvert.DeserializeObject<TerminalInfo>(content);
            var httpContent = new StringContent(JsonConvert.SerializeObject(commandTerminal.command), Encoding.UTF8, "application/json");
            CommandTerminal command = null;
            var url = $"http://178.57.218.210:398/terminals/{commandTerminal.idTerminal}/commands?token={tokens.GetToken(_client, TypeToken.Partner).Result}";
            HttpResponseMessage response = await _client.PostAsync(url, httpContent);
            var result = response.Content.ReadAsStringAsync().Result;
            if (response.IsSuccessStatusCode)
            {
                TerminalResponse objectResponse = JsonConvert.DeserializeObject<TerminalResponse>(result);

                command = objectResponse.item;

                HistoryDto history = new HistoryDto()
                {
                    dataTime = DateTime.Now,
                    param1 = command.parameter1.ToString() != null ? command.parameter1.ToString() : "0",
                    param2 = command.parameter2.ToString() != null ? command.parameter2.ToString() : "0",
                    param3 = command.parameter3.ToString() != null ? command.parameter3.ToString() : "0",
                    status = command.state_name,
                    command = commandTerminal.commandName,
                };
                var res = await historyService.CreateAsync(history);
                if(res != null)
                {
                    return command;
                }
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
