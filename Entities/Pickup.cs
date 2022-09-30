using System.Timers;

namespace Debil
{
    public partial class DebilEngine
    {
        public class Pickup : BaseEntity
        {
            DebilEngine Engine;
            public int Score;
            public Pickup(Coordinate position, string texture, int score, DebilEngine engine) : base(position, texture)
            {
                Score = score;
                Engine = engine;
            }
            public void CheckCollision(object? sender, ElapsedEventArgs? e)
            {
                if (Position == Engine.Debchick.Position)
                {
                    Engine.Debchick.Score += Score;

                    Engine.Map[Position].Status = Tile.StatusEnum.Free;
                    Position = Engine.Map.GetRandomPosition();
                    Engine.Map[Position].Status = Tile.StatusEnum.OccupiedButCanStep;
                }
            }
        }
    }
}