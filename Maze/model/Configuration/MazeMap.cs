using System;
using System.Collections.Generic;

namespace Maze.Model
{
    /// <summary>
    /// Representation of a maze.
    /// </summary>
    public class MazeMap
    {
        /// <summary>
        /// Grid of cell types.
        /// </summary>
        public CellType[,] _grid;

        /// <summary>
        /// Total number of rows in the grid.
        /// </summary>
        public int Rows { get; }

        /// <summary>
        /// Total number of columns in the grid.
        /// </summary>
        public int Cols { get; }

        /// <summary>
        /// Start position (must be within bounds).
        /// </summary>
        public Point Start { get; }

        /// <summary>
        /// Finish position (must be within bounds).
        /// </summary>
        public Point Finish { get; }

        /// <summary>
        /// Creates a <see cref="MazeMap"/> from an existing grid and start/finish coordinates.
        /// </summary>
        /// <param name="grid">2D array of <see cref="CellType"/> representing the maze layout.</param>
        /// <param name="start">Start position.</param>
        /// <param name="finish">Finish position.</param>
        public MazeMap(CellType[,] grid, Point start, Point finish)
        {
            _grid = grid;
            Rows = grid.GetLength(0);
            Cols = grid.GetLength(1);
            Start = start;
            Finish = finish;
        }

        /// <summary>
        /// Indexer to read the cell type at the given row and column.
        /// </summary>
        /// <param name="row">Row index.</param>
        /// <param name="col">Column index.</param>
        /// <returns>The <see cref="CellType"/> at the given coordinates.</returns>
        public CellType this[int row, int col] => _grid[row, col];

        /// <summary>
        /// Checks whether the coordinates lie within the grid bounds.
        /// </summary>
        public bool IsInside(int row, int col) =>
            row >= 0 && row < Rows && col >= 0 && col < Cols;

        /// <summary>
        /// Determines whether the specified cell is walkable.
        /// </summary>
        /// <param name="row">Row index.</param>
        /// <param name="col">Column index.</param>
        /// <returns><c>true</c> if the cell is inside the grid and walkable; otherwise <c>false</c>.</returns>
        public bool IsWalkable(int row, int col)
        {
            if (!IsInside(row, col)) return false;
            var cell = _grid[row, col];
            return cell == CellType.Empty || cell == CellType.Start || cell == CellType.Finish;
        }

        /// <summary>
        /// Enumerates the 4-neighborhood (up, down, left, right) of a point,
        /// yielding only walkable neighbors.
        /// </summary>
        /// <param name="p">The source point.</param>
        /// <returns>Enumerable of adjacent walkable points.</returns>
        public IEnumerable<Point> Neighbors4(Point p)
        {
            var dirs = new[] { new Point(-1, 0), new Point(1, 0), new Point(0, -1), new Point(0, 1) };
            foreach (var d in dirs)
            {
                int r = p.Row + d.Row, c = p.Col + d.Col;
                if (IsWalkable(r, c)) yield return new Point(r, c);
            }
        }

        /// <summary>
        /// Renders the maze grid to the console using characters: 
        /// <c>#</c> for walls, <c>S</c> for start, <c>F</c> for finish, <c>D</c> for dwarfs, and space for empty cells.
        /// </summary>
        public void PrintToConsole()
        {
            for (int r = 0; r < Rows; r++)
            {
                var line = new char[Cols];
                for (int c = 0; c < Cols; c++)
                {
                    char ch;
                    switch (_grid[r, c])
                    {
                        case CellType.Wall: ch = '#'; break;
                        case CellType.Start: ch = 'S'; break;
                        case CellType.Finish: ch = 'F'; break;
                        case CellType.Dwarf: ch = 'D'; break;
                        default: ch = ' '; break;
                    }
                    line[c] = ch;
                }
                System.Console.WriteLine(line);
            }
        }
    }
}
