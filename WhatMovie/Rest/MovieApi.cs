using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;
using WhatMovie.Models;

namespace WhatMovie.Rest
{
    public class MovieApi
    {
        private const string privateApiKey = "8abe256a2710432b88265bf3b5b1d4ab";

        public static async Task MovieDataToListAsync(ObservableCollection<Movie> moviesList)
        {
            var movieData = await GetPopularMoviesAsync();

            var movies = movieData.results;

            foreach (var movie in movies)
            {
                string url = String.Format("http://image.tmdb.org/t/p/w185{0}", movie.poster_path);
                movie.poster_path = url;
                // Filter characters that are missing thumbnail images
                moviesList.Add(movie);
            }
        }


        public async static Task<Movie> GetMovie(string title)
        {
            var http = new HttpClient();
            var response = await http.GetAsync("https://api.themoviedb.org/3/movie/2000?api_key=8abe256a2710432b88265bf3b5b1d4ab&language=en-US");
            var result = await response.Content.ReadAsStringAsync();
            var serializer = new DataContractJsonSerializer(typeof(Movie));

            var ms = new MemoryStream(Encoding.UTF8.GetBytes(result));
            var data = (Movie)serializer.ReadObject(ms);

            return data;
        }

        public async static Task<MovieData> GetPopularMoviesAsync()
        {
            string url = String.Format("https://api.themoviedb.org/3/movie/popular?api_key={0}&language=en-US&page=1", privateApiKey);
            HttpClient http = new HttpClient();
            var response = await http.GetAsync(url);
            var jsonMessage = await response.Content.ReadAsStringAsync();

            //Response

            var serializer = new DataContractJsonSerializer(typeof(MovieData));
            var memoryStream = new MemoryStream(Encoding.UTF8.GetBytes(jsonMessage));

            var data = (MovieData)serializer.ReadObject(memoryStream);

            return data;
        }
    }

}
