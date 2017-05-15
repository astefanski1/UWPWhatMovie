using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using WhatMovie.Models;
using WhatMovie.Rest;
using System.Threading.Tasks;
using Windows.UI.Xaml.Media.Imaging;
using System.Collections.ObjectModel;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace WhatMovie
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class Home : Page
    {

        public ObservableCollection<Movie> movies { get; set; }
        public ObservableCollection<Movie> addMovies { get; set; }
        Movie movieDetails;
        public int page { get; set; }
        public Home()
        {
            this.InitializeComponent();
            PopularMoviesDetails.Visibility = Visibility.Collapsed;
            movies = new ObservableCollection<Movie>();
            addMovies = new ObservableCollection<Movie>();
            page = 1;
        }

        private async void Page_Loaded(object sender, RoutedEventArgs e)
        {
            MyProgresRing.IsActive = true;
            MyProgresRing.Visibility = Visibility.Visible;
            await MovieApi.PopularMovieToListAsync(movies,1);
            MyProgresRing.IsActive = false;
            MyProgresRing.Visibility = Visibility.Collapsed;
        }

        private async void PopularMoviesGrid_ItemClick(object sender, ItemClickEventArgs e)
        {
            PopularMoviesGrid.Visibility = Visibility.Collapsed;
            PopularMoviesDetails.Visibility = Visibility.Visible;
            var selectedMovie = (Movie)e.ClickedItem;

            movieDetails = await MovieApi.GetMovieAsync(selectedMovie.id);

            mdImage.Source = new BitmapImage(new Uri(movieDetails.poster_path, UriKind.Absolute));
            Title.Text = " " + movieDetails.title;
            OriginalTitle.Text = " " + movieDetails.original_title;
            ReleaseDate.Text = " " + movieDetails.release_date;
            Overview.Text = " " + movieDetails.overview;
            VoteCount.Text = " " + movieDetails.vote_count;
            VoteAverage.Text = " " + movieDetails.vote_average;
            foreach (var genre in movieDetails.genres)
            {
                Genres.Text += " " + genre.name;
            }
            OriginalLanguage.Text = " " + movieDetails.original_language;
            Budget.Text = " " + movieDetails.budget;
        }

        private async void MoreMovies_Click(object sender, RoutedEventArgs e)
        {
            page = page + 1;
            await MovieApi.PopularMovieToListAsync(addMovies, page);
            foreach (var movie in addMovies)
            {
                movies.Add(movie);
            }
            addMovies.Clear();
        }
    }
}
