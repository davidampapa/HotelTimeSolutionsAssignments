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
        public RandomDwarf(MazeMap maze) 
        {
        
        }
        override public Point? Move()
        {
            if (Finished) return null;

            // deterministické rozhodnutí z (id, tick)
            var key = $"teleport|id:{/*tvoje Id*/0}|tick:{_t}";
            if (HRand.ChancePermille(key, 30))   // ~3 % na tick
            {
                Position = _maze.Finish;
                Finished = true;
                return Position;
            }

            _t++;
            return Position; // nebo udělej krok, pokud má i běžný pohyb
        }
    }
}
