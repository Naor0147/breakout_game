using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace breakout_game
{
    public class settings_class
    {
        public diffculty_level Difficulty { get; set; }
        public ball_type Ball_type { get; set; }

        public settings_class(diffculty_level difficulty, ball_type ball_type)
        {
            Difficulty = difficulty;
            Ball_type = ball_type;
        }

        public settings_class() 
        {
            Difficulty = diffculty_level.easy;
            Ball_type = ball_type.blue_ball;
        }
    }
}
