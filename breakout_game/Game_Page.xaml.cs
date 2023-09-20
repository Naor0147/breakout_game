using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Windows.UI.Xaml.Shapes;
using System.Windows.Input;
using static System.Net.Mime.MediaTypeNames;
using System.Net.Sockets;
using Windows.System;
using System.Diagnostics;
using Windows.ApplicationModel.VoiceCommands;
using breakout_game.users_service;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace breakout_game
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class Game_Page : Page
    {
        private Rectangle myRectangle;
        public DispatcherTimer timer;
        public double x=600;
        //public _block[] _arr;
        public List<_block> _block_list;

        public _ball _Ball_obj;

        public settings_class settings_Data;

        int ms =0 ,sec = 0, min = 0;
        double time = 0;
        public Game_Page()
        {
            this.InitializeComponent();


            //build the Rectangle
            myRectangle = new Rectangle
            {
                Width = 100,
                Height = 20,
                Tag = "Player",
                Fill= new SolidColorBrush(Colors.Purple)
            };
            Canvas_Game.Children.Add(myRectangle);
            Canvas.SetLeft(myRectangle, x);
            Canvas.SetTop(myRectangle, 600);

            _block_list = new List<_block>();

            Color[] colorarr = { Colors.White, Colors.Blue, Colors.Red, Colors.Purple };
            for (int i = 0; i < 4; i++)
            {
                for (int j = 1; j < 2; j++)
                {
                    _block_list.Add(new _block(70, 30, colorarr[i], 2, Canvas_Game,100+ j * 200,( i*35 +200), 5));

                }

            }




            //timer
            timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromMilliseconds(5);
            timer.Tick += Timer_Tick; 
            timer.Start();

            Window.Current.CoreWindow.KeyDown += CoreWindow_KeyDown; 



        }

        //rectangle movement 
        private void CoreWindow_KeyDown(Windows.UI.Core.CoreWindow sender, Windows.UI.Core.KeyEventArgs args)
        {

            if (args.VirtualKey == VirtualKey.D)
            {

                x += 25;
                if (Canvas_Game.ActualWidth < x)
                {
                    x = -50;
                }
            }
            else if (args.VirtualKey == VirtualKey.A)
            {
                x -= 25;
                if (x < -100)
                {
                    x = Canvas_Game.ActualWidth - 100;
                }
            }
            Canvas.SetLeft(myRectangle, x);


        }
        //timer when you start the start the game 

        public void change_arr_op()
        {
            for (int i = 0; i < _block_list.Count; i++)
            {
                _block_list[i].dx_op_change();
            }
        }

        public void check_if_win()
        {
            var rectangles = Canvas_Game.Children.OfType<Rectangle>().ToList();
            int countOfBlocks = rectangles.Count(rectangle => rectangle.Tag?.ToString() == "block");
            if (countOfBlocks==0 )
            {
                Debug.WriteLine("Win");
                timer.Stop();

                breakout_game.users_service.Service1Client proxy = new breakout_game.users_service.Service1Client();
                Stats_Class stats = new Stats_Class();
                stats.username = settings_Data.Name;
                stats.difficulty = settings_Data.Difficulty.ToString();
               // proxy.AddStastAsync(());


                Frame.Navigate(typeof(MainPage));
            }

        }


        private void Timer_Tick(object sender, object e)
        {
            for (int i = 0; i < _block_list.Count; i++)
            {
                if (_block_list[i].move_block())
                {
                    change_arr_op();
                }
                
            }
            _Ball_obj.move();

            //timer 
            _Time.Text = _Calc_time(5);
            check_if_win();        
        }

        //Update the time 
        public string _Calc_time(int add_ms)
        {
            time += 0.05;
            ms += add_ms;
            if (ms >= 100)
            {
                sec++;
                ms = 0;
            }
            if (sec < 10)
            {
                return min + ":0" + sec + ":" + ms;
            }
            if (sec > 59)
            {
                min++;
                sec = 0;
            }
            return min + ":" + sec + ":" + ms;
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            settings_Data = e.Parameter as settings_class;

            if (settings_Data != null)
            {
                Debug.WriteLine( "work " + settings_Data.Ball_type + " " + settings_Data.Difficulty);
                _Ball_obj = new _ball(settings_Data.Ball_type, 600, 400, 10, 50, Canvas_Game);

            }

        }

    }
}
