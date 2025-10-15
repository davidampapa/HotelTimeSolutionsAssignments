using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maze.Model
{
    /// <summary>
    /// Abstract base class for dwarfs that use wall-following algorithms to navigate the maze.
    /// Provides helper methods for direction logic (left, right, opposite).
    /// </summary>
    abstract class WallFolloverDwarfBase : DwarfBase
    {
        /// <summary>
        /// Calculates the next position in the maze given a starting point and direction.
        /// Must be implemented by derived classes.
        /// </summary>
        /// <param name="p">Current position.</param>
        /// <param name="d">Direction to move.</param>
        /// <returns>The next position after moving in the given direction.</returns>
        abstract protected Point Next(Point p, Direction d);

        /// <summary>
        /// Gets the direction to the left of the given direction.
        /// </summary>
        /// <param name="d">Current direction.</param>
        /// <returns>Direction to the left.</returns>
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

        /// <summary>
        /// Gets the direction to the right of the given direction.
        /// </summary>
        /// <param name="d">Current direction.</param>
        /// <returns>Direction to the right.</returns>
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

        /// <summary>
        /// Gets the direction opposite to the given direction.
        /// </summary>
        /// <param name="d">Current direction.</param>
        /// <returns>Opposite direction.</returns>
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

        /// <summary>
        /// Represents possible movement directions in the maze.
        /// </summary>
        protected enum Direction { Up, Down, Left, Right }
    }
}
