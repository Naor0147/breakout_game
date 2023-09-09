using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Shapes;
using Rectangle = Windows.UI.Xaml.Shapes.Rectangle;

namespace breakout_game
{
    public class _block
    {
        //size
        public double _width;
        public double _height;

        //postion
        private double _x { get; set; }
        private double _y { get; set; }

        //move
        private double _dx { get; set; }


        public Windows.UI.Color _color;

        //times to get hit to be destoryed 
        public int _hp;

        //Canvas in use 
        private Canvas _canvas;

        public Rectangle _myRectangle { get; set; }


        public _block(double width, double height, Windows.UI.Color color, int hP, Canvas canvas , double x,double y,double dx)
        {
            //Initialize
            this._width = width;
            this._height = height;
            this._color = color;
            this._hp = hP;
            this._canvas = canvas;
            this._x = x;
            this._y = y;
            this._dx = dx;

            //create the cube 
            _myRectangle = new Rectangle
            {
                Width = 100,
                Height = 20,

                Fill = new SolidColorBrush(color)
            };
            Canvas.SetLeft(_myRectangle, _x);
            Canvas.SetTop(_myRectangle, _y);

            _myRectangle.Tag = "block";

            _canvas.Children.Add(_myRectangle);

        }
        public void dx_op_change()
        {
            _dx = -_dx;
        }
        
        public bool move_block() 
        {
            bool _hit = false;
            if (_x+_dx+_width>_canvas.ActualWidth || _x+_dx<0)
            {
               // _dx = -_dx;
                return true;// if the block hit the wall it will send back true so it will know to cahnge all op dx
            }
            _x += _dx;
            Canvas.SetLeft(_myRectangle, _x);
            return _hit;

        }

        
        
    }
}
