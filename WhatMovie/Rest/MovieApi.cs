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


        //To Model List
        public static async Task PopularMovieToListAsync(ObservableCollection<Movie> moviesList, int page)
        {
            var movieData = await GetPopularMoviesAsync(page);

            var movies = movieData.results;

            foreach (var movie in movies)
            {
                string url = String.Format("http://image.tmdb.org/t/p/w185{0}", movie.poster_path);
                movie.poster_path = url;
                // Filter characters that are missing thumbnail images
                moviesList.Add(movie);
            }
        }

        public static async Task TopRatedMovieToListAsync(ObservableCollection<Movie> moviesList, int page)
        {
            var movieData = await GetTopRatedMoviesAsync(page);

            var movies = movieData.results;

            foreach (var movie in movies)
            {
                string url = String.Format("http://image.tmdb.org/t/p/w185{0}", movie.poster_path);
                movie.poster_path = url;
                // Filter characters that are missing thumbnail images
                moviesList.Add(movie);
            }
        }

        //Getters

        public async static Task<Movie> GetMovieAsync(int movieId)
        {
            var http = new HttpClient();
            string reqUrl = String.Format("https://api.themoviedb.org/3/movie/{0}?api_key={1}&language=en-US", movieId, privateApiKey);
            var response = await http.GetAsync(reqUrl);
            var result = await response.Content.ReadAsStringAsync();
            var serializer = new DataContractJsonSerializer(typeof(Movie));

            var ms = new MemoryStream(Encoding.UTF8.GetBytes(result));
            var data = (Movie)serializer.ReadObject(ms);


            string url = String.Format("http://image.tmdb.org/t/p/w185{0}", data.poster_path);
            data.poster_path = url;
            return data;
        }

        public async static Task<MovieData> GetPopularMoviesAsync(int page)
        {
            string url = String.Format("https://api.themoviedb.org/3/movie/popular?api_key={0}&language=en-US&page={1}", privateApiKey, page);
            HttpClient http = new HttpClient();
            var response = await http.GetAsync(url);
            var jsonMessage = await response.Content.ReadAsStringAsync();

            //Response

            var serializer = new DataContractJsonSerializer(typeof(MovieData));
            var memoryStream = new MemoryStream(Encoding.UTF8.GetBytes(jsonMessage));

            var data = (MovieData)serializer.ReadObject(memoryStream);

            return data;
        }

        public async static Task<MovieData> GetTopRatedMoviesAsync(int page)
        {
            string url = String.Format("https://api.themoviedb.org/3/movie/top_rated?api_key={0}&language=en-US&page={1}", privateApiKey, page);
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
