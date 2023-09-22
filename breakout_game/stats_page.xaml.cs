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
using System.Diagnostics;
using System.Text.RegularExpressions;
// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace breakout_game
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class stats_page : Page
    {
        private settings_class settings_Data;

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
        public async void getMyUser()
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

        private void Go_Back_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(MainPage), settings_Data);

        }
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            settings_Data = e.Parameter as settings_class;

            if (settings_Data != null)
            {
                Debug.WriteLine("work " + settings_Data.Ball_type + " " + settings_Data.Difficulty + " name: " + settings_Data.Name);
            }

        }

        private void comboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox comboBox = (ComboBox)sender;
            ComboBoxItem selectedItem = (ComboBoxItem)comboBox.SelectedItem;
            string selectedText = selectedItem.Content.ToString();

            switch (selectedText)
            {
                case "show all stats":
                    getAllusers();
                    break;
                
                case "show only my stats":
                    GetuserStats(settings_Data); break;
                case "show Max Stats":
                    showMaxStats(); break;
                case "Avg Score":
                    AvgScore();
                    break;
                case "count Users":
                    CountScore();
                    break;
                case "Sum score":
                    SumScore();
                    break;
                case "inner join all stats":
                    innerStatsUsers(); break;   
                default:
                    break;

            }

            // Do something with the selected item (e.g., display it, perform an action).
        }
        public async void AvgScore()
        {
            breakout_game.users_service.Service1Client proxy = new breakout_game.users_service.Service1Client();
            double stats = await proxy.AvgScoreAsync();
            List<string> stats_string = new List<string>();
            stats_string.Add("the avg is: " + stats);

            lvDataBinding.ItemsSource = stats_string;
        }
        public async void SumScore()
        {
            breakout_game.users_service.Service1Client proxy = new breakout_game.users_service.Service1Client();
            double stats = await proxy.TotalScoreAsync();
            List<string> stats_string = new List<string>();
            stats_string.Add("the sum score is: " + stats);

            lvDataBinding.ItemsSource = stats_string;
        }
        public async void CountScore()
        {
            breakout_game.users_service.Service1Client proxy = new breakout_game.users_service.Service1Client();
            double stats = await proxy.countUsersAsync();
            List<string> stats_string = new List<string>();
            stats_string.Add("how many people won in the game: " + stats);

            lvDataBinding.ItemsSource = stats_string;
        }



        public async void GetuserStats(settings_class x)
        {
            breakout_game.users_service.Service1Client proxy = new breakout_game.users_service.Service1Client();
            ObservableCollection<Stats_Class> stats = await proxy.ShowUserStastAsync(x.Name);
            List<string> stats_string = new List<string>();
            foreach (Stats_Class item in stats)
            {
                stats_string.Add(to_str(item));
            }

            lvDataBinding.ItemsSource = stats_string;
        }
        public async void innerStatsUsers()
        {
            breakout_game.users_service.Service1Client proxy = new breakout_game.users_service.Service1Client();
            ObservableCollection<innerJoin> stats = await proxy.sortByStatsAsync();
            List<string> stats_string = new List<string>();
            foreach (innerJoin item in stats)
            {
                string x = to_str(item.stats)+ " password: "+ item.password;
                stats_string.Add(x);
            }

            lvDataBinding.ItemsSource = stats_string;
        }
        public async void showMaxStats()
        {
            breakout_game.users_service.Service1Client proxy = new breakout_game.users_service.Service1Client();
            ObservableCollection<Stats_Class> stats = await proxy.showMaxstastAsync();
            List<string> stats_string = new List<string>();
            foreach (Stats_Class item in stats)
            {
                stats_string.Add(to_str(item));
            }

            lvDataBinding.ItemsSource = stats_string;
        }
    }
}
