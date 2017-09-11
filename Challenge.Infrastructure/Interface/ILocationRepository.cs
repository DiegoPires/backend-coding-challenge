using System;
using System.Collections.Generic;
using Challenge.Domain;

namespace Challenge.Infrastructure
{
    /// <summary>
    /// Interface d'implentation du repository de recherche de location
    /// </summary>
    public interface ILocationRepository : IDisposable
    {
        ICollection<Location> GetLocations(Search search);
    }
}