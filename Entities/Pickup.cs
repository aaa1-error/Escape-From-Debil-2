using System.Timers;

namespace Debil {
    public partial class DebilEngine {
        public class Pickup : BaseEntity {
            DebilEngine Engine;
            public int Score;
            public Pickup(Coordinate position, string texture, int score, DebilEngine engine) : base(position, texture) {
                Score = score;
                Engine = engine;
            }
            public void CheckCollision(object? sender, ElapsedEventArgs? e) {
                if(Position == Engine.player.Position) {
                    Engine.player.Score += Score;

                    Coordinate new_pos = Engine.map.GetRandomPosition();

                    Engine.map[Position].IsFree = true;
                    Engine.map[new_pos].IsFree = false;
                    Position = new_pos;
                }
            }
        }
    }
}