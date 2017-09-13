namespace Challenge.Infrastructure
{
    using System;
    using System.Linq;
    using System.Collections.Generic;
    using Challenge.Domain;
    using System.Threading.Tasks;
    using Microsoft.Azure.Search;
    using Microsoft.Azure.Search.Models;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Spatial;
    using Microsoft.Extensions.Configuration.Json;

    /// <summary>
    /// Implentation of the interface of recherche of location, made to use Azure Search
    /// </summary>
    public class SuggestionService : ISuggestionService, IDisposable
    {
        ISearchIndexClient indexClientForQueries;

        /// <summary>
        /// On the constructor initialize our connection to Azure Search
        /// </summary>
        public SuggestionService() 
        { 
            IConfigurationBuilder builder = new ConfigurationBuilder().AddJsonFile("appsettings.json");
            IConfigurationRoot configuration = builder.Build();

            indexClientForQueries = CreateSearchIndexClient(configuration);
        }

        public async Task<Suggestions> GetSuggestions(Search search) {

            SearchParameters parameters;
            DocumentSearchResult<Location> results;

            parameters =
                new SearchParameters()
                {
                    Select = new[] { "name", "geo" },
                    Query​Type = Microsoft.Azure.Search.Models.QueryType.Full,
                    SearchFields = new [] { "name", "asciiName", "alternatifName"}
                };

            // TODO: Find the way to call ASYNC
            results = indexClientForQueries.Documents.Search<Location>(
                String.Concat(search.Word, "~"), parameters);

            return CreateReturnObject(results);
        }

        private Suggestions CreateReturnObject(DocumentSearchResult<Location> searchResult){
            var suggestionsReturn = new Suggestions();

            suggestionsReturn.ListSuggestion = new List<Suggestions.Suggestion>();
                
            var suggestions = from o in searchResult.Results
                            select new Suggestions.Suggestion {
                                Name= o.Document.Name, 
                                Latitude = o.Document.Geo.Latitude.ToString(), 
                                Longitude = o.Document.Geo.Longitude.ToString(),
                                Score = o.Score
                            };

            suggestionsReturn.ListSuggestion.AddRange(suggestions);
            
            return suggestionsReturn;
        }

        private static SearchIndexClient CreateSearchIndexClient(IConfigurationRoot configuration)
        {
            string searchServiceName = configuration["SearchServiceName"];
            string queryApiKey = configuration["SearchServiceQueryApiKey"];

            SearchIndexClient indexClient = new SearchIndexClient(searchServiceName, "locations", new SearchCredentials(queryApiKey));
            return indexClient;
        }

        #region "Disposable"
        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    // TODO: Check if its the good way to get ride of him
                    indexClientForQueries.Dispose();
                    indexClientForQueries = null; 
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        #endregion
    }
}
