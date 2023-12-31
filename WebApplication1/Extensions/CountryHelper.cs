﻿using System.Runtime.CompilerServices;

namespace UC1.Extensions
{
    public static class CountryHelper
    {
        public static IEnumerable<Country> ProcessCountries(IEnumerable<Country> countries, string? filter, int? populationFilter, string? sortOrder, int limit)
        {
            IEnumerable<Country> _processedCountries;
            _processedCountries = SearchCountries(countries, filter);
            _processedCountries = FilterByPopulation(_processedCountries, populationFilter);
            _processedCountries = SortCountries(_processedCountries, sortOrder);
            _processedCountries = GetPagedCountries(_processedCountries, limit);

            return _processedCountries;
        }

        internal static IEnumerable<Country> SearchCountries(IEnumerable<Country> countries, string? searchTerm)
        {
            if (string.IsNullOrEmpty(searchTerm))
            {
                return countries;  // return all countries if searchTerm is null or empty
            }
            searchTerm = searchTerm.Trim().ToLower();
            return countries
                .Where(country =>
                    !string.IsNullOrEmpty(country.Name) &&
                    country.Name.ToLower().Contains(searchTerm))
                .ToList();
        }

        internal static IEnumerable<Country> FilterByPopulation(IEnumerable<Country> countries, int? millions = null)
        {
            if (millions == null)
                return countries;

            long populationThreshold = (long)millions * 1_000_000;
            return countries.Where(c => c.Population < populationThreshold).ToList();
        }

        internal static IEnumerable<Country> SortCountries(IEnumerable<Country> countries, string? order)
        {
            if (order == "descend")
            {
                return countries.OrderByDescending(c => c.Name).ToList();
            }
            else if (order == "ascend")
            {
                return countries.OrderBy(c => c.Name).ToList();
            }
            else return countries;
        }

        internal static IEnumerable<Country> GetPagedCountries(IEnumerable<Country> countries, int numberOfCountries)
        {
            return countries.Take(numberOfCountries).ToList();
        }

    }
}
