using System;
using System.Runtime.Serialization;

namespace Challenge.Domain{
    
    /// DTO used to pass the parameters of recherche into the repository
    public class Search
    {
        public Search(string searchWord){
            this.SearchWord = searchWord;
        }
        
        public string SearchWord { get; set; }
        
        public decimal Latitude { get; set; }
        
        public decimal Longitude { get; set; }
    }
}