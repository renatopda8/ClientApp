namespace ClientApp.Services.Ibge
{
    public interface IIbgeService
    {
        Task<IEnumerable<Estado>?> GetEstadosAsync();
        Task<IEnumerable<Municipio>?> GetMunicipiosPorEstadoAsync(string siglaUf);
    }
}