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
        private static int _limitOfCountriesPerRequest = 10;

        public CountryController(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        /// <summary>
        /// Retieves country filtered by name and maximum population, and order it by ascend or descend sortOrder
        /// </summary>
        /// <param name="filter">Insensitive search</param>
        /// <param name="populationFilter">Maximum population in millions</param>
        /// <param name="sortOrder">Sort order - ascend or descend</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetCountries(string? filter, int? populationFilter, string? sortOrder)
        {
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    var response = await client.GetStringAsync(_configuration["ApiUrl"]);
                    var countries = JArray.Parse(response);

                    // Trim down data
                    var trimmedCountries = countries.Select(c => new Country
                    {
                        Name = c["name"]?["common"]?.ToString(),
                        Capital = c["capital"]?.FirstOrDefault()?.ToString(),
                        Region = c["region"]?.ToString(),
                        SubRegion = c["subregion"]?.ToString(),
                        Population = Convert.ToInt64(c["population"])
                    });

                    var processedCountries = CountryHelper.ProcessCountries(trimmedCountries, filter, populationFilter, sortOrder, _limitOfCountriesPerRequest);

                    return Ok(processedCountries);
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