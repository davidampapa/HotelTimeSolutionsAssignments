using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace Maze.Model
{
    class RandomDwarf : DwarfBase
    {
        private MazeMap _maze;
        private int _delay;
        private int _ticks = 0;

        public RandomDwarf(MazeMap maze) 
        {
            _maze = maze;
            Position = maze.Start;
            _delay = new Random().Next(5, 40);
        }
        override public Point? Move()
        {
            if (Finished) return null;
            _ticks++;

            if (_ticks >= _delay)
            {
                Position = _maze.Finish;
                Finished = true;
            }

            return Position;
        }
    }
}
