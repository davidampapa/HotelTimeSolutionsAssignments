using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maze.Model
{
    /// <summary>
    /// Dwarf that finds its path through the maze using BFS.
    /// </summary>
    internal class BfsDwarf : DwarfBase
    {
        /// <summary>
        /// Queue containing the sequence of points representing the path from start to finish.
        /// </summary>
        private Queue<Point> _path;

        /// <summary>
        /// The finish point in the maze.
        /// </summary>
        private Point _finish;

        /// <summary>
        /// Initializes a new instance of <see cref="BfsDwarf"/> and calculates the path using BFS.
        /// </summary>
        /// <param name="maze">Maze map to solve.</param>
        public BfsDwarf(MazeMap maze)
        {
            Position = maze.Start;
            _finish = maze.Finish;
            CalculatePath(maze);
        }

        /// <summary>
        /// Calculates the shortest path from start to finish using BFS.
        /// </summary>
        /// <param name="maze">Maze map to search.</param>
        private void CalculatePath(MazeMap maze)
        {
            // Dictionary to reconstruct the path: key is current point, value is previous point.
            var cameFrom = new Dictionary<Point, Point>();
            // Queue for BFS traversal.
            var q = new Queue<Point>();
            // Set to keep track of visited points.
            var seen = new HashSet<Point>();

            q.Enqueue(maze.Start);
            seen.Add(maze.Start);

            // BFS loop
            while (q.Count > 0)
            {
                var u = q.Dequeue();
                // Stop if finish is reached.
                if (u.Row == maze.Finish.Row && u.Col == maze.Finish.Col)
                    break;

                // Explore all walkable neighbors.
                foreach (Point v in maze.Neighbors4(u))
                {
                    if (seen.Contains(v)) continue;
                    seen.Add(v);
                    cameFrom[v] = u;
                    q.Enqueue(v);
                }
            }

            _path = new Queue<Point>();

            // If finish is unreachable, leave path empty.
            if (!cameFrom.ContainsKey(maze.Finish))
                return;

            // Reconstruct path from finish to start.
            var rev = new List<Point>();
            var cur = maze.Finish;
            while (!(cur.Equals(maze.Start)))
            {
                rev.Add(cur);
                cur = cameFrom[cur];
            }
            rev.Reverse();

            // Fill the path queue with the reconstructed path.
            for (int i = 0; i < rev.Count; i++)
                _path.Enqueue(rev[i]);
        }

        /// <summary>
        /// Moves the dwarf one step along the calculated path.
        /// </summary>
        /// <returns>
        /// The new position if the dwarf moved; <c>null</c> if finished or no path exists.
        /// </returns>
        override public Point? Move()
        {
            // If already finished, do nothing.
            if (Finished) return null;

            // If no path or path is exhausted, check if at finish.
            if (_path == null || _path.Count == 0)
            {
                Finished = (Position.Row == _finish.Row && Position.Col == _finish.Col);
                return null;
            }
            
            // Move to the next point in the path.
            Position = _path.Dequeue();

            // Mark as finished if reached the finish point.
            if (Position.Row == _finish.Row && Position.Col == _finish.Col)
                Finished = true;
            return Position;
        }
    }
}
