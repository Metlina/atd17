using ATD17.Web.Interfaces;

namespace ATD17.Web.Controllers;

[ApiController]
[Route("Country")]
public class CountryController : ControllerBase
{
    private readonly ICountryService _countryService;

    public CountryController(ICountryService service)
    {
        _countryService = service;
    }
    
    [HttpGet("GetCountries")]
    public async Task<IActionResult> GetCountries()
    {
        return Ok(await _countryService.GetCountries(new Empty(), null));
    }
    
    [HttpGet("GetCountry/{id:int}")]
    public async Task<IActionResult> GetCountry(int id)
    {
        var request = new CountryRequest { Id = id };
        return Ok(await _countryService.GetCountry(request, null));
    }
}