using System;
using System.Collections.Generic;
using System.Threading;
using Maze.Model;
using Maze.View;
using Maze.controller;

namespace Maze.Controller
{
    /// <summary>
    /// Controls the main game loop, manages dwarfs, and handles maze rendering.
    /// </summary>
    public class GameController
    {
        /// <summary>
        /// List of all dwarfs in the game.
        /// </summary>
        private readonly List<DwarfBase> _dwarfs = new List<DwarfBase>();

        /// <summary>
        /// Renderer for displaying the maze in the console.
        /// </summary>
        private readonly ConsoleRenderer _renderer;

        /// <summary>
        /// Responsible for spawning dwarfs into the maze.
        /// </summary>
        private readonly DwarfSpawner _spawner;

        /// <summary>
        /// The maze instance used in the game.
        /// </summary>
        private readonly MazeMap _maze;

        /// <summary>
        /// Initializes a new instance of <see cref="GameController"/> for the specified maze.
        /// </summary>
        /// <param name="maze">The maze to be used in the game.</param>
        public GameController(MazeMap maze)
        {
            _maze = maze;
            _renderer = new ConsoleRenderer(maze);
            _spawner = new DwarfSpawner(_dwarfs, _maze);
        }

        /// <summary>
        /// Runs the main game loop until all dwarfs reach the finish.
        /// </summary>
        public void Run()
        {
            while (true)
            {
                SpawnIfNeeded();

                bool allDone = StepAllDwarfs();

                RenderFrame();

                if (allDone) break;

                Thread.Sleep(100);
            }

            Console.WriteLine("Done. All dwarfs have reached the goal.");
        }

        /// <summary>
        /// Attempts to spawn a new dwarf if the spawner conditions are met.
        /// </summary>
        private void SpawnIfNeeded()
        {
            if (_spawner.SpawnDwarf())
                SetDwarf(_maze.Start);
        }

        /// <summary>
        /// Performs one step for all dwarfs.
        /// </summary>
        /// <returns>True if all dwarfs have finished; otherwise, false.</returns>
        private bool StepAllDwarfs()
        {
            bool allFinished = true;

            for (int i = 0; i < _dwarfs.Count; i++)
            {
                var dwarf = _dwarfs[i];
                bool finishedNow = StepOneDwarf(dwarf);

                if (!finishedNow) allFinished = false;
            }

            return allFinished;
        }

        /// <summary>
        /// Performs one tick for the specified dwarf.
        /// </summary>
        /// <param name="dwarf">The dwarf to move.</param>
        /// <returns>True if the dwarf has finished; otherwise, false.</returns>
        private bool StepOneDwarf(DwarfBase dwarf)
        {
            var oldPos = dwarf.Position;

            ClearCell(oldPos);

            Point? newPos = dwarf.Move();
            if (newPos == null)
            {
                RestoreSpecialCells(oldPos, null);
                return true;
            }

            SetDwarf(newPos.Value);

            RestoreSpecialCells(oldPos, newPos);

            return dwarf.Finished;
        }

        /// <summary>
        /// Renders the current state of the maze to the console.
        /// </summary>
        private void RenderFrame()
        {
            Console.Clear();
            Console.WriteLine("\x1b[3J");
            _renderer.PrintToConsole();
        }

        /// <summary>
        /// Clears the cell at the specified position (sets it to <see cref="CellType.Empty"/>).
        /// </summary>
        /// <param name="p">The position of the cell to clear.</param>
        private void ClearCell(Point p)
        {
            _maze._grid[p.Row, p.Col] = CellType.Empty;
        }

        /// <summary>
        /// Sets the cell at the specified position to <see cref="CellType.Dwarf"/>.
        /// </summary>
        /// <param name="p">The position to set as a dwarf.</param>
        private void SetDwarf(Point p)
        {
            _maze._grid[p.Row, p.Col] = CellType.Dwarf;
        }

        /// <summary>
        /// Restores special cells (start/finish) if a dwarf leaves them.
        /// </summary>
        /// <param name="oldPos">The previous position of the dwarf.</param>
        /// <param name="newPos">The new position of the dwarf, or null if finished.</param>
        private void RestoreSpecialCells(Point oldPos, Point? newPos)
        {
            if (oldPos.Equals(_maze.Start) && (newPos.HasValue && !newPos.Value.Equals(_maze.Start)))
            {
                _maze._grid[_maze.Start.Row, _maze.Start.Col] = CellType.Start;
            }

            if (oldPos.Equals(_maze.Finish) && !newPos.HasValue)
            {
                _maze._grid[_maze.Finish.Row, _maze.Finish.Col] = CellType.Finish;
            }
        }
    }
}
