using System;
using System.Collections.Generic;
using Challenge.Domain;
using System.Threading.Tasks;

namespace Challenge.Infrastructure
{
    /// <summary>
    /// Interface of the implentation of our suggestion service
    /// </summary>
    public interface ISuggestionService : IDisposable
    {
         Task<Suggestions> GetSuggestions(Search search);
    }
}