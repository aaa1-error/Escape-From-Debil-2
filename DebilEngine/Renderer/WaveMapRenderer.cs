namespace Debil
{
    public partial class DebilEngine
    {
        public class WaveMapRenderer : IRenderer
        {
            static List<int[]> AnsiWaveColors = new List<int[]> 
            {
                new int[] {0, 255, 0},
                new int[] {0, 255, 7},
                new int[] {0, 255, 13},
                new int[] {0, 255, 20},
                new int[] {0, 255, 26},
                new int[] {0, 255, 33},
                new int[] {0, 255, 39},
                new int[] {0, 255, 46},
                new int[] {0, 255, 52},
                new int[] {0, 255, 59},
                new int[] {0, 255, 65},
                new int[] {0, 255, 72},
                new int[] {0, 255, 78},
                new int[] {0, 255, 85},
                new int[] {0, 255, 92},
                new int[] {0, 255, 98},
                new int[] {0, 255, 105},
                new int[] {0, 255, 111},
                new int[] {0, 255, 118},
                new int[] {0, 255, 124},
                new int[] {0, 255, 131},
                new int[] {0, 255, 137},
                new int[] {0, 255, 144},
                new int[] {0, 255, 150},
                new int[] {0, 255, 157},
                new int[] {0, 255, 163},
                new int[] {0, 255, 170},
                new int[] {0, 255, 177},
                new int[] {0, 255, 183},
                new int[] {0, 255, 190},
                new int[] {0, 255, 196},
                new int[] {0, 255, 203},
                new int[] {0, 255, 209},
                new int[] {0, 255, 216},
                new int[] {0, 255, 222},
                new int[] {0, 255, 229},
                new int[] {0, 255, 235},
                new int[] {0, 255, 242},
                new int[] {0, 255, 248},
                new int[] {0, 255, 255},
                new int[] {0, 255, 255},
                new int[] {7, 248, 255},
                new int[] {13, 242, 255},
                new int[] {20, 235, 255},
                new int[] {26, 229, 255},
                new int[] {33, 222, 255},
                new int[] {39, 216, 255},
                new int[] {46, 209, 255},
                new int[] {52, 203, 255},
                new int[] {59, 196, 255},
                new int[] {65, 190, 255},
                new int[] {72, 183, 255},
                new int[] {78, 177, 255},
                new int[] {85, 170, 255},
                new int[] {92, 163, 255},
                new int[] {98, 157, 255},
                new int[] {105, 150, 255},
                new int[] {111, 144, 255},
                new int[] {118, 137, 255},
                new int[] {124, 131, 255},
                new int[] {131, 124, 255},
                new int[] {137, 118, 255},
                new int[] {144, 111, 255},
                new int[] {150, 105, 255},
                new int[] {157, 98, 255},
                new int[] {163, 92, 255},
                new int[] {170, 85, 255},
                new int[] {177, 78, 255},
                new int[] {183, 72, 255},
                new int[] {190, 65, 255},
                new int[] {196, 59, 255},
                new int[] {203, 52, 255},
                new int[] {209, 46, 255},
                new int[] {216, 39, 255},
                new int[] {222, 33, 255},
                new int[] {229, 26, 255},
                new int[] {235, 20, 255},
                new int[] {242, 13, 255},
                new int[] {248, 7, 255},
                new int[] {255, 0, 255},
                new int[] {255, 0, 255},
                new int[] {255, 0, 248},
                new int[] {255, 0, 242},
                new int[] {255, 0, 235},
                new int[] {255, 0, 229},
                new int[] {255, 0, 222},
                new int[] {255, 0, 216},
                new int[] {255, 0, 209},
                new int[] {255, 0, 203},
                new int[] {255, 0, 196},
                new int[] {255, 0, 190},
                new int[] {255, 0, 183},
                new int[] {255, 0, 177},
                new int[] {255, 0, 170},
                new int[] {255, 0, 163},
                new int[] {255, 0, 157},
                new int[] {255, 0, 150},
                new int[] {255, 0, 144},
                new int[] {255, 0, 137},
                new int[] {255, 0, 131},
                new int[] {255, 0, 124},
                new int[] {255, 0, 118},
                new int[] {255, 0, 111},
                new int[] {255, 0, 105},
                new int[] {255, 0, 98},
                new int[] {255, 0, 92},
                new int[] {255, 0, 85},
                new int[] {255, 0, 78},
                new int[] {255, 0, 72},
                new int[] {255, 0, 65},
                new int[] {255, 0, 59},
                new int[] {255, 0, 52},
                new int[] {255, 0, 46},
                new int[] {255, 0, 39},
                new int[] {255, 0, 33},
                new int[] {255, 0, 26},
                new int[] {255, 0, 20},
                new int[] {255, 0, 13},
                new int[] {255, 0, 7},
                new int[] {255, 0, 0},
                new int[] {255, 0, 0},
                new int[] {255, 5, 0},
                new int[] {255, 10, 0},
                new int[] {255, 15, 0},
                new int[] {255, 20, 0},
                new int[] {255, 25, 0},
                new int[] {255, 30, 0},
                new int[] {255, 36, 0},
                new int[] {255, 41, 0},
                new int[] {255, 46, 0},
                new int[] {255, 51, 0},
                new int[] {255, 56, 0},
                new int[] {255, 61, 0},
                new int[] {255, 66, 0},
                new int[] {255, 71, 0},
                new int[] {255, 76, 0},
                new int[] {255, 81, 0},
                new int[] {255, 86, 0},
                new int[] {255, 91, 0},
                new int[] {255, 96, 0},
                new int[] {255, 102, 0},
                new int[] {255, 107, 0},
                new int[] {255, 112, 0},
                new int[] {255, 117, 0},
                new int[] {255, 122, 0},
                new int[] {255, 127, 0},
                new int[] {255, 132, 0},
                new int[] {255, 137, 0},
                new int[] {255, 142, 0},
                new int[] {255, 147, 0},
                new int[] {255, 152, 0},
                new int[] {255, 157, 0},
                new int[] {255, 162, 0},
                new int[] {255, 168, 0},
                new int[] {255, 173, 0},
                new int[] {255, 178, 0},
                new int[] {255, 183, 0},
                new int[] {255, 188, 0},
                new int[] {255, 193, 0},
                new int[] {255, 198, 0},
                new int[] {255, 198, 0},
                new int[] {248, 199, 0},
                new int[] {242, 201, 0},
                new int[] {235, 202, 0},
                new int[] {229, 204, 0},
                new int[] {222, 205, 0},
                new int[] {216, 207, 0},
                new int[] {209, 208, 0},
                new int[] {203, 210, 0},
                new int[] {196, 211, 0},
                new int[] {190, 213, 0},
                new int[] {183, 214, 0},
                new int[] {177, 216, 0},
                new int[] {170, 217, 0},
                new int[] {163, 218, 0},
                new int[] {157, 220, 0},
                new int[] {150, 221, 0},
                new int[] {144, 223, 0},
                new int[] {137, 224, 0},
                new int[] {131, 226, 0},
                new int[] {124, 227, 0},
                new int[] {118, 229, 0},
                new int[] {111, 230, 0},
                new int[] {105, 232, 0},
                new int[] {98, 233, 0},
                new int[] {92, 235, 0},
                new int[] {85, 236, 0},
                new int[] {78, 237, 0},
                new int[] {72, 239, 0},
                new int[] {65, 240, 0},
                new int[] {59, 242, 0},
                new int[] {52, 243, 0},
                new int[] {46, 245, 0},
                new int[] {39, 246, 0},
                new int[] {33, 248, 0},
                new int[] {26, 249, 0},
                new int[] {20, 251, 0},
                new int[] {13, 252, 0},
                new int[] {7, 254, 0},
                new int[] {0, 255, 0},
            };
            public WaveMapRenderer()
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
                        if (Map.WaveMap[i, j] == 0)
                        {
                            frame[i, j] = "⬛️";
                        }
                        else
                        {
                            int index = Map.WaveMap[i, j] % AnsiWaveColors.Count;

                            int[] color = AnsiWaveColors[index];
                            frame[i, j] = $"\u001b[48;2;{color[0]};{color[1]};{color[2]}m  \u001b[0m";
                        }
                    }
                }

                frame[Map.Engine.Debchick.Position.y, Map.Engine.Debchick.Position.x] = $"\u001b[48;2;{AnsiWaveColors[0][0]};{AnsiWaveColors[0][1]};{AnsiWaveColors[0][2]}m{Map.Engine.Debchick.Texture}\u001b[0m";

                foreach (var pickup in Map.Pickups)
                {
                    if(Map.WaveMap[pickup.Position.y, pickup.Position.x] == 0) continue;

                    int index = Map.WaveMap[pickup.Position.y, pickup.Position.x] % AnsiWaveColors.Count;

                    int[] color = AnsiWaveColors[index];
                    frame[pickup.Position.y, pickup.Position.x] = $"\u001b[48;2;{color[0]};{color[1]};{color[2]}m{pickup.Texture}\u001b[0m";
                }

                foreach (var mob in Map.Mobs)
                {
                    if(Map.WaveMap[mob.Position.y, mob.Position.x] == 0) continue;

                    int index = Map.WaveMap[mob.Position.y, mob.Position.x] % AnsiWaveColors.Count;

                    int[] color = AnsiWaveColors[index];
                    frame[mob.Position.y, mob.Position.x] = $"\u001b[48;2;{color[0]};{color[1]};{color[2]}m{mob.Texture}\u001b[0m";
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