using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maze.Model
{
    /// <summary>
    /// Represents a dwarf that waits a random number of ticks before instantly moving to the finish.
    /// </summary>
    class RandomDwarf : DwarfBase
    {
        /// <summary>
        /// Reference to the maze map.
        /// </summary>
        private readonly MazeMap _maze;

        /// <summary>
        /// Number of ticks to wait before moving to the finish.
        /// </summary>
        private int _delay;

        /// <summary>
        /// Current tick count since the dwarf was created.
        /// </summary>
        private int _ticks = 0;

        /// <summary>
        /// Initializes a new instance of <see cref="RandomDwarf"/> at the maze start.
        /// The dwarf will wait a random number of ticks before finishing.
        /// </summary>
        /// <param name="maze">Maze map to solve.</param>
        public RandomDwarf(MazeMap maze) 
        {
            _maze = maze;
            Position = maze.Start;
            // Set a random delay between 10 and 150 ticks before the dwarf finishes
            _delay = new Random().Next(10, 150);
        }

        /// <summary>
        /// Moves the dwarf. Increments the tick counter and, after the delay, moves the dwarf to the finish.
        /// </summary>
        /// <returns>
        /// The new position after moving, or <c>null</c> if the dwarf has finished.
        /// </returns>
        override public Point? Move()
        {
            // If already finished, do nothing
            if (Finished) return null;
            _ticks++;

            // After waiting for the random delay, jump to the finish and mark as finished
            if (_ticks >= _delay)
            {
                Position = _maze.Finish;
                Finished = true;
            }

            // Return current position (either still at start or at finish)
            return Position;
        }
    }
}
