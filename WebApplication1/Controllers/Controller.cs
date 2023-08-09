using Microsoft.AspNetCore.Mvc;

using Newtonsoft.Json.Linq;
using UC1.Extensions;

namespace WebApplication1.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CountryController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        public CountryController(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        [HttpGet]
        public async Task<IActionResult> GetCountries(string? filter, int? number, string? param3)
        {

            using (HttpClient client = new HttpClient())
            {
                try
                {
                    var response = await client.GetStringAsync(_configuration["ApiUrl"]);
                    var countries = JArray.Parse(response);

                    // Trim down data
                    var trimmedCountries = countries.Select(c => new
                    Country
                    {
                        Name = c["name"]?["common"]?.ToString(),
                        Capital = c["capital"]?.FirstOrDefault()?.ToString(),
                        Region = c["region"]?.ToString(),
                        SubRegion = c["subregion"]?.ToString(),
                        Population = Convert.ToInt32(c["population"]) 
                    });

                    var filteredCountries = CountryHelper.SearchCountries(trimmedCountries, filter);

                    return Ok(filteredCountries);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    return StatusCode(500, "An internal server error occurred.");
                }
            }
        }

    }
}