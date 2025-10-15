using Maze.Model;
using System;

namespace Maze.Model
{
    /// <summary>
    /// Dwarf that follows the left wall in the maze to reach the finish.
    /// </summary>
    internal sealed class LeftDwarf : WallFolloverDwarfBase
    {
        /// <summary>
        /// Reference to the maze map.
        /// </summary>
        private readonly MazeMap _maze;

        /// <summary>
        /// Current facing direction of the dwarf.
        /// </summary>
        private Direction _facing;

        /// <summary>
        /// Initializes a new instance of <see cref="LeftDwarf"/> at the maze start, facing down.
        /// </summary>
        /// <param name="maze">Maze map to solve.</param>
        public LeftDwarf(MazeMap maze)
        {
            _maze = maze;
            Finished = false;
            Position = _maze.Start;
            _facing = Direction.Down;
        }

        /// <summary>
        /// Moves the dwarf one step according to the left-hand wall-following strategy.
        /// Tries to move left, forward, right, or backward (in that order) relative to current facing.
        /// </summary>
        /// <returns>
        /// The new position after moving, or <c>null</c> if the dwarf has finished or cannot move.
        /// </returns>
        public override Point? Move()
        {
            if (Finished) return null;

            // Order of directions to try: left, forward, right, backward
            Direction[] tryOrder = new[]
            {
                LeftOf(_facing),
                _facing,
                RightOf(_facing),
                Opposite(_facing)
            };

            // Try each direction in order
            for (int i = 0; i < tryOrder.Length; i++)
            {
                var dir = tryOrder[i];
                var next = Next(Position, dir);

                // Move if the next cell is walkable
                if (_maze.IsWalkable(next.Row, next.Col))
                {
                    _facing = dir;
                    Position = next;
                    SetFinishedIfAt(_maze.Finish);
                    return Position;
                }
            }

            // No move possible
            return null;
        }

        /// <summary>
        /// Calculates the next point in the maze given a direction.
        /// </summary>
        /// <param name="p">Current position.</param>
        /// <param name="d">Direction to move.</param>
        /// <returns>The next position after moving in the given direction.</returns>
        protected override Point Next(Point p, Direction d)
        {
            switch (d)
            {
                case Direction.Up: return new Point(p.Row - 1, p.Col);
                case Direction.Down: return new Point(p.Row + 1, p.Col);
                case Direction.Left: return new Point(p.Row, p.Col - 1);
                default: return new Point(p.Row, p.Col + 1); // Right
            }
        }
    }
}
