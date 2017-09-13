using System;
using Xunit;
using Challenge.Infrastructure;
using Challenge.Domain;

namespace Challenge.Infrastructure.Tests
{
    // Current test doing directly to the research from Azure Search
    // TODO: Implement mock to test the fonctionnality, not the data
    public class SuggestionServiceTest
    {
        private readonly SuggestionService _service;

        public SuggestionServiceTest(){
            _service = new SuggestionService();
        }

        // test to see if given any recherche parameter still returns something
        [Fact]
        public async void GetSuggestionNotEmpty()
        {
            var searchInput = new Search("a");
            var result = await _service.GetSuggestions(searchInput);
            Assert.NotEqual(result.ListSuggestion.Count, 0);
        }

        // test with multiple values in different alphabets that we know will not be found in our recherche
        [Theory]
        [InlineData("ondejudasperdeuasbotas")]
        //[InlineData("犹大失去了靴子")]
        //[InlineData("حيث خسر يهوذا حذاءه")]
        public async void GetSuggestionEmpty(string search)
        {
            var searchInput = new Search(search);

            var result = await _service.GetSuggestions(searchInput);
            Assert.Equal(result.ListSuggestion.Count, 0);
        }
    }
}
