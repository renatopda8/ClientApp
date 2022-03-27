namespace ClientApp.Services.Ibge
{
    public class IbgeService : IIbgeService
    {
        private readonly HttpClient _httpClient;
        //private readonly IMemoryCache _memoryCache;

        public IbgeService(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _httpClient.BaseAddress = new Uri("https://servicodados.ibge.gov.br/api/v1/");

            //_memoryCache = memoryCache;
        }

        public async Task<IEnumerable<Estado>?> GetEstadosAsync()
        {
            var estados = (await _httpClient.GetFromJsonAsync<IEnumerable<Estado>>("localidades/estados"))?.ToList();

            return estados?.OrderBy(e => e.Nome);
        }

        public async Task<IEnumerable<Municipio>?> GetMunicipiosPorEstadoAsync(string siglaUf)
        {
            var municipios = (await _httpClient.GetFromJsonAsync<IEnumerable<Municipio>>($"localidades/estados/{siglaUf}/municipios"))?.ToList();

            return municipios;
        }
    }
}