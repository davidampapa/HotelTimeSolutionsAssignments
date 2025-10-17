using Maze.Model;
using System;
using System.Collections.Generic;

namespace Maze.controller
{
    /// <summary>
    /// Responsible for spawning dwarfs into the simulation.
    /// </summary>
    /// <remarks>
    /// This is a simple, scheduler driven by an internal tick counter.
    /// On tick 0 it adds a <see cref="BfsDwarf"/>, and on tick 50 it adds a
    /// <see cref="RandomDwarf"/>. Extend <see cref="SpawnDwarf"/> to add more
    /// spawn points or types.
    /// </remarks>
    class DwarfSpawner
    {
        /// <summary>
        /// The collection that receives newly spawned dwarfs.
        /// </summary>
        private readonly List<DwarfBase> _dwarfs;

        /// <summary>
        /// Reference to the maze used when constructing dwarf instances.
        /// </summary>
        private readonly MazeMap _maze;

        /// <summary>
        /// Internal game tick counter used to decide when to spawn.
        /// </summary>
        private int _gameTick = 0;

        /// <summary>
        /// Creates a new <see cref="DwarfSpawner"/>.
        /// </summary>
        /// <param name="dwarfs">The list to which spawned dwarfs will be added.</param>
        /// <param name="maze">The maze map passed to dwarf constructors.</param>
        public DwarfSpawner(List<DwarfBase> dwarfs, MazeMap maze)
        {
            _dwarfs = dwarfs;
            _maze = maze;
        }

        /// <summary>
        /// Advances the internal tick and, if a spawn point is reached,
        /// creates the appropriate dwarf and appends it to <see cref="_dwarfs"/>.
        /// </summary>
        /// <returns>
        /// <c>true</c> if a dwarf was spawned on this call; otherwise <c>false</c>.
        /// </returns>
        /// </example>
        public bool SpawnDwarf()
        {
            _gameTick++;

            switch (_gameTick - 1)
            {
                case 0:
                    _dwarfs.Add(new LeftDwarf(_maze));
                    return true;
                case 50:
                    _dwarfs.Add(new RightDwarf(_maze));
                    return true;
                case 100:
                    _dwarfs.Add(new RandomDwarf(_maze));
                    return true;
                case 150:
                    _dwarfs.Add(new BfsDwarf(_maze));
                    return true;
            }
            return false;
        }
    }
}