namespace Challenge.WebApi.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Challenge.Domain;
    using Challenge.Infrastructure;
    using System.Threading.Tasks;

    /// <summary>
    /// Main controller responsable to return the suggestion of locations 
    /// according to the entered criteries
    /// </summary>
    [Route("api/v1/[controller]")]
    public class SuggestionsController : Controller
    {
        ILocationService _service;

        /// <summary>
        /// Constructor of the controller with injection of the desired service
        /// </summary>
        /// <param name="service"></param>
        public SuggestionsController(ILocationService service){
            _service = service;
        }

        /// <summary>
        /// Get call to obtain the list of sugestion regarding the parameters done
        /// </summary>
        /// <remarks>
        ///     # Sample: 
        ///     ```
        ///     GET /api/v1/suggestions?q=Londo&amp;latitude=43.70011&amp;longitude=-79.4163
        ///     {
        ///         "suggestions": [
        ///         {
        ///             "name": "London, ON, Canada",
        ///             "latitude": "42.98339",
        ///             "longitude": "-81.23304",
        ///             "score": 0.9
        ///         }
        ///       ]
        ///     }  
        ///     ```           
        /// </remarks>
        /// <param name="q">Term to search in the location database</param>
        /// <param name="longitude">Longitude of the caller</param>
        /// <param name="latitude">Latitude of the caller</param>
        /// <returns>List of suggestion for the term used in the search</returns>
        [HttpGet]
        [Produces("application/json")]
        public async Task<JsonResult> Get(string q=null, string longitude=null, string latitude=null)
        {
            var suggestions = new Suggestions();

            if (!String.IsNullOrEmpty(q)){
            
                // create our DTO to send to the repository
                Search searchDTO = CreateSearchDTO(q, longitude, latitude);
                
                // call the repository
                var suggestions = await _service.GetLocations(searchDTO);
                
            }else{
                suggestions.ListSuggestion = new List<Suggestions.Suggestion>();
            }
            
            return Json(suggestions);
        }

        private Search CreateSearchDTO(string q, string longitude, string latitude){
            var search = new Search(q);
                 
            search.Longitude = longitude.ToDecimal() ;
            search.Latitude = latitude.ToDecimal();
            
            return search;
        }

        
    }
}