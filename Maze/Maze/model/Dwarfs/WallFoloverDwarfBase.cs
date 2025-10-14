using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maze.Model
{
    abstract class WallFoloverDwarfBase : DwarfBase
    {
        abstract protected Point Next(Point p, Direction d);
        protected Direction LeftOf(Direction d)
        {
            switch (d)
            {
                case Direction.Up: return Direction.Left;
                case Direction.Down: return Direction.Right;
                case Direction.Left: return Direction.Down;
                default: return Direction.Up;    // Right
            }
        }

        protected Direction RightOf(Direction d)
        {
            switch (d)
            {
                case Direction.Up: return Direction.Right;
                case Direction.Down: return Direction.Left;
                case Direction.Left: return Direction.Up;
                default: return Direction.Down;  // Right
            }
        }

        protected Direction Opposite(Direction d)
        {
            switch (d)
            {
                case Direction.Up: return Direction.Down;
                case Direction.Down: return Direction.Up;
                case Direction.Left: return Direction.Right;
                default: return Direction.Left;  // Right
            }
        }
        protected enum Direction { Up, Down, Left, Right }

    }
}
