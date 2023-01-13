namespace ATD17.Web.Interfaces;

public interface ICountryService
{
    Task<CountriesResponse> GetCountries(Empty request, ServerCallContext context);

    Task<CountryResponse> GetCountry(CountryRequest request, ServerCallContext context);
    
    Task GetStreamCountries(Empty request, IServerStreamWriter<CountryResponse> responseStream,
        ServerCallContext context);
}