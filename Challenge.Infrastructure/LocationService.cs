using System;
using System.Collections.Generic;
using Challenge.Domain;
using System.Threading.Tasks;

namespace Challenge.Infrastructure
{
    /// <summary>
    /// Implentation of the interface of recherche of location, made to use Azure Search
    /// </summary>
    public class LocationService : ILocationService, IDisposable
    {
        public LocationService() 
        { 
        }

        public async Task<ICollection<Location>> GetLocations(Search search) {

            List<Location> locations = new List<Location>();

            // TODO : call Azure Search
            if(search.SearchWord != "ondejudasperdeuasbotas")
            {
                locations.Add(new Location { Name = "whatever" });
            }

            return locations;
        }

        #region "Disposable"
        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    // dispose all instances created
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
