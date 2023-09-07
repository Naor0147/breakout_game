using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Animation;
using Windows.UI.Xaml.Shapes;

namespace breakout_game
{
    public class _ball
    {
        private string[] _imgArr = new string[] { "ms-appx:///pics/blue_ball.png", "ms-appx:///pics/cool_ball.png" , "ms-appx:///pics/red_ball.png" };
        private ball_type _ball_Type { get; set; }

        private Ellipse _ball_obj { get; set; }


        //pos
        private double _x { get; set; }
        private double _y { get; set; }

        //size
        private double _size { get; set; }
        private double _movement_speed { get;set; }

        //canbas
        private Canvas _canvas { get; set; }

        //speed
        double _vx=0;
        double _vy=0.5;

        public _ball( ball_type ball_Type, double x, double y, double movement_speed , double size ,Canvas canvas)
        {
            this._ball_Type = ball_Type;
            this._x = x;
            this._y = y;
            this._movement_speed = movement_speed;
            this._size = size;
            this._canvas = canvas;


            //add ball
            _ball_obj = new Ellipse();
            _ball_obj.Fill = new SolidColorBrush(Colors.Blue);
            _ball_obj.HorizontalAlignment = HorizontalAlignment.Left;
            _ball_obj.VerticalAlignment = VerticalAlignment.Center;
            _ball_obj.Width = 50;
            _ball_obj.Height = 50;

            Canvas.SetLeft(_ball_obj, _x);
            Canvas.SetTop(_ball_obj, _y);
            _canvas.Children.Add(_ball_obj);
            

        }

        public void move()
        {
            _y += _vy;
            Canvas.SetTop(_ball_obj, _y);
        }

    }
}
