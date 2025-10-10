using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maze.Model
{
    internal class BfsDwarf : DwarfBase
    {
        private Queue<Point> _path;
        private Point _finish;

        public BfsDwarf(MazeMap maze)
        {
            Position = maze.Start;
            _finish = maze.Finish;
            CalculatePath(maze);
        }

        private void CalculatePath(MazeMap maze)
        {
            var cameFrom = new Dictionary<Point, Point>();
            var q = new Queue<Point>();
            var seen = new HashSet<Point>();

            q.Enqueue(maze.Start);
            seen.Add(maze.Start);

            while (q.Count > 0)
            {
                var u = q.Dequeue();
                if (u.Row == maze.Finish.Row && u.Col == maze.Finish.Col)
                    break;

                foreach (var v in maze.Neighbors4(u))
                {
                    if (seen.Contains(v)) continue;
                    seen.Add(v);
                    cameFrom[v] = u;
                    q.Enqueue(v);
                }
            }

            _path = new Queue<Point>();

            if (!cameFrom.ContainsKey(maze.Finish))
                return;

            var rev = new List<Point>();
            var cur = maze.Finish;
            while (!(cur.Row == maze.Start.Row && cur.Col == maze.Start.Col))
            {
                rev.Add(cur);
                cur = cameFrom[cur];
            }
            rev.Reverse();

            // Naplnit frontu kroků
            for (int i = 0; i < rev.Count; i++)
                _path.Enqueue(rev[i]);
        }

        override public Point? Move()
        {
            if (Finished) return null;

            if (_path == null || _path.Count == 0)
            {
                Finished = (Position.Row == _finish.Row && Position.Col == _finish.Col);
                return null;
            }

            Position = _path.Dequeue();

            if (Position.Row == _finish.Row && Position.Col == _finish.Col)
                Finished = true;
            return Position;
        }
    }
}
