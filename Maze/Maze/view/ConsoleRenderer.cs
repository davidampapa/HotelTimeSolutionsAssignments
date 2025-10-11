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

        void SetupConsole(int cols, int rows)
        {
            try
            {
                if (Console.BufferWidth < cols) Console.BufferWidth = cols;
                if (Console.BufferHeight < rows) Console.BufferHeight = rows;
                if (Console.WindowWidth < cols) Console.WindowWidth = cols;
                if (Console.WindowHeight < rows) Console.WindowHeight = rows;
            }
            catch {}
            Console.CursorVisible = false;
        }
        //private readonly Dictionary<int, Point> _lastPos = new Dictionary<int, Point>();

        public ConsoleRenderer(MazeMap maze) 
        {
            _maze = maze;
            SetupConsole(maze.Cols, maze.Rows);
        }

        public void PrintToConsole()
        {
            _maze.PrintToConsole();
        }
    }
}
