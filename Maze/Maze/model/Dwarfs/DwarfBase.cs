using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maze.Model
{
    internal abstract class DwarfBase
    {
        public Point Position { get; protected set; }
        public bool Finished { get; protected set; }
        public abstract Point? Move();
        // v DwarfBase
        protected void SetFinishedIfAt(Point target)
        {
            if (Position.Row == target.Row && Position.Col == target.Col)
                Finished = true;
        }

    }
}
