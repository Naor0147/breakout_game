using System;
using System.Collections.Generic;
using System.Diagnostics;
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

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

public enum diffculty_level
{
    hard,normal,easy
}
public enum ball_type
{
    blue_ball , red_ball, green_ball,
}
namespace breakout_game
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class Setting_page : Page
    {
        public ball_type Ball_type_settings { get; set; }
        public diffculty_level Diffculty_Level_settings { get; set; }
        public settings_class settings_Data { get; set; }  
        public Setting_page()
        {
            this.InitializeComponent();
          
        }

        private void save_settings_Click(object sender, RoutedEventArgs e)
        {
            if (blue_ball_RB.IsChecked.Value)
            {
                Ball_type_settings = ball_type.blue_ball;
            }
            else if (red_ball_RB.IsChecked.Value)
            {
                Ball_type_settings = ball_type.red_ball;
            }
            else
            {
                Ball_type_settings = ball_type.green_ball;
            }

            if (Hard_RD.IsChecked.Value)
            {
                Diffculty_Level_settings = diffculty_level.hard;
            }
            else if (Normal_RD.IsChecked.Value)
            {
                Diffculty_Level_settings = diffculty_level.normal;
            }
            else
            {
                Diffculty_Level_settings = diffculty_level.easy;

            }
            settings_Data.Difficulty = Diffculty_Level_settings;
            settings_Data.Ball_type = Ball_type_settings;

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


    }
}
