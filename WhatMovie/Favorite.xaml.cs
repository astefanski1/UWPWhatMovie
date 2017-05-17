using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using WhatMovie.Models;
using WhatMovie.Storage;
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
using WhatMovie.Rest;
using Microsoft.Toolkit.Uwp.Notifications;
using Windows.UI.Notifications;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace WhatMovie
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class Favorite : Page
    {
        public List<Movie> movies { get; set; }
        public Movie movieDetails { get; set; }
        public Movie movieClicked { get; set; }
        public Favorite()
        {
            this.InitializeComponent();
            FavoriteMoviesDetails.Visibility = Visibility.Collapsed;
            using(var db = dbConnection.connectionDB)
            {
                movies = (from p in db.Table<Movie>() select p).ToList();
            }
            if(movies.Count != 0)
            {
                isEmpty.Visibility = Visibility.Collapsed;
            }
        }

        private async void FavoriteMoviesGridView_ItemClick(object sender, ItemClickEventArgs e)
        {
            FavoriteMoviesGrid.Visibility = Visibility.Collapsed;
            MyProgresRing.IsActive = true;
            MyProgresRing.Visibility = Visibility.Visible;
            
            var selectedMovie = (Movie)e.ClickedItem;
            movieClicked = (Movie)e.ClickedItem;

            movieDetails = await MovieApi.GetMovieAsync(selectedMovie.id);

            mdImage.Source = new BitmapImage(new Uri(movieDetails.poster_path, UriKind.Absolute));
            Title.Text = " " + movieDetails.title;
            OriginalTitle.Text = " " + movieDetails.original_title;
            ReleaseDate.Text = " " + movieDetails.release_date;
            Overview.Text = " " + movieDetails.overview;
            VoteCount.Text = " " + movieDetails.vote_count;
            VoteAverage.Text = " " + movieDetails.vote_average;
            OriginalLanguage.Text = " " + movieDetails.original_language;
            Budget.Text = " " + movieDetails.budget;

            MyProgresRing.IsActive = false;
            MyProgresRing.Visibility = Visibility.Collapsed;
            FavoriteMoviesDetails.Visibility = Visibility.Visible;
        }


        private void removeFromCollection_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                using(var db = dbConnection.connectionDB)
                {
                    db.Delete(movieClicked);
                    FavoriteMoviesDetails.Visibility = Visibility.Collapsed;
                    FavoriteMoviesGrid.Visibility = Visibility.Visible;
                    MyFrame.Navigate(typeof(Favorite));
                    PopToast();
                }
            }
            catch
            {
                removeFromCollection.Content = "Can't remove movie from collection";
            }
        }

        private void backToList_Click(object sender, RoutedEventArgs e)
        {
            MyFrame.Navigate(typeof(Favorite));
        }

        private void PopToast()
        {
            ToastContent toastContent = CreateDummyToast();
            ToastNotificationManager.CreateToastNotifier()
            .Show(new ToastNotification(toastContent.GetXml()));
        }

        private ToastContent CreateDummyToast()
        {
            return new ToastContent()
            {
                Launch = "action=viewEvent&eventId=1983",
                Scenario = ToastScenario.Default,
                Visual = new ToastVisual()
                {
                    BindingGeneric = new ToastBindingGeneric()
                    {
                        Children =
                        {
                            new AdaptiveText(){Text = $"Movie removed!"},
                            new AdaptiveText(){Text = $"{movieDetails.title} was removed from u collection just now!"},
                            new AdaptiveText(){Text = $"Always u can add this movie again! ;)"}
                        }
                    }
                }
            };
        }
    }
}
