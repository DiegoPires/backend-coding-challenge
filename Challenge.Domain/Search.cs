namespace Challenge.Domain{
    
    using System;
    using System.Runtime.Serialization;

    /// <summary>
    /// DTO used to pass the parameters of recherche into the repository
    /// </summary>
    public class Search
    {
        // Force a criteria to initialize, because its mandatory to give a suggestion
        public Search(string term){
            this.Word = term;
        }
        
        public string Word { get; set; }
        
        public decimal Latitude { get; set; }
        
        public decimal Longitude { get; set; }
    }
}