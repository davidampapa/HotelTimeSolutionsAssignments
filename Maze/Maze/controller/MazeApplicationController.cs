using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Maze.Model;

namespace Maze.Controller
{
    class MazeApplicationController
    {
        private FileLoader fileLoader;
        private GameController gameController;
        public MazeApplicationController() {
            fileLoader = new FileLoader();
        }
        public void RunApplication() {
            MazeMap maze = fileLoader.LoadFromFile();
            gameController = new GameController(maze);
            gameController.Run();
            maze.PrintToConsole();
        }
    }
}
