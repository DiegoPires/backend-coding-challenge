using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

using Challenge.WebApi;
using Challenge.WebApi.Controllers;
using Challenge.Domain;
using Challenge.Infrastructure;

namespace Challenge.WebApi.Tests
{
    public class SuggestionsControllerTest
    {
        [Fact]
        public async Task ReturnNothingWhenNothingIsGivenAsTheRechercheCriteria()
        {
            // mock prep
            var mockService = new Mock<ISuggestionService>();
            
            // Declaration and call to the controller
            var controller = new SuggestionsController(mockService.Object);
            JsonResult result = await controller.Get();

            // Tests
            Assert.IsType<Suggestions>(result.Value);

            Suggestions resultObject = (Suggestions)result.Value;
            Assert.Equal(0, resultObject.ListSuggestion.Count);
        }

        [Theory]
        [InlineData("a")]
        [InlineData("该")]
        [InlineData("ال")]
        public async Task ReturnSomethingWhateverWeGive(string dataFromTheory)
        {
            // variables used for the MOCK
            var search = new Search(dataFromTheory);
            search.Latitude = 0m;
            search.Longitude = 0m;

            var suggestions = new Suggestions();
            suggestions.ListSuggestion = new List<Suggestions.Suggestion>();

            suggestions.ListSuggestion.Add(new Suggestions.Suggestion {
                Name = "wordwithana",
                Latitude = "1.1",
                Longitude = "1.1",
                Score = 1
            });

            // Configure the mock of the service to return what we want for the test
            // TODO: Doesnt work now, GetLocations is not overloaded, locations return nothing
            // and crashs the test
            var mockService = new Mock<ISuggestionService>();
            mockService.Setup(repo => repo.GetSuggestions(search))
                .Returns(Task.FromResult(suggestions));

            // Declaration and call to the controller
            var controller = new SuggestionsController(mockService.Object);
            JsonResult result = await controller.Get(dataFromTheory);

            // tests
            Assert.IsType<Suggestions>(result.Value);

            Suggestions resultObject = (Suggestions)result.Value;
            
            Assert.Equal(1, resultObject.ListSuggestion.Count);
        }
    }
}
