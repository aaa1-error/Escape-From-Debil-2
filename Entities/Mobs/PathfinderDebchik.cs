using System.Timers;

namespace Debil {
    public partial class DebilEngine {
        public class PathFindingDebchick : BaseMob {
            string SadTexture = ":C";
            string DefaultTexture;
            public PathFindingDebchick(Coordinate position, string _texture, DebilEngine _engine) : base(position, _texture, _engine) {
                DefaultTexture = _texture;
            }

            public override void Move(object? sender, ElapsedEventArgs? e)
            {
                List<Coordinate> positions = Engine.map.PossibleMoves(Position);

                if(positions.Count == 0 || positions.All(pos => Engine.map.WaveMap[pos.y, pos.x] == 0)) {
                    Texture = SadTexture;
                    return;
                } else {
                    Texture = DefaultTexture;
                }

                Coordinate positionWithLeastIndex = positions[0];

                foreach(Coordinate pos in positions) {
                    if(Engine.map.WaveMap[pos.y, pos.x] < Engine.map.WaveMap[positionWithLeastIndex.y, positionWithLeastIndex.x]) {
                        positionWithLeastIndex = pos;
                    }
                }

                Engine.map[Position].IsFree = true;
                Engine.map[positionWithLeastIndex].IsFree = false;

                Position = positionWithLeastIndex;
            }
            public override void Update(object? sender, ElapsedEventArgs? e) {
                if(Engine.player.Position == this.Position) {
                    Engine.player.Health--;
                    Engine.map.TeleportRandom(ref Engine.player);
                    Engine.map.UpdateWaveMap(Engine.player.Position);
                }
            }
        }
    }
}