using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WhatMovie.Models
{
    public class Movie
    {
        public int movieId { get; set; }
        public string title { get; set; }
        public string release_date { get; set; }
        public int vote_count { get; set; }
        public Double vote_average { get; set; }
        public string tagline { get; set; }
        public string poster_path { get; set; }
        public string overview { get; set; }
    }

    public class MovieManager
    {
        public static List<Movie> GetMovies()
        {
            var movies = new List<Movie>();

            movies.Add(new Movie { movieId = 1, title = "2012", release_date = "2012-02-20", vote_count = 150, vote_average = 7.8, tagline = "Great Movie", poster_path = "Assets/2012.jpg", overview = "Great Film" });
            movies.Add(new Movie { movieId = 2, title = "2012", release_date = "2012-02-20", vote_count = 150, vote_average = 7.8, tagline = "Great Movie", poster_path = "Assets/2012.jpg", overview = "Great Film" });
            movies.Add(new Movie { movieId = 3, title = "2012", release_date = "2012-02-20", vote_count = 150, vote_average = 7.8, tagline = "Great Movie", poster_path = "Assets/2012.jpg", overview = "Great Film" });
            movies.Add(new Movie { movieId = 4, title = "2012", release_date = "2012-02-20", vote_count = 150, vote_average = 7.8, tagline = "Great Movie", poster_path = "Assets/2012.jpg", overview = "Great Film" });
            movies.Add(new Movie { movieId = 5, title = "2012", release_date = "2012-02-20", vote_count = 150, vote_average = 7.8, tagline = "Great Movie", poster_path = "Assets/2012.jpg", overview = "Great Film" });
            movies.Add(new Movie { movieId = 6, title = "2012", release_date = "2012-02-20", vote_count = 150, vote_average = 7.8, tagline = "Great Movie", poster_path = "Assets/2012.jpg", overview = "Great Film" });
            movies.Add(new Movie { movieId = 7, title = "2012", release_date = "2012-02-20", vote_count = 150, vote_average = 7.8, tagline = "Great Movie", poster_path = "Assets/2012.jpg", overview = "Great Film" });
            movies.Add(new Movie { movieId = 8, title = "2012", release_date = "2012-02-20", vote_count = 150, vote_average = 7.8, tagline = "Great Movie", poster_path = "Assets/2012.jpg", overview = "Great Film" });
            movies.Add(new Movie { movieId = 9, title = "2012", release_date = "2012-02-20", vote_count = 150, vote_average = 7.8, tagline = "Great Movie", poster_path = "Assets/2012.jpg", overview = "Great Film" });
            movies.Add(new Movie { movieId = 10, title = "2012", release_date = "2012-02-20", vote_count = 150, vote_average = 7.8, tagline = "Great Movie", poster_path = "Assets/2012.jpg", overview = "Great Film" });


            return movies;
        }
    }
}
