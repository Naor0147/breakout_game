using System;
using System.Collections.Generic;
using System.Diagnostics;
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
        private string[] _imgArr = new string[] { "ms-appx:///pics/blue_ball.png", "ms-appx:///pics/green_ball.png" , "ms-appx:///pics/red_ball.png" };
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
        double _vx=2.5;
        double _vy=2.5;

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
            Color _color_ball = Colors.Blue;
            if (_ball_Type ==ball_type.green_ball)
            {
                _color_ball= Colors.Green;
            }
            else if (_ball_Type==ball_type.red_ball)
            {
                _color_ball = Colors.Red;

            }

            // ball look
            _ball_obj.Fill = new SolidColorBrush(_color_ball);
            _ball_obj.HorizontalAlignment = HorizontalAlignment.Left;
            _ball_obj.VerticalAlignment = VerticalAlignment.Center;
            _ball_obj.Width = 50;
            _ball_obj.Height = 50;

            // add to canvas
            Canvas.SetLeft(_ball_obj, _x);
            Canvas.SetTop(_ball_obj, _y);
            _canvas.Children.Add(_ball_obj);
            

        }


     
        public void change_speed(double dis,double length_rec)
        {
            // the +5 is for safty
            //the the max dis is size of the ball +5
            //the min ball-5 and the max is the length of the rectangle + ball +5 
            
            double all_dis = _size + length_rec;
            dis += _size;
            double ratio = all_dis / dis;

            if (ratio>3)
            {
                _vy = -_movement_speed * 0.7;
                _vx = -_movement_speed * 0.3;
            }
            else if (ratio <1.5)
            {
                _vy = -_movement_speed * 0.7;
                _vx = _movement_speed * 0.3;
            }
            else
            {
                _vy = -_movement_speed;
                _vx = 0;

            }
        }

        public void move()
        {
            Rect rectangle;
            Rect ball_rect = new Rect(_x, _y, _size, _size);
            foreach (var temp_obj in _canvas.Children.OfType<Rectangle>())
            {
                rectangle = new Rect((double)Canvas.GetLeft(temp_obj), Canvas.GetTop(temp_obj), temp_obj.Width, temp_obj.Height);
                Rect _rectHelp=RectHelper.Intersect(rectangle, ball_rect);
                if (_rectHelp.Width > 0 || _rectHelp.Height > 0)
                {
                    if ((string)temp_obj.Tag == "Platfrom")
                    {
                        _vy = -_vy;
                    }
                    else if ((string)temp_obj.Tag == "Player")
                    {
                        change_speed(_x - Canvas.GetLeft(temp_obj), temp_obj.Width);

                    }
                    else
                    {
                        change_speed(_x - Canvas.GetLeft(temp_obj), temp_obj.Width);
                        _canvas.Children.Remove(temp_obj);
                    }
                }
            }
            _y += _vy;
            if (_x + _vx + _size > _canvas.ActualWidth || _x + _vx < 0)
            {
                _vx =- _vx;
            }
            _x += +_vx;
            Canvas.SetTop(_ball_obj, _y);
            Canvas.SetLeft(_ball_obj, _x);
        }

    }
}
