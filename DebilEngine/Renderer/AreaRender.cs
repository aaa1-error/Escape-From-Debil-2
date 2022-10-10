namespace Debil
{
    public partial class DebilEngine
    {
        public class AreaRender : IRenderer
        {
            int RenderHeight;
            int RenderWidth;
            public AreaRender(int renderHeight, int renderWidth)
            {
                RenderHeight = renderHeight;
                RenderWidth = renderWidth;
            }
            void IRenderer.Draw(Level Map)
            {
                Console.WriteLine(
                $"Health: {string.Join("", Enumerable.Repeat("❤️", Map.Engine.Debchick.Health).ToArray())}  Score: {Map.Engine.Debchick.Score}".PadRight(Console.WindowWidth - 2, ' '));

                string[,] frame = new string[RenderHeight, RenderWidth];

                Coordinate PlayerPos = Map.Engine.Debchick.Position;
                int RenderStartY = PlayerPos.y - (RenderHeight / 2);
                int RenderStartX = PlayerPos.x - (RenderWidth / 2);
                int RenderEndY = RenderStartY + RenderHeight;
                int RenderEndX = RenderStartX + RenderWidth;

                for (int y = 0; y < RenderHeight; y++)
                {
                    for (int x = 0; x < RenderWidth; x++)
                    {
                        if (RenderStartY + y >= 0 && RenderStartY + y < Map.Height && RenderStartX + x >= 0 && RenderStartX + x < Map.Width)
                        {
                            frame[y, x] = Map[RenderStartY + y, RenderStartX + x].Texture;
                        }
                        else
                        {
                            frame[y, x] = "  ";
                        }
                    }
                }

                frame[RenderHeight / 2, RenderWidth / 2] = Map.Engine.Debchick.Texture;

                foreach (var pickup in Map.Pickups)
                {
                    int y = pickup.Position.y;
                    int x = pickup.Position.x;

                    if(y >= RenderStartY && y < RenderEndY && x >= RenderStartX && x < RenderEndX) {
                        frame[y - RenderStartY, x - RenderStartX] = pickup.Texture;
                    }
                }

                foreach (var mob in Map.Mobs)
                {
                    int y = mob.Position.y;
                    int x = mob.Position.x;

                    if(y >= RenderStartY && y < RenderEndY && x >= RenderStartX && x < RenderEndX) {
                        frame[y - RenderStartY, x - RenderStartX] = mob.Texture;
                    }
                }

                for (int i = 0; i < RenderHeight; i++)
                {
                    for (int j = 0; j < RenderWidth; j++)
                    {
                        System.Console.Write(frame[i, j]);
                    }
                    System.Console.WriteLine();
                }
            }
        }
    }
}