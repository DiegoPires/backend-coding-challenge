using System;
using Challenge.Domain;

namespace Challenge.Seed
{
    using System;
    using System.IO;
    using System.Linq;
    using System.Threading;
    using System.Collections.Generic;
    using Microsoft.Azure.Search;
    using Microsoft.Azure.Search.Models;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Spatial;
    using Microsoft.Extensions.Configuration.Json;
    
    /// <summary>
    /// This console is used just to seed data to Azure Search
    /// Based to the repository: 
    /// https://github.com/Azure-Samples/search-dotnet-getting-started/tree/master/DotNetHowTo/DotNetHowTo
    /// </summary>
    class Program
    {
        static void Main(string[] args)
        {
            Seed();

            //Search();
        }


        #region "Seed methods"
        private static void Seed() {

            IConfigurationBuilder builder = new ConfigurationBuilder().AddJsonFile("appsettings.json");
            IConfigurationRoot configuration = builder.Build();

            SearchServiceClient serviceClient = CreateSearchServiceClient(configuration);

            Console.WriteLine("{0}", "Deleting index...\n");
            DeleteLocationsIndexIfExists(serviceClient);

            Console.WriteLine("{0}", "Creating index...\n");
            CreateLocationsIndex(serviceClient);

            ISearchIndexClient indexClient = serviceClient.Indexes.GetClient("locations");

            Console.WriteLine("{0}", "Uploading documents...\n");
            UploadDocuments(indexClient);
        }

        private static SearchServiceClient CreateSearchServiceClient(IConfigurationRoot configuration)
        {
            string searchServiceName = configuration["SearchServiceName"];
            string adminApiKey = configuration["SearchServiceAdminApiKey"];

            SearchServiceClient serviceClient = new SearchServiceClient(searchServiceName, new SearchCredentials(adminApiKey));
            return serviceClient;
        }

        private static SearchIndexClient CreateSearchIndexClient(IConfigurationRoot configuration)
        {
            string searchServiceName = configuration["SearchServiceName"];
            string queryApiKey = configuration["SearchServiceQueryApiKey"];

            SearchIndexClient indexClient = new SearchIndexClient(searchServiceName, "locations", new SearchCredentials(queryApiKey));
            return indexClient;
        }

        private static void DeleteLocationsIndexIfExists(SearchServiceClient serviceClient)
        {
            if (serviceClient.Indexes.Exists("locations"))
            {
                serviceClient.Indexes.Delete("locations");
            }
        }

        private static void CreateLocationsIndex(SearchServiceClient serviceClient)
        {
            var definition = new Index()
            {
                Name = "locations",
                Fields = FieldBuilder.BuildForType<Location>()
            };

            serviceClient.Indexes.Create(definition);
        }

        private static void UploadDocuments(ISearchIndexClient indexClient)
        {
            var locations = CreateList();

            try
            {
                // Azure Search dont accepted for than 1000 at a time to upload
                var size = 1000;
                for(int i=0; i < locations.Count; i+= size){
                    
                    var max = Math.Min(size, locations.Count -i);
                    Console.WriteLine("{0} {1}\n", i, max);

                    // pumping the data here
                    var batch = IndexBatch.Upload(
                        locations.GetRange(i, max).ToArray()
                    );
                    indexClient.Documents.Index(batch);
                }
            }
            catch (IndexBatchException e)
            {
                // Sometimes when your Search service is under load, indexing will fail for some of the documents in
                // the batch. Depending on your application, you can take compensating actions like delaying and
                // retrying. For this simple demo, we just log the failed document keys and continue.
                Console.WriteLine(
                    "Failed to index some of the documents: {0}",
                    String.Join(", ", e.IndexingResults.Where(r => !r.Succeeded).Select(r => r.Key)));
            }

            Console.WriteLine("Waiting for documents to be indexed...\n");
            Thread.Sleep(2000);
        }

        private static List<Location> CreateList()
        {
            /* Example of the data in the file as a JSON
                {
                    "id": 4794350,
                    "name": "Wolf Trap",
                    "ascii": "Wolf Trap",
                    "alt_name": null,
                    "lat": 38.93983,
                    "long": -77.28609,
                    "feat_class": "P",
                    "feat_code": "PPL",
                    "country": "US",
                    "cc2": null,
                    "admin1": "VA",
                    "admin2": 59,
                    "admin3": null,
                    "admin4": null,
                    "population": 16131,
                    "elevation": 86,
                    "dem": 89,
                    "tz": "America/New_York",
                    "modified_at": "2011-05-14"
                }, */

            string filePath = System.IO.Path.GetFullPath("data/cities_canada-usa.tsv");

            var locations = new List<Location>();

            // Haven't find a library that does this, so doing manually
            using (var streamReader = System.IO.File.OpenText(filePath))
            {
                while (!streamReader.EndOfStream)
                {
                    var line = streamReader.ReadLine();
                    var data = line.Split(new[] { '\t' });

                    if (!String.IsNullOrEmpty(data[0]) && data[0] != "id") {
                        
                        var location = new Location() { 
                            Id = data[0], 
                            Name = data[1], 
                            AsciiName = data[2],
                            AlternatifName = data[3],
                            Geo = GeographyPoint.Create(
                                    double.Parse(data[4]), 
                                    double.Parse(data[5])
                                    )
                        };

                        locations.Add(location);
                    }
                }
            }

            return locations;
        }
        #endregion

        #region "Search methods"
        private static void Search(){
            IConfigurationBuilder builder = new ConfigurationBuilder().AddJsonFile("appsettings.json");
            IConfigurationRoot configuration = builder.Build();

            ISearchIndexClient indexClientForQueries = CreateSearchIndexClient(configuration);

            RunQueries(indexClientForQueries);

            Console.WriteLine("{0}", "Complete.  Press any key to end application...\n");
            Console.ReadKey();
        }
        
        private static void RunQueries(ISearchIndexClient indexClient)
        {
            SearchParameters parameters;
            DocumentSearchResult<Location> results;

            Console.WriteLine("Search the entire index for the term 'qu' and return only the name field:\n");

            parameters =
                new SearchParameters()
                {
                    Select = new[] { "name", "asciiName", "alternatifName" }
                };

            results = indexClient.Documents.Search<Location>("", parameters);

            WriteDocuments(results);
        }

        private static void WriteDocuments(DocumentSearchResult<Location> searchResults)
        {
            foreach (SearchResult<Location> result in searchResults.Results)
            {
                Console.WriteLine(result.Document);
            }

            Console.WriteLine();
        }

        #endregion
    }
}
