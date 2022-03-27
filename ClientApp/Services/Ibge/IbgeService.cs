namespace ClientApp.Services.Ibge
{
    public class IbgeService : IIbgeService
    {
        private readonly HttpClient _httpClient;

        private readonly List<Estado> _estadosCache;
        private readonly Dictionary<string, List<Municipio>>? _municipiosPorEstadoCache;

        public IbgeService(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _httpClient.BaseAddress = new Uri("https://servicodados.ibge.gov.br/api/v1/");

            _estadosCache = new List<Estado>();
            _municipiosPorEstadoCache = new Dictionary<string, List<Municipio>>();
        }

        public async Task<IEnumerable<Estado>?> GetEstadosAsync()
        {
            if (_estadosCache.Any() == true)
            {
                return _estadosCache;
            }

            var estados = (await _httpClient.GetFromJsonAsync<IEnumerable<Estado>>("localidades/estados"))?.OrderBy(e => e.Nome)?.ToList();
            
            if (estados != null)
            {
                _estadosCache.Clear();
                _estadosCache.AddRange(estados);
            }
            
            return estados;
        }

        public async Task<IEnumerable<Municipio>?> GetMunicipiosPorEstadoAsync(string siglaUf)
        {
            if (_municipiosPorEstadoCache?.TryGetValue(siglaUf, out var municipios) == true)
            {
                return municipios;
            }

            municipios = (await _httpClient.GetFromJsonAsync<IEnumerable<Municipio>>($"localidades/estados/{siglaUf}/municipios"))?.ToList();

            if (municipios != null)
            {
                _municipiosPorEstadoCache?.Add(siglaUf, municipios);
            }

            return municipios;
        }
    }
}