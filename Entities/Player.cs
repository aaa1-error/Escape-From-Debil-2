using System.Diagnostics;

namespace Debil
{
    public partial class DebilEngine
    {
        public enum WalkMode
        {
            Walking,
            PlacingWalls,
            RemovingWalls
        }
        public class Player
        {
            DebilEngine Engine;
            public Coordinate Position;
            public string Texture;
            public short Health;
            public long Score;
            bool IgnoreWalls;
            WalkMode Mode;
            public Player(int _y, int _x, string _texture, DebilEngine _engine)
            {
                Position = new Coordinate(_y, _x);
                Texture = _texture;
                Engine = _engine;
                Health = 1;
                Score = 0;
                IgnoreWalls = false;
                Mode = WalkMode.Walking;
            }
            public void Input()
            {
                if (!Console.KeyAvailable) return;

                bool MovedSuccessfully = false;
                Coordinate old_pos = new Coordinate(Position);

                switch (Console.ReadKey(true).Key)
                {
                    case ConsoleKey.UpArrow:
                    case ConsoleKey.W:
                        if (Position.y == 0) break;

                        if (!Engine.Map[this.Position.y - 1, this.Position.x].IsSolid || IgnoreWalls)
                        {
                            Position.y--;
                            Score += 10;

                            MovedSuccessfully = true;
                        }
                        break;

                    case ConsoleKey.LeftArrow:
                    case ConsoleKey.A:
                        if (Position.x == 0) break;

                        if (!Engine.Map[this.Position.y, this.Position.x - 1].IsSolid || IgnoreWalls)
                        {
                            Position.x--;
                            Score += 10;

                            MovedSuccessfully = true;
                        }
                        break;

                    case ConsoleKey.DownArrow:
                    case ConsoleKey.S:
                        if (Position.y == Engine.Map.Height - 1) break;

                        if (!Engine.Map[this.Position.y + 1, this.Position.x].IsSolid || IgnoreWalls)
                        {
                            Position.y++;
                            Score += 10;

                            MovedSuccessfully = true;
                        }
                        break;

                    case ConsoleKey.RightArrow:
                    case ConsoleKey.D:
                        if (Position.x == Engine.Map.Width - 1) break;

                        if (!Engine.Map[this.Position.y, this.Position.x + 1].IsSolid || IgnoreWalls)
                        {
                            Position.x++;
                            Score += 10;

                            MovedSuccessfully = true;
                        }
                        break;

                    case ConsoleKey.T:
                        if (Score >= 500)
                        {
                            Position = Engine.Map.GetRandomPosition();
                            Engine.Map[Position].IsFree = false;
                            Engine.Map[old_pos].IsFree = true;

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
                        if (Health >= 3) break;
                        Health++;
                        break;

                    case ConsoleKey.F:
                        if (IgnoreWalls == true) IgnoreWalls = false;
                        else IgnoreWalls = true;
                        break;

                    case ConsoleKey.PageUp:
                        if(Engine.RendererIndex < Engine.Renderers.Count - 1) {
                            Engine.RendererIndex++;
                            Engine.CurrentRenderer = Engine.Renderers[Engine.RendererIndex];
                        }

                        break;

                    case ConsoleKey.PageDown:
                        if(Engine.RendererIndex > 0) {
                            Engine.RendererIndex--;
                            Engine.CurrentRenderer = Engine.Renderers[Engine.RendererIndex];
                        }

                        break;

                    case ConsoleKey.D1:
                        Mode = WalkMode.Walking;
                        break;

                    case ConsoleKey.D2:
                        Mode = WalkMode.PlacingWalls;
                        Engine.Map[Position].Texture = "⬛";
                        Engine.Map[Position].IsSolid = true;
                        break;
                    case ConsoleKey.D3:
                        Mode = WalkMode.RemovingWalls;
                        Engine.Map[Position].Texture = "  ";
                        Engine.Map[Position].IsSolid = false;
                        break;

                    default:
                        break;
                }

                if (MovedSuccessfully)
                {
                    Engine.Map.UpdateWaveMap(Position);

                    switch (Mode)
                    {
                        case WalkMode.RemovingWalls:
                            Engine.Map[Position].Texture = "  ";
                            Engine.Map[Position].IsSolid = false;
                            break;
                        case WalkMode.PlacingWalls:
                            Engine.Map[Position].Texture = "⬛";
                            Engine.Map[Position].IsSolid = true;
                            break;
                        default:
                            break;
                    }
                }

            }

            public void Update(object? sender, System.Timers.ElapsedEventArgs? e)
            {
                Score += 5;
            }
            public void Update()
            {

            }
        }
    }
}