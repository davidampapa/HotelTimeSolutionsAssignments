using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maze.Model
{
    /// <summary>
    /// Abstract base class for all dwarfs navigating the maze.
    /// Provides common properties and logic for position tracking and completion status.
    /// </summary>
    internal abstract class DwarfBase
    {
        /// <summary>
        /// Current position of the dwarf in the maze.
        /// </summary>
        public Point Position { get; protected set; }

        /// <summary>
        /// Indicates whether the dwarf has reached its goal and finished.
        /// </summary>
        public bool Finished { get; protected set; }

        /// <summary>
        /// Moves the dwarf one step according to its strategy.
        /// </summary>
        /// <returns>
        /// The new position after moving, or <c>null</c> if the dwarf has finished.
        /// </returns>
        public abstract Point? Move();

        /// <summary>
        /// Sets <see cref="Finished"/> to true if the dwarf is at the specified target position.
        /// </summary>
        /// <param name="target">The target position to check against.</param>
        protected void SetFinishedIfAt(Point target)
        {
            if (Position.Row == target.Row && Position.Col == target.Col)
                Finished = true;
        }
    }
}
