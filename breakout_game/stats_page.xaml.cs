using breakout_game.users_service;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Xml.Linq;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using System.Linq;
using System.Collections.ObjectModel;
using System.Data.SqlClient;
// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace breakout_game
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class stats_page : Page
    {
        public stats_page()
        {
            this.InitializeComponent();
            List<int> ints = new List<int>() { 3, 2, 1, 3, 5 };
            lvDataBinding.ItemsSource = ints;
            getAllusers();
           
        }

        public string to_str(Stats_Class stats_)
        {
            return "Name: " + stats_.username + " Score: " + stats_.score + " Time: " + stats_.time + " Difficulty: " + stats_.difficulty;
        }

        public async void getAllusers()
        {
            breakout_game.users_service.Service1Client proxy = new breakout_game.users_service.Service1Client();
            ObservableCollection<Stats_Class> stats = await proxy.ShowListStastAsync();
            List<string> stats_string = new List<string>();
            foreach (Stats_Class item in stats)
            {
                stats_string.Add(to_str(item));
            }

            lvDataBinding.ItemsSource = stats_string;
        }
       
    }
}
