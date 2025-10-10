using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maze.Model
{
    public struct Point
    {
        public int Row { get; }
        public int Col { get; }
        public Point(int row, int col) { Row = row; Col = col; }
        public override string ToString() => $"({Row},{Col})";
    }
}
