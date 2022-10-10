namespace Debil
{
    public partial class DebilEngine
    {
        public class LGBTRenderer2 : IRenderer
        {
            static List<string> DistanceColors = "🟩|🟨|🟧|🟥|🟪|🟦|  ".Split('|').ToList();
            public LGBTRenderer2()
            {
            }
            void IRenderer.Draw(Level Map)
            {
                Console.WriteLine(
                $"Health: {string.Join("", Enumerable.Repeat("❤️", Map.Engine.Debchick.Health).ToArray())}  Score: {Map.Engine.Debchick.Score}".PadRight(Console.WindowWidth - 2, ' '));

                string[,] frame = new string[Map.Height, Map.Width];
                int index = 0;

                for (int i = 0; i < Map.Height; i++)
                {
                    for (int j = 0; j < Map.Width; j++)
                    {
                        if (Map[i, j].IsSolid || Map.WaveMap[i, j] == 0)
                        {
                            frame[i, j] = "⬛️";
                            continue;
                        }
                        else
                        {
                            index = Map.WaveMap[i, j] / 5;
                            if(index < DistanceColors.Count)
                                frame[i, j] = DistanceColors[index];
                            else
                                frame[i, j] = DistanceColors.Last();
                        }
                    }
                }

                frame[Map.Engine.Debchick.Position.y, Map.Engine.Debchick.Position.x] = Map.Engine.Debchick.Texture;

                foreach (var pickup in Map.Pickups)
                {
                    frame[pickup.Position.y, pickup.Position.x] = pickup.Texture;
                }
                
                foreach (var mob in Map.Mobs)
                {
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