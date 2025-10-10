using Maze.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maze.View
{
    class ConsoleRenderer
    {
        private readonly MazeMap _maze;
        //private readonly Dictionary<int, Point> _lastPos = new Dictionary<int, Point>();

        public ConsoleRenderer(MazeMap maze) { _maze = maze; }

        public void PrintToConsole()
        {
            _maze.PrintToConsole();
        }
    }
}
