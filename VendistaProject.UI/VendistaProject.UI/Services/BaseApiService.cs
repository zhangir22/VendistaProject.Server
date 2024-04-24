using VendistaProject.UI.Services.Interfaces;
using VendistaProject.Domain.Dto;
namespace VendistaProject.UI.Services
{
    public enum ApiTypeQuery
    {
        GET,
        POST,
    }
    public class BaseApiService: IBaseApiService
    {
        private readonly HttpClient _client;
        public BaseApiService()
        {
            _client = new HttpClient();
        }
        
        public static HttpClient PreparedClient()
        {
            HttpClientHandler handler = new HttpClientHandler();
            handler.ServerCertificateCustomValidationCallback += (sender, cert, chain, sslPolicyErrors) => { return true; };

            HttpClient client = new HttpClient(handler);
            return client;
        } 
        protected async Task<ResponseDto> SendQuery(string path, string queryParams = "", ApiTypeQuery queryType = ApiTypeQuery.GET)
        {
            string url = $"{AppConfiguration.ApiUrl}{path}?{queryParams}";
        }
    }
}
