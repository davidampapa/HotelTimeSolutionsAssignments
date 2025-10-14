namespace Maze.Model
{
    /// <summary>
    /// Represents the possible types of cells that can appear in the maze grid.
    /// </summary>
    public enum CellType
    {
        /// <summary>
        /// A walkable.
        /// </summary>
        Empty,

        /// <summary>
        /// A non-walkable obstacle cell (wall).
        /// </summary>
        Wall,

        /// <summary>
        /// The starting position in the maze.
        /// </summary>
        Start,

        /// <summary>
        /// The target or goal position in the maze.
        /// </summary>
        Finish,

        /// <summary>
        /// A cell occupied by a dwarf.
        /// </summary>
        Dwarf
    }
}
