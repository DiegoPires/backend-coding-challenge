using System;
using System.Runtime.Serialization;

namespace Challenge.Domain{
    
    /// <summary>
    /// Object used to interact with Azure Search
    /// </summary>
    [DataContract]
    public class Location
    {
        [DataMember(Name="id")]
        public int Id {get;set;}

        [DataMember(Name="name")]
        public string Name {get;set;}
        
        [DataMember(Name="ascii")]
        public string AsciiName	{get;set;}    

        [DataMember(Name="alt_name")]
        public string AlternatifName { get; set; }
        
        [DataMember(Name="lat")]
        public decimal Latitude { get; set;}
        
        [DataMember(Name="long")]
        public decimal Longitude { get; set; }	

        [DataMember(Name ="feat_class")]
        public string FeatClass	{get;set;}	
        
        [DataMember(Name ="feat_code")]
        public string FeatCode	{get;set;}	
        
        [DataMember(Name ="country")]
        public string Country {get;set;}	
        
        [DataMember(Name ="cc2")]
        public string CC2 {get;set;}	
        
        [DataMember(Name ="admin1")]
        public string Admin1 {get;set;}	
        	
        [DataMember(Name ="admin2")]
        public int Admin2{get;set;}	
            	
        [DataMember(Name ="admin3")]
        public int Admin3 {get;set;}	
                	
        [DataMember(Name ="admin4")]
        public int Admin4 {get;set;}
                    	
        [DataMember(Name ="population")]
        public int Population {get;set;}
        
        [DataMember(Name ="elevation")]
        public int Elevation {get;set;}
                            	
        [DataMember(Name ="dem")]
        public int Dem {get;set;}
        
        [DataMember(Name ="tz")]
        public string Timezone {get;set;}
                                	
        [DataMember(Name ="modified_at")]
        public DateTime Modified {get;set;}

    }
}