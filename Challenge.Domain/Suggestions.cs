namespace Challenge.Domain{

    using System;
    using System.Collections.Generic;
    using System.Runtime.Serialization;

    /// <summary>
    /// Declaration of the object returned for the API
    /// </summary>
    [DataContract]
    public class Suggestions
    {
        [DataMember(Name ="suggestions")]
        public List<Suggestion> ListSuggestion {get;set;}
        
        [DataContract]
        public class Suggestion
        {
            [DataMember(Name ="name")]
            public string Name {get;set;}

            [DataMember(Name ="latitude")]
            public string Latitude {get;set;}

            [DataMember(Name ="longitude")]
            public string Longitude {get;set;}

            [DataMember(Name ="score")]
            public double Score {get;set;}
        }

    }

}