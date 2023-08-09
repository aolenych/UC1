using UC1.Extensions;

namespace UC1.Tests
{
    [TestFixture]
    public class CountryHelperTests
    {
        private List<Country> _testCountries;

        [SetUp]
        public void Setup()
        {
            _testCountries = new List<Country>
        {
            new Country { Name = "United States", Population = 300_000_000 },
            new Country { Name = "Canada", Population = 35_000_000 },
            new Country { Name = "Australia", Population = 25_000_000 }
        };
        }

        [Test]
        public void SearchCountries_WithValidSearchTerm_ReturnsFilteredCountries()
        {
            var result = CountryHelper.SearchCountries(_testCountries, "Can");
            Assert.That(result.Count(), Is.EqualTo(1));
            Assert.That(result.First().Name, Is.EqualTo("Canada"));
        }

        [Test]
        public void SearchCountries_WithNullOrEmptySearchTerm_ReturnsAllCountries()
        {
            var result = CountryHelper.SearchCountries(_testCountries, null);
            Assert.That(result.Count(), Is.EqualTo(3));
        }

        [Test]
        public void FilterByPopulation_WithValidMillionsFilter_ReturnsFilteredCountries()
        {
            var result = CountryHelper.FilterByPopulation(_testCountries, 30);
            Assert.That(result.Count(), Is.EqualTo(1));
        }

        [Test]
        public void FilterByPopulation_WithoutFilter_ReturnsAllCountries()
        {
            var result = CountryHelper.FilterByPopulation(_testCountries, null);
            Assert.AreEqual(3, result.Count());
        }

        [Test]
        public void SortCountries_DescendingOrder_ReturnsDescSortedCountries()
        {
            var result = CountryHelper.SortCountries(_testCountries, "descend");
            Assert.That(result.First().Name, Is.EqualTo("United States"));
            Assert.That(result.Last().Name, Is.EqualTo("Australia"));
        }

        [Test]
        public void SortCountries_AscendingOrder_ReturnsAscSortedCountries()
        {
            var result = CountryHelper.SortCountries(_testCountries, "ascend");
            Assert.That(result.First().Name, Is.EqualTo("Australia"));
            Assert.That(result.Last().Name, Is.EqualTo("United States"));
        }

        [Test]
        public void SortCountries_InvalidOrder_ReturnsSameOrder()
        {
            var result = CountryHelper.SortCountries(_testCountries, "invalid");
            Assert.That(result.First().Name, Is.EqualTo("United States"));
            Assert.That(result.Last().Name, Is.EqualTo("Australia"));
        }

        [Test]
        public void GetPagedCountries_ValidNumber_ReturnsLimitedCountries()
        {
            var result = CountryHelper.GetPagedCountries(_testCountries, 2);
            Assert.That(result.Count(), Is.EqualTo(2));
        }

        [Test]
        public void ProcessCountries_ValidParams_ReturnsCountries()
        {
            var result = CountryHelper.ProcessCountries(_testCountries,"a", 350, "descend", 2);
            Assert.That(result.Count(), Is.EqualTo(2));
        }
    }

}