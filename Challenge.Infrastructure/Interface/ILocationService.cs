using System;
using System.Collections.Generic;
using Challenge.Domain;
using System.Threading.Tasks;

namespace Challenge.Infrastructure
{
    /// <summary>
    /// Interface d'implentation du repository de recherche de location
    /// </summary>
    public interface ILocationService : IDisposable
    {
         Task<ICollection<Location>> GetLocations(Search search);
    }
}