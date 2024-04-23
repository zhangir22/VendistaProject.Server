using log4net.Appender;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using VendistaProject.Dto.Models;
using VendistaProject.Infrastructure.Migrations;

namespace VendistaProject.Server.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class BaseController : ControllerBase
    {
        protected HttpClient _client = new HttpClient();
        private string? __tokenPartner;
        private string? __tokenClient;
        public BaseController() 
        { 
        }
        public async Task Init()
        {
            __tokenPartner = await TokenInit.GetToken(_client, TypeToken.Partner);
            __tokenClient = await TokenInit.GetToken(_client, TypeToken.Client);
        }


        #region ClientRequests
        [Route("api/SendCommandByClient/{idTerminal}/{token}")]
        [HttpPost]
        public async Task<CommandTerminal> SendCommandByClient(int idTerminal, MultyCommand multyCommand)
        {
            HttpResponseMessage response = await _client.GetAsync("http://178.57.218.210:398/terminals/" + idTerminal + "/commands?token=" + __tokenClient);
            return JsonConvert.DeserializeObject<CommandTerminal>(await response.Content.ReadAsStringAsync());
        }
        [Route("api/GetTerminalsByClient")]
        [HttpGet]
        public async Task<Terminal> GetTerminalByClient()
        {
            HttpResponseMessage response = await _client.GetAsync("http://178.57.218.210:398/terminals?token=" + __tokenClient);
            return JsonConvert.DeserializeObject<Terminal>(await response.Content.ReadAsStringAsync());
        }
        [Route("api/GetCommandByClient")]
        [HttpGet]
        public async Task<Command> GetCommandByClient()
        {
            HttpResponseMessage response = await _client.GetAsync("http://178.57.218.210:398/commands/types?token=" + __tokenClient);
            return JsonConvert.DeserializeObject<Command>(await response.Content.ReadAsStringAsync());
        }
        #endregion
        #region PartnerRequests

        [Route("api/SendCommandByPartner/{idTerminal}/{token}")]
        [HttpPost]
        public async Task<CommandTerminal> SendCommandByPartner(int idTerminal, MultyCommand multyCommand)
        {
            HttpResponseMessage response = await _client.GetAsync("http://178.57.218.210:398/terminals/" + idTerminal + "/commands?token=" + __tokenPartner);
            return JsonConvert.DeserializeObject<CommandTerminal>(await response.Content.ReadAsStringAsync());
        }

        [Route("api/GetTerminalsByPartner")]
        [HttpGet]
        public async Task<Terminal> GetTerminalByPartner()
        {
            HttpResponseMessage response = await _client.GetAsync("http://178.57.218.210:398/terminals?token=" + __tokenPartner);
            return JsonConvert.DeserializeObject<Terminal>(await response.Content.ReadAsStringAsync());
        }
        [Route("api/GetCommandByPartner")]
        [HttpGet]
        public async Task<Command> GetCommandByPartner()
        {
            HttpResponseMessage response = await _client.GetAsync("http://178.57.218.210:398/commands/types?token=" + __tokenPartner);
            return JsonConvert.DeserializeObject<Command>(await response.Content.ReadAsStringAsync());
        }
        #endregion

    }
   
    public enum TypeToken
    {
        Partner = 0,
        Client
    }
    public abstract class  TokenInit
    {
 
        private readonly static string __logP = "user2";
        private readonly static string __pasP = "password2";

        private readonly static string __logC = "user1";
        private readonly static string __pasC = "password1";
        private readonly static string __baseUrl = "http://178.57.218.210:398/token?";
        public static async Task<string?> GetToken(HttpClient client, TypeToken type)
        {
            switch (type)
            {
                case TypeToken.Client:
                    var result = await client.GetAsync(__baseUrl + $"login={__logC}&password={__pasC}");
                    return await result.Content.ReadAsStringAsync();
                case TypeToken.Partner:
                    result = await client.GetAsync(__baseUrl + $"login={__logP}&password{__pasP}");
                    return await result.Content.ReadAsStringAsync();
                default:
                    return null;
            }
        }
   
    }
}
