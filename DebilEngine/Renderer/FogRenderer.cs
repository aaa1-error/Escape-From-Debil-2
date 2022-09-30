namespace Debil
{
    public partial class DebilEngine
    {
        public class FogRenderer : IRenderer
        {
            static int FogDistance = 10;
            public FogRenderer()
            {
            }
            void IRenderer.Draw(Level Map)
            {
                Console.WriteLine(
                $"Health: {string.Join("", Enumerable.Repeat("❤️", Map.Engine.Debchick.Health).ToArray())}  Score: {Map.Engine.Debchick.Score}".PadRight(Console.WindowWidth - 2, ' '));

                string[,] frame = new string[Map.Height, Map.Width];

                for (int i = 0; i < Map.Height; i++)
                {
                    for (int j = 0; j < Map.Width; j++)
                    {
                        if (Map.WaveMap[i, j] > 0 && Map.WaveMap[i, j] <= FogDistance)
                        {
                            frame[i, j] = "  ";
                        }
                        else 
                        {
                            frame[i, j] = "⬛️";
                        }
                    }
                }

                frame[Map.Engine.Debchick.Position.y, Map.Engine.Debchick.Position.x] = Map.Engine.Debchick.Texture;
                
                foreach (var pickup in Map.Pickups)
                {
                    if(Map.WaveMap[pickup.Position.y, pickup.Position.x] <= FogDistance && Map.WaveMap[pickup.Position.y, pickup.Position.x] != 0)
                        frame[pickup.Position.y, pickup.Position.x] = pickup.Texture;
                }

                foreach (var mob in Map.Mobs)
                {
                    if(Map.WaveMap[mob.Position.y, mob.Position.x] <= FogDistance && Map.WaveMap[mob.Position.y, mob.Position.x] != 0)
                        frame[mob.Position.y, mob.Position.x] = mob.Texture;
                }

                for (int i = 0; i < Map.Height; i++)
                {
                    for (int j = 0; j < Map.Width; j++)
                    {
                        Console.Write(frame[i, j]);
                    }
                    Console.WriteLine();
                }
            }
        }
    }
}