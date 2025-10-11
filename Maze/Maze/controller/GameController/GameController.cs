using Maze.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Maze.View;
namespace Maze.Controller
{
    public class GameController
    {
        private MazeMap _maze;
        private readonly List<DwarfBase> _dwarfs = new List<DwarfBase>();
        //private readonly Spawner _spawner;
        private readonly ConsoleRenderer _renderer;
        private int _gameTick = 0;


        public GameController(MazeMap maze)
        {
            _maze = maze;
            _renderer = new ConsoleRenderer(maze);
        }

        private void SpawnDwarfInMaze(DwarfBase dwarf)
        {
            _dwarfs.Add(dwarf);
            _maze._grid[_maze.Start.Row, _maze.Start.Col] = CellType.Dwarf;
        }

        public void Run()
        {
            SpawnDwarfInMaze(new BfsDwarf(_maze));
            while (true)
            {
                Console.SetCursorPosition(0, 0);
                if (_gameTick == 50)
                    SpawnDwarfInMaze(new RandomDwarf(_maze));
                
                bool alldone = true;
                foreach (var dwarf in _dwarfs) 
                {
                    var oldPos = dwarf.Position;
                    _maze._grid[oldPos.Row, oldPos.Col] = CellType.Empty;

                    Point? newPos = dwarf.Move();
                    
                    if (newPos == null) break;

                    if(!dwarf.Finished) alldone = false;
                    _maze._grid[newPos.Value.Row, newPos.Value.Col] = CellType.Dwarf;
                }
                Console.Clear();
                Console.WriteLine("\x1b[3J");
                if (alldone) break;
                _renderer.PrintToConsole();
                Thread.Sleep(250);

                _gameTick++;
            }

            Console.WriteLine("Hotovo. Všichni trpaslíci v cíli.");
        }
    }
}