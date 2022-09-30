using System.Timers;

namespace Debil
{
    public partial class DebilEngine
    {
        public class PathFinderAndRandomDurachock : BaseMob
        {
            Random Rand;
            string SadTexture = ">C";
            string PathfindingModeTexture;
            string RandomModeTexture;
            static int MaxPathfindingDistance = 50;
            public PathFinderAndRandomDurachock(Coordinate position, string pathfindingModeTexture, string randomModeTexture, DebilEngine _engine) : base(position, pathfindingModeTexture, _engine)
            {
                PathfindingModeTexture = pathfindingModeTexture;
                RandomModeTexture = randomModeTexture;

                Rand = new Random(Guid.NewGuid().GetHashCode());
            }
            public override void Move(object? sender, ElapsedEventArgs? e)
            {
                List<Coordinate> positions = Engine.Map.PossibleMoves(Position);

                if (positions.Count == 0)
                {
                    Texture = SadTexture;
                    return;
                }

                Coordinate oldPosition = Position;

                if (positions.Count == 1)
                {
                    Position = positions[0];
                }
                else
                {
                    if (Engine.Map.WaveMap[Position.y, Position.x] >= MaxPathfindingDistance)
                    {
                        Position = positions[Rand.Next(positions.Count)];
                        Texture = RandomModeTexture;
                    }
                    else
                    {
                        foreach (Coordinate pos in positions)
                        {
                            if (Engine.Map.WaveMap[pos.y, pos.x] < Engine.Map.WaveMap[Position.y, Position.x])
                            {
                                Position = pos;
                            }
                        }

                        Texture = PathfindingModeTexture;
                    }
                }

                Engine.Map[oldPosition].Status = Tile.StatusEnum.Free;
                Engine.Map[Position].Status = Tile.StatusEnum.Occupied;
            }
            public override void Update(object? sender, ElapsedEventArgs? e)
            {
                if (Engine.Debchick.Position == this.Position)
                {
                    Engine.Debchick.Health--;
                    Engine.Map.TeleportRandom(ref Engine.Debchick);
                    Engine.Map.UpdateWaveMap(Engine.Debchick.Position);
                }
            }
        }
    }
}