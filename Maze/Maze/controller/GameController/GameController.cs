using System;
using System.Collections.Generic;
using System.Threading;
using Maze.Model;
using Maze.View;
using Maze.controller;

namespace Maze.Controller
{
    public class GameController
    {
        private readonly List<DwarfBase> _dwarfs = new List<DwarfBase>();
        private readonly ConsoleRenderer _renderer;
        private readonly DwarfSpawner _spawner;
        private readonly MazeMap _maze;

        public GameController(MazeMap maze)
        {
            _maze = maze;
            _renderer = new ConsoleRenderer(maze);
            _spawner = new DwarfSpawner(_dwarfs, _maze);
        }

        public void Run()
        {
            while (true)
            {
                SpawnIfNeeded();

                bool allDone = StepAllDwarfs();

                RenderFrame();

                if (allDone) break;

                Thread.Sleep(250);
            }

            Console.WriteLine("Hotovo. Všichni trpaslíci v cíli.");
        }


        private void SpawnIfNeeded()
        {
            if (_spawner.SpawnDwarf())
                SetDwarf(_maze.Start); // na start vlož „D“
        }

        private bool StepAllDwarfs()
        {
            bool allFinished = true;

            for (int i = 0; i < _dwarfs.Count; i++)
            {
                var dwarf = _dwarfs[i];
                bool finishedNow = StepOneDwarf(dwarf);

                if (!finishedNow) allFinished = false;
                else Console.WriteLine("Trpaslík: " + i + " je hotov");
            }

            return allFinished;
        }

        /// <summary>
        /// Provede jeden „tick“ pro daného trpaslíka, vrací true pokud je hotový.
        /// </summary>
        private bool StepOneDwarf(DwarfBase dwarf)
        {
            var oldPos = dwarf.Position;

            // smazat starou stopu
            ClearCell(oldPos);

            // posun
            Point? newPos = dwarf.Move();
            if (newPos == null)
            {
                // trpaslík skončil; obnov speciální buňky pokud je opouštěl
                RestoreSpecialCells(oldPos, null);
                return true;
            }

            // zápis nové pozice
            SetDwarf(newPos.Value);

            // pokud opustil Start, vrať tam 'S'; pokud skončil ve Finish a končí, vrať 'F'
            RestoreSpecialCells(oldPos, newPos);

            // je hotový?
            return dwarf.Finished;
        }

        private void RenderFrame()
        {
            Console.Clear();
            Console.WriteLine("\x1b[3J");
            _renderer.PrintToConsole();
        }

        private void ClearCell(Point p)
        {
            _maze._grid[p.Row, p.Col] = CellType.Empty;
        }

        private void SetDwarf(Point p)
        {
            _maze._grid[p.Row, p.Col] = CellType.Dwarf;
        }

        /// <summary>
        /// 
        /// </summary>
        private void RestoreSpecialCells(Point oldPos, Point? newPos)
        {
            // opustil start?
            if (oldPos.Equals(_maze.Start) && (newPos.HasValue && !newPos.Value.Equals(_maze.Start)))
            {
                _maze._grid[_maze.Start.Row, _maze.Start.Col] = CellType.Start;
            }

            // skončil ve finiši?
            if (oldPos.Equals(_maze.Finish) && !newPos.HasValue)
            {
                _maze._grid[_maze.Finish.Row, _maze.Finish.Col] = CellType.Finish;
            }
        }
    }
}
