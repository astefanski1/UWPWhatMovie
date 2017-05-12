using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;

namespace WhatMovie.Rest
{
    public class MovieApi
    {
        public async static Task<RootObject> GetMovie(string title)
        {
            var http = new HttpClient();
            var response = await http.GetAsync("https://api.themoviedb.org/3/movie/2000?api_key=8abe256a2710432b88265bf3b5b1d4ab&language=en-US");
            var result = await response.Content.ReadAsStringAsync();
            var serializer = new DataContractJsonSerializer(typeof(RootObject));

            var ms = new MemoryStream(Encoding.UTF8.GetBytes(result));
            var data = (RootObject)serializer.ReadObject(ms);

            return data;
        }
    }

    [DataContract]
    public class Genre
    {
        [DataMember]
        public int id { get; set; }
        [DataMember]
        public string name { get; set; }
    }

    [DataContract]
    public class ProductionCompany
    {
        [DataMember]
        public string name { get; set; }
        [DataMember]
        public int id { get; set; }
    }

    [DataContract]
    public class ProductionCountry
    {
        [DataMember]
        public string iso_3166_1 { get; set; }
        [DataMember]
        public string name { get; set; }
    }

    [DataContract]
    public class SpokenLanguage
    {
        [DataMember]
        public string iso_639_1 { get; set; }
        [DataMember]
        public string name { get; set; }
    }

    [DataContract]
    public class RootObject
    {
        [DataMember]
        public bool adult { get; set; }
        [DataMember]
        public string backdrop_path { get; set; }
        [DataMember]
        public object belongs_to_collection { get; set; }
        [DataMember]
        public int budget { get; set; }
        [DataMember]
        public List<Genre> genres { get; set; }
        [DataMember]
        public string homepage { get; set; }
        [DataMember]
        public int id { get; set; }
        [DataMember]
        public string imdb_id { get; set; }
        [DataMember]
        public string original_language { get; set; }
        [DataMember]
        public string original_title { get; set; }
        [DataMember]
        public string overview { get; set; }
        [DataMember]
        public double popularity { get; set; }
        [DataMember]
        public string poster_path { get; set; }
        [DataMember]
        public List<ProductionCompany> production_companies { get; set; }
        [DataMember]
        public List<ProductionCountry> production_countries { get; set; }
        [DataMember]
        public string release_date { get; set; }
        [DataMember]
        public int revenue { get; set; }
        [DataMember]
        public int runtime { get; set; }
        [DataMember]
        public List<SpokenLanguage> spoken_languages { get; set; }
        [DataMember]
        public string status { get; set; }
        [DataMember]
        public string tagline { get; set; }
        [DataMember]
        public string title { get; set; }
        [DataMember]
        public bool video { get; set; }
        [DataMember]
        public double vote_average { get; set; }
        [DataMember]
        public int vote_count { get; set; }
    }
}
