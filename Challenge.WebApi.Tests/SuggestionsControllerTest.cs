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
            var mockService = new Mock<ILocationService>();
            
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

            var locations = new List<Location>();
            locations.Add(new Location {
                Name = "wordwithana",
                Latitude = 1.1m,
                Longitude = 1.1m
            });

            // Configure the mock of the service to return what we want for the test
            // TODO: Doesnt work now, GetLocations is not overloaded, locations return nothing
            // and crashs the test
            var mockService = new Mock<ILocationService>();
            mockService.Setup(repo => repo.GetLocations(search))
                .Returns(Task.FromResult(locations));

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
