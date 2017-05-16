using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite.Net;
using SQLite.Net.Platform.WinRT;
using System.IO;
using Windows.Storage;
using System.Diagnostics;
using WhatMovie.Models;

namespace WhatMovie.Storage
{
    class dbConnection
    {
        public static SQLiteConnection connectionDB
        {
            get
            {
                return new SQLiteConnection(
                    new SQLitePlatformWinRT(),
                    Path.Combine(ApplicationData.Current.LocalFolder.Path, "Storage.sqlite"));
            }
        }

        public class DebugTraceListener : ITraceListener
        {
            public void Receive(string message)
            {
                Debug.WriteLine(message);
            }
        }

        public static void createDb()
        {
            using(var db = connectionDB)
            {
                //Movie
                var movie = db.CreateTable<Movie>();
                var movieInfo = db.GetMapping(typeof(Movie));
            }
        }
    }
}
