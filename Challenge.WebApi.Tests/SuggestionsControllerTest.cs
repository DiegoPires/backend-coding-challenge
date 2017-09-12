using System;
using Xunit;
using Challenge.Domain;
using Challenge.Infrastructure;
using Challenge.WebApi;
using System.Collections.Generic;
using System.Threading.Tasks;
using backend_coding_challenge.Controllers;

namespace Challenge.WebApi.Tests
{
    public class SuggestionsControllerTest
    {
        [Fact]
        public async Task ReturnSomething()
        {
            var search = new Search("whateve");
            var locations = new List<Location>();
            
            // Arrange & Act
            //var mockService = new Mock<ILocationService>();
            //mockService.Setup(repo => repo.GetLocations(search))
            //    .Returns(Task.FromResult(locations));

            //var controller = new SuggestionController(mockService.Object);
            //controller.ModelState.AddModelError("error","some error");

            // Act
            //var result = await controller.Get("nada");

            //var contentResult = Assert.IsType<ICollection<Location>>(result);
            //Assert.NotEqual(0, contentResult.Count());
        }
    }
}
