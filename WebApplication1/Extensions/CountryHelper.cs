using Newtonsoft.Json.Linq;

namespace UC1.Extensions
{
    public static class CountryHelper
    {
        public static IEnumerable<Country> SearchCountries(IEnumerable<Country> countries, string searchTerm)
        {
            if (string.IsNullOrEmpty(searchTerm))
            {
                return countries;  // return all countries if searchTerm is null or empty
            }

            return countries
                .Where(country =>
                    !string.IsNullOrEmpty(country.Name) &&
                    country.Name.IndexOf(searchTerm, StringComparison.OrdinalIgnoreCase) >= 0)
                .ToList();
        }
    }
}
