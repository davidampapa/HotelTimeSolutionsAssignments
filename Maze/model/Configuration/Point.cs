namespace Maze.Model
{
    /// <summary>
    /// Immutable 2D coordinate used to address cells in the maze grid.
    /// </summary>
    public struct Point
    {
        /// <summary>
        /// Row index.
        /// </summary>
        public int Row { get; }

        /// <summary>
        /// Column index.
        /// </summary>
        public int Col { get; }

        /// <summary>
        /// Initializes a new <see cref="Point"/> with the specified row and column.
        /// </summary>
        /// <param name="row">Row index.</param>
        /// <param name="col">Column index.</param>
        public Point(int row, int col)
        {
            Row = row;
            Col = col;
        }
    }
}
