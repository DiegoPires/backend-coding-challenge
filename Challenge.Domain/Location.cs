namespace Challenge.Domain{
    
    using System;
    using System.Runtime.Serialization;
    using Microsoft.Azure.Search;
    using Microsoft.Azure.Search.Models;
    using Microsoft.Spatial;
    using System.ComponentModel.DataAnnotations;
    using Newtonsoft.Json;

    /// <summary>
    /// Object used to interact with Azure Search
    /// "IsSearchable" are full text search-able
    /// "IsFilterable" will be searched as the exactly value
    /// "IsSortable" can be sorted by
    /// "IsFacetable" to search categorys 
    /// </summary>
    //[DataContract]
    [SerializePropertyNamesAsCamelCase]
    public class Location
    {
        //[DataMember(Name="id")]
        [System.ComponentModel.DataAnnotations.Key]
        public string Id {get;set;}

        //[DataMember(Name="name")]
        [IsSearchable]
        public string Name {get;set;}
        
        //[DataMember(Name="ascii")]
        [IsSearchable]
        public string AsciiName	{get;set;}    

        //[DataMember(Name="alt_name")]
        [IsSearchable]
        public string AlternatifName { get; set; }
        
        //[DataMember(Name="lat")]
        //public decimal Latitude { get; set;}
        
        //[DataMember(Name="long")]
        //public decimal Longitude { get; set; }	

        [IsFilterable]
        public GeographyPoint Geo { get; set; }

        #region "Fields not yet used"

        //[DataMember(Name ="feat_class")]
        public string FeatClass	{get;set;}	
        
        //[DataMember(Name ="feat_code")]
        public string FeatCode	{get;set;}	
        
        //[DataMember(Name ="country")]
        public string Country {get;set;}	
        
        //[DataMember(Name ="cc2")]
        public string CC2 {get;set;}	
        
        //[DataMember(Name ="admin1")]
        public string Admin1 {get;set;}	
        	
        //[DataMember(Name ="admin2")]
        public int Admin2{get;set;}	
            	
        //[DataMember(Name ="admin3")]
        public int Admin3 {get;set;}	
                	
        //[DataMember(Name ="admin4")]
        public int Admin4 {get;set;}
                    	
        //[DataMember(Name ="population")]
        public int? Population {get;set;}
        
        //[DataMember(Name ="elevation")]
        public int? Elevation {get;set;}
                            	
        //[DataMember(Name ="dem")]
        public int? Dem {get;set;}
        
        //[DataMember(Name ="tz")]
        public string Timezone {get;set;}
                                	
        //[DataMember(Name ="modified_at")]
        public DateTime Modified {get;set;}

        #endregion
    }
}