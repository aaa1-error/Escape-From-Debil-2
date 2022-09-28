using System.Diagnostics;

namespace Debil {
    public partial class DebilEngine {
        public enum WalkMode {
            Walking,
            PlacingWalls,
            RemovingWalls
        }
        public class Player {
            DebilEngine Engine;
            public Coordinate Position;
            public string Texture;
            public short Health;
            public long Score;
            bool IgnoreWalls;
            WalkMode Mode;
            public Player(int _y, int _x, string _texture, DebilEngine _engine) {
                Position = new Coordinate(_y, _x);
                Texture = _texture;
                Engine = _engine;
                Health = 1;
                Score = 0;
                IgnoreWalls = false;
                Mode = WalkMode.Walking;
            }
            public void Input() {
                if(!Console.KeyAvailable) return;

                bool MovedSuccessfully = false;
                Coordinate old_pos = new Coordinate(Position);

                switch(Console.ReadKey(true).Key) {
                    case ConsoleKey.UpArrow:
                    case ConsoleKey.W:
                        if(Position.y == 0) break;

                        if (!Engine.map[this.Position.y - 1, this.Position.x].IsSolid || IgnoreWalls) {
                            Position.y--;
                            Score += 10;

                            MovedSuccessfully = true;
                        }
                        break;

                    case ConsoleKey.LeftArrow:
                    case ConsoleKey.A:
                        if(Position.x == 0) break;

                        if (!Engine.map[this.Position.y, this.Position.x - 1].IsSolid || IgnoreWalls) {
                            Position.x--;
                            Score += 10;

                            MovedSuccessfully = true;
                        }
                        break;

                    case ConsoleKey.DownArrow:
                    case ConsoleKey.S:
                        if(Position.y == Engine.map.Height - 1) break;

                        if (!Engine.map[this.Position.y + 1, this.Position.x].IsSolid || IgnoreWalls) {
                            Position.y++;
                            Score += 10;

                            MovedSuccessfully = true;
                        }
                        break;

                    case ConsoleKey.RightArrow:
                    case ConsoleKey.D:
                        if(Position.x == Engine.map.Width - 1) break;

                        if (!Engine.map[this.Position.y, this.Position.x + 1].IsSolid || IgnoreWalls) {
                            Position.x++;
                            Score += 10;

                            MovedSuccessfully = true;
                        }
                        break;

                    case ConsoleKey.T:
                        if(Score >= 500) {
                            Position = Engine.map.GetRandomPosition();
                            Engine.map[Position].IsFree = false;
                            Engine.map[old_pos].IsFree = true;

                            Score -= 500;

                            MovedSuccessfully = true;
                        }
                        break;

                    case ConsoleKey.C:
                        var proc = Process.Start("clear");
                        proc.Kill(true);
                        break;

                    case ConsoleKey.K:
                        Health--;
                        break;

                    case ConsoleKey.L:
                        if(Health >= 3) break;
                        Health++;
                        break;

                    case ConsoleKey.X:
                        if(Engine.DoDebugDraw == true) Engine.DoDebugDraw = false;
                        else Engine.DoDebugDraw = true;
                        break;

                    case ConsoleKey.F:
                        if(IgnoreWalls == true) IgnoreWalls = false;
                        else IgnoreWalls = true;
                        break;

                    case ConsoleKey.PageUp:
                        Engine.EntityMoveInterval = 100;
                        Engine.EntityMoveTimer.Interval = Engine.EntityMoveInterval;
                        break;

                    case ConsoleKey.PageDown:
                        Engine.EntityMoveInterval = 350;
                        Engine.EntityMoveTimer.Interval = Engine.EntityMoveInterval;
                        break;

                    case ConsoleKey.D1:
                        Mode = WalkMode.Walking;
                        break;
                    
                    case ConsoleKey.D2:
                        Mode = WalkMode.PlacingWalls;
                        Engine.map[Position].Texture = "⬛";
                        Engine.map[Position].IsSolid = true;
                        break;
                    case ConsoleKey.D3:
                        Mode = WalkMode.RemovingWalls;
                        Engine.map[Position].Texture = "  ";
                        Engine.map[Position].IsSolid = false;
                        break;

                    default:
                        break;
                }

                if(MovedSuccessfully) {
                    Engine.map.UpdateWaveMap(Position);

                    switch(Mode) {
                        case WalkMode.RemovingWalls:
                            Engine.map[Position].Texture = "  ";
                            Engine.map[Position].IsSolid = false;
                            break;
                        case WalkMode.PlacingWalls:
                            Engine.map[Position].Texture = "⬛";
                            Engine.map[Position].IsSolid = true;
                            break;
                        default:
                            break;
                    }
                }

            }

            public void Update(object? sender, System.Timers.ElapsedEventArgs? e) {
                Score += 5;
            }
            public void Update() {

            }
        }
    }
}