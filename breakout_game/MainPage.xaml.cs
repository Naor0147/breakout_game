using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace breakout_game
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public settings_class settings_Data;

        public MainPage()
        {
            this.InitializeComponent();
        }


        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (settings_Data == null)
            {
                settings_Data = new settings_class();
            }
            Frame.Navigate(typeof(login_page),settings_Data);
        }

        private void Start_button_Click(object sender, RoutedEventArgs e)
        {
            if (settings_Data==null)
            {
                settings_Data = new settings_class();
            }
            Frame.Navigate(typeof(Game_Page),settings_Data);
        }

        private void Settings_button_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(Setting_page),settings_Data);
        }
        protected override void OnNavigatedTo (NavigationEventArgs e)
        {
            settings_Data = e.Parameter as settings_class;

            if (settings_Data != null)
            {
                messege_box.Text = "work "+settings_Data.Ball_type+" "+settings_Data.Difficulty + " name: " + settings_Data.Name;
            }
            else
            {
                settings_Data = new settings_class();
            }

        }


       
    }
}
