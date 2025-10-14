using System;

namespace Maze.Model
{
    internal sealed class RightDwarf : WallFoloverDwarfBase
    {
        private readonly MazeMap _maze;
        private Direction _facing;

        public RightDwarf(MazeMap maze)
        {
            _maze = maze;
            Finished = false;
            Position = _maze.Start;
            _facing = Direction.Down;
        }

        public override Point? Move()
        {
            if (Finished) return null;
            // Console.WriteLine("Snaha o pohyb (pravotočivý)"); // případně log

            var tryOrder = new[]
            {
                RightOf(_facing),
                _facing,
                LeftOf(_facing),
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
