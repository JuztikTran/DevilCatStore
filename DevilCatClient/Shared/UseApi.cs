namespace DevilCatClient.Shared
{
    public interface IUseApi
    {
        Task<string?> GetData(string? uri);
    }

    public class UseApi : IUseApi
    {
        private IHttpClientFactory _httpClientFactory;
        private HttpClient _httpClient = null;

        public UseApi(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
            _httpClient = _httpClientFactory.CreateClient();
            _httpClient.BaseAddress = new Uri("http://devilcatbackend:8080/odata/");
        }

        public async Task<string?> GetData(string? uri)
        {
            Console.WriteLine($"{_httpClient.BaseAddress}{uri}");
            return await _httpClient.GetStringAsync($"{uri}");
        }
    }
}
