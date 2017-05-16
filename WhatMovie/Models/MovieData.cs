using SQLite.Net.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WhatMovie.Models
{
    public class Movie
    {
        [PrimaryKey, AutoIncrement]
        public int idMovie { get; set; }
        public bool adult { get; set; }
        public string backdrop_path { get; set; }
        public int budget { get; set; }
        public string homepage { get; set; }
        public int id { get; set; }
        public string imdb_id { get; set; }
        public string original_language { get; set; }
        public string original_title { get; set; }
        public string overview { get; set; }
        public double popularity { get; set; }
        public string poster_path { get; set; }
        public string release_date { get; set; }
        public int revenue { get; set; }
        public int runtime { get; set; }
        public string status { get; set; }
        public string tagline { get; set; }
        public string title { get; set; }
        public bool video { get; set; }
        public double vote_average { get; set; }
        public int vote_count { get; set; }
    }

    public class MovieData
    {
        public int page { get; set; }
        public List<Movie> results { get; set; }
        public int total_results { get; set; }
        public int total_pages { get; set; }
    }
}
