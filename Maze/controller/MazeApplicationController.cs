using Maze.Model;

namespace Maze.Controller
{
    /// <summary>
    /// Application controller that starts the game loop.
    /// </summary>
    class MazeApplicationController
    {
        /// <summary>
        /// Helper responsible for reading the maze definition from file.
        /// </summary>
        private FileLoader _fileLoader;

        /// <summary>
        /// Game controller that manages the game loop.
        /// </summary>
        private GameController _gameController;

        /// <summary>
        /// Creates a new <see cref="MazeApplicationController"/> and initializes the file loader.
        /// </summary>
        public MazeApplicationController()
        {
            _fileLoader = new FileLoader();
        }

        /// <summary>
        /// Loads the maze and runs the game simulation.
        /// </summary>
        /// <remarks>
        /// This method blocks until the game loop finishes.
        /// </remarks>
        public void RunApplication()
        {
            MazeMap maze = _fileLoader.LoadFromFile();
            _gameController = new GameController(maze);
            _gameController.Run();
        }
    }
}
