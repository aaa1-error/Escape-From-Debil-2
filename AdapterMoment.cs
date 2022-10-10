using System.Timers;

namespace Samara
{
    public class Point
    {
        public double X;
        public double Y;
        public Point(double x, double y)
        {
            X = x;
            Y = y;
        }
    }
}

namespace Debil
{
    public partial class DebilEngine
    {
        public class AdaptedPoint : BaseMob
        {
            Samara.Point Point;
            Random Rand;
            public AdaptedPoint(Samara.Point point, DebilEngine _engine) : base(_engine)
            {
                Point = point;
                Position = new Coordinate((int)Point.X, (int)Point.Y);

                int avg = (Position.y + Position.x) % 256;
                Texture = $"\u001b[48;5;{avg}m\u001b[38;5;{(255 - avg)}m •\u001b[0m";

                Rand = new Random(Guid.NewGuid().GetHashCode());
            }

            public override void Move(object? sender, ElapsedEventArgs? e)
            {
                List<Coordinate> possible_moves = Engine.Map.PossibleMoves(Position);

                if (possible_moves.Count == 0) return;

                Engine.Map[Position].Status = Tile.StatusEnum.Free;
                Position = possible_moves[Rand.Next(possible_moves.Count)];
                Engine.Map[Position].Status = Tile.StatusEnum.Occupied;

                Point.Y = (double)Position.y;
                Point.X = (double)Position.x;

                int avg = (Position.y + Position.x) % 256;
                Texture = $"\u001b[48;5;{avg}m\u001b[38;5;{(255 - avg)}m •\u001b[0m";
            }

            public override void Update(object? sender, ElapsedEventArgs? e)
            {
                
            }
        }
    }
}