﻿using Microsoft.Toolkit.Uwp.Notifications;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using WhatMovie.Models;
using WhatMovie.Rest;
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
    public sealed partial class Top : Page
    {
        public ObservableCollection<Movie> movies { get; set; }
        public ObservableCollection<Movie> addMovies { get; set; }
        public int page { get; set; }
        public Movie movieDetails { get; set; }
        public Top()
        {
            this.InitializeComponent();
            movies = new ObservableCollection<Movie>();
            addMovies = new ObservableCollection<Movie>();
            MovieDetailsGrid.Visibility = Visibility.Collapsed;
            page = 1;
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
            MoviesGrid.Visibility = Visibility.Collapsed;

            MyProgresRing.IsActive = true;
            MyProgresRing.Visibility = Visibility.Visible;
            
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


            MyProgresRing.IsActive = false;
            MyProgresRing.Visibility = Visibility.Collapsed;
            MovieDetailsGrid.Visibility = Visibility.Visible;
        }

        private async void MoreMovies_Click(object sender, RoutedEventArgs e)
        {
            page = page + 1;
            await MovieApi.TopRatedMovieToListAsync(addMovies, page);
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
                    PopToast();
                }
            }
            catch (Exception)
            {
                addToCollection.Content = "Can't add movie to collection";
            }
        }

        private void backToList_Click(object sender, RoutedEventArgs e)
        {
            MyFrame.Navigate(typeof(Top));
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
                            new AdaptiveText(){Text = $"U added {movieDetails.title} to your collections!"},
                            new AdaptiveText(){Text = $"Go to collection and check this out!"}
                        }
                    }
                }
            };
        }
    }
}
