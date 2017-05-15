using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using WhatMovie.Models;
using WhatMovie.Rest;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace WhatMovie
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class Top : Page
    {
        public ObservableCollection<Movie> movies { get; set; }
        Movie movieDetails;
        public Top()
        {
            this.InitializeComponent();
            movies = new ObservableCollection<Movie>();
            MovieDetailsGrid.Visibility = Visibility.Collapsed;
        }

        private async void Page_Loaded(object sender, RoutedEventArgs e)
        {
            MyProgresRing.IsActive = true;
            MyProgresRing.Visibility = Visibility.Visible;
            await MovieApi.TopRatedMovieToListAsync(movies, 1);
            MyProgresRing.IsActive = false;
            MyProgresRing.Visibility = Visibility.Collapsed;
        }

        private async void TopRatedGridView_ItemClick(object sender, ItemClickEventArgs e)
        {
            MovieDetailsGrid.Visibility = Visibility.Visible;
            var selectedMovie = (Movie)e.ClickedItem;

            movieDetails = await MovieApi.GetMovieAsync(selectedMovie.id);

            mdImage.Source = new BitmapImage(new Uri(movieDetails.poster_path, UriKind.Absolute));
            Title.Text = " " + movieDetails.title;
            OriginalTitle.Text = " " + movieDetails.original_title;
            ReleaseDate.Text = " " + movieDetails.release_date;
            Overview.Text = " " + movieDetails.overview;
            VoteCount.Text = " " + movieDetails.vote_count;
            VoteAverage.Text = " " + movieDetails.vote_average;
            foreach(var genre in movieDetails.genres)
            {
                Genres.Text += " " + genre.name;
            }
            OriginalLanguage.Text = " " + movieDetails.original_language;
            Budget.Text = " " + movieDetails.budget;


            MoviesGrid.Visibility = Visibility.Collapsed;
        }
    }
}
