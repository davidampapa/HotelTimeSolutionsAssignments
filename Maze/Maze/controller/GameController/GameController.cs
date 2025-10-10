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
        private readonly Random _rng = new Random();

        public GameController(MazeMap maze)
        {
            _maze = maze;
            //_spawner = new Spawner(maze);
            _renderer = new ConsoleRenderer(maze);
        }

        public void Run()
        {
            _dwarfs.Add(new BfsDwarf(_maze));
            _maze._grid[_maze.Start.Row, _maze.Start.Col] = CellType.Dwarf;
            while (true)
            {
                var oldPos = _dwarfs[0].Position;
                _maze._grid[oldPos.Row, oldPos.Col] = CellType.Empty;

                Point? newPos = _dwarfs[0].Move();
                if (newPos == null) break;

                _maze._grid[newPos.Value.Row, newPos.Value.Col] = CellType.Dwarf;

                Console.Clear();
                _renderer.PrintToConsole();

                Thread.Sleep(100);
            }

            //Console.SetCursorPosition(0, _maze.Rows + 1);
            Console.WriteLine("Hotovo. Všichni trpaslíci v cíli.");
        }
    }
}
// spawn
//_spawner.TrySpawn(_dwarfs, _rng);

// krok každého trpaslíka
/*   for (int i = 0; i < _dwarfs.Count; i++)
   {
       var d = _dwarfs[i];
       if (!d.Finished)
       {
           d.Step(_maze, _rng);
           _renderer.UpdateDwarf(d);
       }
   }

   // ukončení: všichni ve finiši a už není co spawnovat
   bool allDone = _spawner.AllSpawned;
   for (int i = 0; i < _dwarfs.Count && allDone; i++)
       if (!_dwarfs[i].Finished) allDone = false;

   if (allDone) break;
   */