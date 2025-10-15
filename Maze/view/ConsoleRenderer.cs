using System;
using Maze.Model;

namespace Maze.View
{
    /// <summary>
    /// Renders the maze into the console.
    /// </summary>
    class ConsoleRenderer
    {
        /// <summary>
        /// Maze data to render.
        /// </summary>
        private readonly MazeMap _maze;

        /// <summary>
        /// Configures the console window and buffer to match the maze dimensions,
        /// ensuring the entire maze can be rendered without scrolling.
        /// </summary>
        /// <param name="desiredCols">The desired width of the console in characters (columns).</param>
        /// <param name="desiredRows">The desired height of the console in lines (rows).</param>
        /// <remarks>
        private void SetupConsole(int desiredCols, int desiredRows)
        {
            int cols = Math.Min(desiredCols, Console.LargestWindowWidth);
            int rows = Math.Min(desiredRows, Console.LargestWindowHeight);

            try
            {
                int bufW = Math.Max(cols, Console.BufferWidth);
                int bufH = Math.Max(rows, Console.BufferHeight);
                if (Console.BufferWidth != bufW || Console.BufferHeight != bufH)
                    Console.SetBufferSize(bufW, bufH);
                
                int winW = Math.Min(cols, Console.BufferWidth);
                int winH = Math.Min(rows, Console.BufferHeight);
                if (Console.WindowWidth != winW || Console.WindowHeight != winH)
                    Console.SetWindowSize(winW, winH);
            }
            catch
            {
            }

            try { Console.CursorVisible = false; } catch { }
        }

        /// <summary>
        /// Creates a <see cref="ConsoleRenderer"/> bound to a specific maze
        /// and prepares the console for rendering.
        /// </summary>
        /// <param name="maze">The maze to render.</param>
        public ConsoleRenderer(MazeMap maze)
        {
            _maze = maze;
            SetupConsole(maze.Cols, maze.Rows);
        }

        /// <summary>
        /// Prints the current maze grid to the console.
        /// </summary>
        public void PrintToConsole()
        {
            _maze.PrintToConsole();
        }
    }
}
