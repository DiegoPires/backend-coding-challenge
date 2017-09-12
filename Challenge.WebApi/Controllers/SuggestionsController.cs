﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Challenge.Domain;
using Challenge.Infrastructure;
using System.Threading.Tasks;

namespace Challenge.WebApi.Controllers
{
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
            var retour = new Suggestions();

            if (!String.IsNullOrEmpty(q)){
            
                // create our DTO to send to the repository
                Search searchDTO = CreateSearchDTO(q, longitude, latitude);
                
                // call the repository
                var locations = await _service.GetLocations(searchDTO);
                
                // transfor the return from the repository in the return required for the API
                retour = CreateReturnObject(locations);
            }else{
                retour.ListSuggestion = new List<Suggestions.Suggestion>();
            }
            
            return Json(retour);
        }

        private Search CreateSearchDTO(string q, string longitude, string latitude){
            var search = new Search(q);
                 
            search.Longitude = longitude.ToDecimal() ;
            search.Latitude = latitude.ToDecimal();
            //decimal.TryParse(longitude, out search.Longitude);
            //decimal.TryParse(latitude, out search.Latitude);

            return search;
        }

        private Suggestions CreateReturnObject(List<Location> locations){
            var suggestionsReturn = new Suggestions();

            suggestionsReturn.ListSuggestion = new List<Suggestions.Suggestion>();
                
            if(locations.Count() > 0){
                
                var suggestions = from o in locations
                                select new Suggestions.Suggestion {
                                    Name= o.Name, 
                                    Latitude = o.Latitude.ToString(), 
                                    Longitude = o.Longitude.ToString(),
                                    Score = 1
                                };

                suggestionsReturn.ListSuggestion.AddRange(suggestions);
            }

            return suggestionsReturn;
        }
    }
}