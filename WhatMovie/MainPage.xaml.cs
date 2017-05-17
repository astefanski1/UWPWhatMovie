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
using WhatMovie.Storage;
using System.Collections.ObjectModel;
using WhatMovie.Rest;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace WhatMovie
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public ObservableCollection<Movie> searchedMovies { get; set; }
        private List<String> Suggestions { get; set; }
        public MainPage()
        {
            this.InitializeComponent();
            MyFrame.Navigate(typeof(Home));
            TitleTextBlock.Text = "Home";
            BackButton.Visibility = Visibility.Collapsed;
            dbConnection.createDb();
            searchedMovies = new ObservableCollection<Movie>();
        }

        private void HamburgerButton_Click(object sender, RoutedEventArgs e)
        {
            MySplitView.IsPaneOpen = !MySplitView.IsPaneOpen;
        }

        private void IconsListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            

            if(HomeListBoxItem.IsSelected)
            {
                MyFrame.Navigate(typeof(Home));
                TitleTextBlock.Text = "Home";
                BackButton.Visibility = Visibility.Collapsed;
            }
            if(TopListBoxItem.IsSelected)
            {
                MyFrame.Navigate(typeof(Top));
                TitleTextBlock.Text = "Top Rated";
                BackButton.Visibility = Visibility.Visible;
            }
            if(FavoriteListBoxItem.IsSelected)
            {
                MyFrame.Navigate(typeof(Favorite));
                TitleTextBlock.Text = "Favorite";
                BackButton.Visibility = Visibility.Visible;
            }

        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            if (MyFrame.CanGoBack)
            {
                MyFrame.GoBack();
                HomeListBoxItem.IsSelected = true;
            }
        }

        private async void SearchAutoSuggestBox_TextChanged(AutoSuggestBox sender, AutoSuggestBoxTextChangedEventArgs args)
        {
            await MovieApi.searchedMoviesToListAsync(searchedMovies, 1, SearchAutoSuggestBox.Text);
            
            Suggestions = searchedMovies
                .Where(p => p.title.StartsWith(sender.Text))
                .Select(p => p.title)
                .ToList();

            SearchAutoSuggestBox.ItemsSource = Suggestions;
        }

        private void SearchAutoSuggestBox_QuerySubmitted(AutoSuggestBox sender, AutoSuggestBoxQuerySubmittedEventArgs args)
        {

            Movie movieToPass = new Movie();
            foreach(var movie in searchedMovies)
            {
                if(movie.title == args.ChosenSuggestion)
                {
                    movieToPass = movie;
                }
            }
            if(movieToPass.title != null)
            {
                MyFrame.Navigate(typeof(MovieDetails), movieToPass);
            }
            
        }
    }
}
