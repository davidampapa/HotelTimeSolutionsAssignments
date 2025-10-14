using Maze.Model;
using System;

namespace Maze.Model
{
    // Trpaslík, který se drží levé zdi: zkusí doleva, rovně, doprava, otočka.
    internal sealed class LeftDwarf : WallFoloverDwarfBase
    {
        private readonly MazeMap _maze;

        // směr, kam je trpaslík „otočený“
        private Direction _facing;

        public LeftDwarf(MazeMap maze)
        {
            _maze = maze;
            Finished = false;
            Position = _maze.Start;
            _facing = Direction.Down; // počáteční orientace – klidně změň dle potřeby
        }

        public override Point? Move()
        {
            if (Finished) return null;
            Console.WriteLine("Snaha o pohyb");

            var tryOrder = new[]
            {
                LeftOf(_facing),
                _facing,
                RightOf(_facing),
                Opposite(_facing)
            };

            for (int i = 0; i < tryOrder.Length; i++)
            {
                var dir = tryOrder[i];
                var next = Next(Position, dir);

                if (_maze.IsWalkable(next.Row, next.Col))
                {
                    _facing = dir;
                    Position = next;
                    SetFinishedIfAt(_maze.Finish);
                    return Position;
                }
            }

            // obklopen zdmi – nemůže se pohnout
            return null;
        }

        // ---------- helpers ----------

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
