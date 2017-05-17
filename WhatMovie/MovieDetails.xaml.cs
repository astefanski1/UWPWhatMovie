using Microsoft.Toolkit.Uwp.Notifications;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using WhatMovie.Models;
using WhatMovie.Storage;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Notifications;
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
    public sealed partial class MovieDetails : Page
    {
        public Movie movie { get; set; }
        public MovieDetails()
        {
            this.InitializeComponent();
            movie = new Movie();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            movie = (Movie)e.Parameter;
        }

        private void backToList_Click(object sender, RoutedEventArgs e)
        {
            MyFrame.Navigate(typeof(Home));
        }

        private void addToCollection_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                using (var db = dbConnection.connectionDB)
                {
                    Movie movieToSave = movie;
                    db.Insert(movieToSave);
                    addToCollection.IsEnabled = false;
                }
            }
            catch (Exception)
            {
                addToCollection.Content = "Can't add movie to collection";
                PopToast();
            }
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            using (var db = dbConnection.connectionDB)
            {
                List<Movie> favoriteMovies = (from p in db.Table<Movie>() select p).ToList();
                foreach (var favMovie in favoriteMovies)
                {
                    if (favMovie.id == movie.id)
                    {
                        addToCollection.Visibility = Visibility.Collapsed;
                    }
                }
            }

            mdImage.Source = new BitmapImage(new Uri(movie.poster_path, UriKind.Absolute));
            Title.Text = " " + movie.title;
            OriginalTitle.Text = " " + movie.original_title;
            ReleaseDate.Text = " " + movie.release_date;
            Overview.Text = " " + movie.overview;
            VoteCount.Text = " " + movie.vote_count;
            VoteAverage.Text = " " + movie.vote_average;
            OriginalLanguage.Text = " " + movie.original_language;
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
                            new AdaptiveText(){Text = $"Good job!"},
                            new AdaptiveText(){Text = $"U added {movie.title} to your collections!"},
                            new AdaptiveText(){Text = $"Go to collection and check this out!"}
                        }
                    }
                }
            };
        }
    }
}
