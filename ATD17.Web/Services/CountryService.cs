using ATD17.Web.Data;
using ATD17.Web.Interfaces;

namespace ATD17.Web.Services;

public class CountryService : Country.CountryBase, ICountryService
{
    private readonly ILogger<CountryService> _logger;
    
    public CountryService(ILogger<CountryService> logger)
    {
        _logger = logger;
    }

    public override async Task GetStreamCountries(Empty request, IServerStreamWriter<CountryResponse> responseStream, ServerCallContext context)
    {
        foreach (var dummyCountry in FakeCountries.Countries)
        {
            await responseStream.WriteAsync(new CountryResponse
            {
                Code = dummyCountry.Code,
                Id = dummyCountry.Id,
                Name = dummyCountry.Name
            });
            await Task.Delay(100);
        }
    }

    public override Task<CountriesResponse> GetCountries(Empty request, ServerCallContext context)
    {
        var countriesResponse = new CountriesResponse();
        foreach (var dummyCountry in FakeCountries.Countries)
        {
            countriesResponse.Countries.Add(new CountryResponse
            {
                Code = dummyCountry.Code,
                Id = dummyCountry.Id,
                Name = dummyCountry.Name
            });
        }

        return Task.FromResult(countriesResponse);
    }

    public override Task<CountryResponse> GetCountry(CountryRequest request, ServerCallContext context)
    {
        try
        {
            if (request.Id <= 0)
            {
                throw new RpcException(new Status(StatusCode.InvalidArgument, "Id should be greater than 1"));
            }

            var country = FakeCountries.Countries.SingleOrDefault(x => x.Id == request.Id);

            if (country == null)
            {
                throw new RpcException(new Status(StatusCode.NotFound, $"No country found for id:{request.Id}"));
            }

            return Task.FromResult(new CountryResponse
            {
                Code = country.Code,
                Id = country.Id,
                Name = country.Name
            });
        }
        catch (Exception e)
        {
            _logger.LogError($"{nameof(GetCountries)}, exception message: {e.Message}");
            throw;
        }
    }
}
