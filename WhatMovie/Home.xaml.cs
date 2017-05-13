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
        public Home()
        {
            this.InitializeComponent();

            movies = new ObservableCollection<Movie>();
        }

        private async void Page_Loaded(object sender, RoutedEventArgs e)
        {
            MyProgresRing.IsActive = true;
            MyProgresRing.Visibility = Visibility.Visible;
            await MovieApi.MovieDataToListAsync(movies);
            MyProgresRing.IsActive = false;
            MyProgresRing.Visibility = Visibility.Collapsed;
        }

        /*private async void GetData()
        {
            Movie myMovie = await MovieApi.GetMovie("Blue");
            Title.Text = myMovie.title;
            ReleaseDate.Text = myMovie.release_date;
            string poster = String.Format("http://image.tmdb.org/t/p/w185/wzLtkJbUQoI2dEtBgxRYRmPUIV9.jpg");
            ResultImage.Source = new BitmapImage(new Uri(poster, UriKind.Absolute));
        }*/

    }
}
