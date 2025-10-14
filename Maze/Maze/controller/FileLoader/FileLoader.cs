using Maze.Model;
using System;
using System.IO;

namespace Maze.Controller
{
    /// <summary>
    /// Loads a maze from a text file and constructs a <see cref="MazeMap"/>.
    /// </summary>
    /// <remarks>
    /// Expects a file named <c>Maze.dat</c> under the <c>Data</c> folder at the project root.
    /// Each line must have the same length.
    /// </remarks>
    class FileLoader
    {
        /// <summary>
        /// Relative folder that contains the maze file (from the project root).
        /// </summary>
        private readonly string folder = "Data";

        /// <summary>
        /// Maze file name to load.
        /// </summary>
        private readonly string fileName = "Maze.dat";

        /// <summary>
        /// Initializes a new instance of <see cref="FileLoader"/>.
        /// </summary>
        public FileLoader() { }

        /// <summary>
        /// Reads the maze file, validates its structure, and builds a <see cref="MazeMap"/>.
        /// </summary>
        /// <returns>A fully populated <see cref="MazeMap"/> with grid, start, and finish.</returns>
        /// <exception cref="Exception">
        /// Thrown when the file is not found, the file is empty, line lengths are inconsistent,
        /// or when either the start (<c>S</c>) or finish (<c>F</c>) marker is missing.
        /// </exception>
        /// <example>
        /// Typical usage:
        /// <code>
        /// var loader = new FileLoader();
        /// MazeMap map = loader.LoadFromFile();
        /// </code>
        /// </example>
        public MazeMap LoadFromFile()
        {
            // read file
            var fullPath = Path.Combine(
                Directory.GetParent(AppDomain.CurrentDomain.BaseDirectory).Parent.Parent.FullName,
                folder,
                fileName);

            if (!File.Exists(fullPath))
                throw new Exception("Soubor nenalezen: " + fullPath);

            var rawLines = File.ReadAllLines(fullPath);

            var lines = Array.FindAll(rawLines, l => !string.IsNullOrWhiteSpace(l));
            if (lines.Length == 0) throw new Exception("Soubor je prázdný.");

            // check if the maze is valid
            int cols = lines[0].Length;
            for (int r = 0; r < lines.Length; r++)
                if (lines[r].Length != cols)
                    throw new Exception($"Řádek {r + 1} má jinou délku ({lines[r].Length} vs {cols}).");

            var grid = new CellType[lines.Length, cols];
            Point? start = null, finish = null;

            // save maze chars into array 
            for (int r = 0; r < lines.Length; r++)
            {
                for (int c = 0; c < cols; c++)
                {
                    char ch = lines[r][c];
                    switch (ch)
                    {
                        case '#': grid[r, c] = CellType.Wall; break;
                        case 'S': grid[r, c] = CellType.Start; start = new Point(r, c); break;
                        case 'F': grid[r, c] = CellType.Finish; finish = new Point(r, c); break;
                        case ' ':
                        default: grid[r, c] = CellType.Empty; break;
                    }
                }
            }

            // check if there is existing start and end
            if (start == null) throw new Exception("V bludišti chybí 'S' (Start).");
            if (finish == null) throw new Exception("V bludišti chybí 'F' (Cíl).");

            return new MazeMap(grid, start.Value, finish.Value);
        }
    }
}
