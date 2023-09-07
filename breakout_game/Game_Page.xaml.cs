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


        int ms =0 ,sec = 0, min = 0;

        public Game_Page()
        {
            this.InitializeComponent();
            

            //build the Rectangle
            myRectangle = new Rectangle
            {
                Width = 100,
                Height = 50,
                
                Fill= new SolidColorBrush(Colors.Blue)
            };
            Canvas_Game.Children.Add(myRectangle);
            Canvas.SetLeft(myRectangle, x);
            Canvas.SetTop(myRectangle, 600);

            _block_list = new List<_block>();
            for (int i = 0; i < 4; i++)
            {
               _block_list.Add( new _block(70, 30, Colors.White, 2, Canvas_Game, i*200, 200, 5));

            }

             _Ball_obj = new _ball(ball_type.blue_ball,100,100,5,50,Canvas_Game);



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

                x += 10;
                if (Canvas_Game.ActualWidth < x)
                {
                    x = -50;
                }
            }
            else if (args.VirtualKey == VirtualKey.A)
            {
                x -= 10;
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

        }

        //Update the time 
        public string _Calc_time(int add_ms)
        {
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
            settings_class settings_Data = e.Parameter as settings_class;

            if (settings_Data != null)
            {
                Debug.WriteLine( "work " + settings_Data.Ball_type + " " + settings_Data.Difficulty);
            }

        }

    }
}
