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

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace WhatMovie
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
            MyFrame.Navigate(typeof(Home));
            TitleTextBlock.Text = "Home";
            BackButton.Visibility = Visibility.Collapsed;
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
                TitleTextBlock.Text = "Top10";
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
    }
}
