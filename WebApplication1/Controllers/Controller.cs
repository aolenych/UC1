using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using static System.Net.Mime.MediaTypeNames;
using System.Reflection.Metadata;
using System.Runtime.ConstrainedExecution;
using System;
using Newtonsoft.Json.Linq;
using System.Data.SqlTypes;
using System.Security.Cryptography;

namespace WebApplication1.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CountryController : ControllerBase
    {
        private const string ApiUrl = "https://restcountries.com/v3.1/all";

        [HttpGet]
        public async Task<IActionResult> GetCountries(string? param1, int? number, string? param3)
        {

            using (HttpClient client = new HttpClient())
            {
                try
                {
                    var response = await client.GetStringAsync(ApiUrl);
                    var countries = JArray.Parse(response);

                    // Trim down data
                    var trimmedCountries = countries.Select(c => new
                    {
                        Name = c["name"]["common"]?.ToString(),
                        Capital = c["capital"]?.FirstOrDefault()?.ToString(),
                        Region = c["region"]?.ToString(),
                        SubRegion = c["subregion"]?.ToString(),
                        Population = Convert.ToInt32(c["population"])  // Convert JToken to int
                    });

                    return Ok(trimmedCountries);
                }
                catch (Exception ex)
                {
                    // Log the exception (you should ideally be using a logging framework/library)
                    Console.WriteLine(ex.Message);
                    return StatusCode(500, "An internal server error occurred.");
                }
            }
        }

    }
}