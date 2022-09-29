using System.Timers;

namespace Debil
{
    public partial class DebilEngine
    {
        public class RandomDurachock : BaseMob
        {
            public Random Rand;
            public RandomDurachock(Coordinate position, string texture, DebilEngine engine) : base(position, texture, engine)
            {
                Rand = new Random(Guid.NewGuid().GetHashCode());
            }
            public override void Move(object? sender, ElapsedEventArgs? e)
            {
                List<Coordinate> possible_moves = Engine.Map.PossibleMoves(Position);

                if (possible_moves.Count == 0) return;

                Engine.Map[Position].IsFree = true;
                Position = possible_moves[Rand.Next(possible_moves.Count)];
                Engine.Map[Position].IsFree = false;
            }
            public override void Update(object? sender, ElapsedEventArgs? e)
            {
                if (Engine.Debchick.Position == this.Position)
                {
                    Engine.Debchick.Health--;
                    Engine.Map.TeleportRandom(ref Engine.Debchick);
                }
            }
        }
    }
}