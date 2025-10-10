using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maze.Model
{
    public class MazeMap
    {
        public CellType[,] _grid;

        public int Rows { get; }
        public int Cols { get; }
        public Point Start { get; }
        public Point Finish { get; }
        public MazeMap() { }
        public MazeMap(CellType[,] grid, Point start, Point finish)
        {
            _grid = grid;
            Rows = grid.GetLength(0);
            Cols = grid.GetLength(1);
            Start = start;
            Finish = finish;
        }

        public CellType this[int row, int col] => _grid[row, col];

        public bool IsInside(int row, int col) => row >= 0 && row < Rows && col >= 0 && col < Cols;

        public bool IsWalkable(int row, int col)
        {
            if (!IsInside(row, col)) return false;
            var cell = _grid[row, col];
            return cell == CellType.Empty || cell == CellType.Start || cell == CellType.Finish;
        }

        public IEnumerable<Point> Neighbors4(Point p)
        {
            var dirs = new[] { new Point(-1, 0), new Point(1, 0), new Point(0, -1), new Point(0, 1) };
            foreach (var d in dirs)
            {
                int r = p.Row + d.Row, c = p.Col + d.Col;
                if (IsWalkable(r, c)) yield return new Point(r, c);
            }
        }

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
