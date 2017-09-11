using System;
using Xunit;
using Challenge.Infrastructure;
using Challenge.Domain;

namespace Challenge.Infrastructure.Tests
{
    // Current test doing directly to the research from Azure Search
    // TODO: Implement mock to test the fonctionnality, not the data
    public class LocationRepositoryTest
    {
        private readonly LocationRepository _repository;

        public LocationRepositoryTest(){
            _repository = new LocationRepository();
        }

        // test to see if given any recherche parameter still returns something
        [Fact]
        public void GetLocationNotEmpty()
        {
            var searchInput = new Search();
            var result = _repository.GetLocations(searchInput);
            Assert.NotEqual(result.Count, 0);
        }

        // test with multiple values in different alphabets that we know will not be found in our recherche
        [Theory]
        [InlineData("ondejudasperdeuasbotas")]
        //[InlineData("犹大失去了靴子")]
        //[InlineData("حيث خسر يهوذا حذاءه")]
        public void GetLocationEmpty(string search)
        {
            var searchInput = new Search();
            searchInput.SearchWord = search;

            var result = _repository.GetLocations(searchInput);
            Assert.Equal(result.Count, 0);
        }
    }
}
