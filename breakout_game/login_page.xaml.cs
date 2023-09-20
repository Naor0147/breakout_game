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

namespace breakout_game
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class login_page : Page
    {
        public settings_class settings_Data;
        public login_page()
        {
            this.InitializeComponent();

        }

        private void Go_Back_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(MainPage),settings_Data);
        }

        private async void login_Click(object sender, RoutedEventArgs e)
        {
            string name = (string)Name_user_box.Text;
            string password = (string)Password_user_box.Text;
            if (password== "" ||name=="")
            {
                messsege_text.Text = "One of the field is emptey ";
                return;
            }

            breakout_game.users_service.Service1Client proxy = new breakout_game.users_service.Service1Client();
            bool check =await proxy.LoginAsync(name,password);
            if (check)
            {
                settings_Data.Name = name; 
                Frame.Navigate(typeof(MainPage), settings_Data);
            }
           
            messsege_text.Text = "The password or the username is wrong";

        }

        private async void create_account_Click(object sender, RoutedEventArgs e)
        {
            string name = (string)Name_user_box.Text;
            string password = (string)Password_user_box.Text;
            if (password == "" || name == "")
            {
                messsege_text.Text = "One of the field is emptey ";
                return;
            }
            breakout_game.users_service.Service1Client proxy = new breakout_game.users_service.Service1Client();

            bool check = await proxy.AddUserAsync(name, password);
            if (check)
            {
                messsege_text.Text = "User has been created , Now use the login Button";
                return;

            }

            messsege_text.Text = "User Name already exists ";
            
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            settings_Data = e.Parameter as settings_class;

            if (settings_Data != null)
            {
                Debug.WriteLine("work " + settings_Data.Ball_type + " " + settings_Data.Difficulty);

            }


        }
    }
    
}
