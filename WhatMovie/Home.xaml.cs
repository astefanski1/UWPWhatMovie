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
using WhatMovie.Storage;

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
        public Movie movieDetails { get; set; }
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

            using (var db = dbConnection.connectionDB)
            {
                List<Movie> favoriteMovies = (from p in db.Table<Movie>() select p).ToList();
                foreach (var favMovie in favoriteMovies)
                {
                    if (favMovie.id == movieDetails.id)
                    {
                        addToCollection.Visibility = Visibility.Collapsed;
                    }
                }
            }

            mdImage.Source = new BitmapImage(new Uri(movieDetails.poster_path, UriKind.Absolute));
            Title.Text = " " + movieDetails.title;
            OriginalTitle.Text = " " + movieDetails.original_title;
            ReleaseDate.Text = " " + movieDetails.release_date;
            Overview.Text = " " + movieDetails.overview;
            VoteCount.Text = " " + movieDetails.vote_count;
            VoteAverage.Text = " " + movieDetails.vote_average;
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

        private void addToCollection_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                using (var db = dbConnection.connectionDB)
                {
                    Movie movieToSave = movieDetails;
                    db.Insert(movieToSave);
                    addToCollection.IsEnabled = false;
                }
            }
            catch (Exception)
            {
                addToCollection.Content = "Can't add movie to collection";
            }
        }

        private void backToList_Click(object sender, RoutedEventArgs e)
        {
            MyFrame.Navigate(typeof(Home));
        }
    }
}
