using System.Timers;

namespace Debil
{
    public partial class DebilEngine
    {
        public class PathFinderDurachock : BaseMob
        {
            string SadTexture = ":C";
            string DefaultTexture;
            public PathFinderDurachock(Coordinate position, string _texture, DebilEngine _engine) : base(position, _texture, _engine)
            {
                DefaultTexture = _texture;
            }

            public override void Move(object? sender, ElapsedEventArgs? e)
            {
                List<Coordinate> positions = Engine.Map.PossibleMoves(Position);

                if (positions.Count == 0 || positions.All(pos => Engine.Map.WaveMap[pos.y, pos.x] == 0))
                {
                    Texture = SadTexture;
                    return;
                }
                else
                {
                    Texture = DefaultTexture;
                }

                Coordinate positionWithLeastIndex = positions[0];

                foreach (Coordinate pos in positions)
                {
                    if (Engine.Map.WaveMap[pos.y, pos.x] < Engine.Map.WaveMap[positionWithLeastIndex.y, positionWithLeastIndex.x])
                    {
                        positionWithLeastIndex = pos;
                    }
                }

                Engine.Map[Position].Status = Tile.StatusEnum.Free;
                Engine.Map[positionWithLeastIndex].Status = Tile.StatusEnum.Occupied;

                Position = positionWithLeastIndex;
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