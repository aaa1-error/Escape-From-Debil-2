namespace Debil
{
    public partial class DebilEngine
    {
        public class AreaWaveRender : IRenderer
        {
            static List<int[]> AnsiWaveColors = new List<int[]> 
            {
                new int[] {255, 0, 0},
                new int[] {255, 0, 12},
                new int[] {255, 0, 21},
                new int[] {255, 0, 29},
                new int[] {255, 0, 35},
                new int[] {255, 0, 42},
                new int[] {255, 0, 47},
                new int[] {255, 0, 53},
                new int[] {255, 0, 59},
                new int[] {255, 0, 64},
                new int[] {255, 0, 70},
                new int[] {255, 0, 76},
                new int[] {255, 0, 82},
                new int[] {255, 0, 88},
                new int[] {255, 0, 94},
                new int[] {255, 0, 100},
                new int[] {255, 0, 106},
                new int[] {255, 0, 112},
                new int[] {255, 0, 119},
                new int[] {252, 0, 125},
                new int[] {249, 0, 132},
                new int[] {245, 0, 138},
                new int[] {241, 0, 145},
                new int[] {237, 0, 152},
                new int[] {232, 0, 159},
                new int[] {226, 0, 166},
                new int[] {220, 0, 172},
                new int[] {214, 0, 179},
                new int[] {207, 0, 186},
                new int[] {199, 0, 193},
                new int[] {191, 0, 200},
                new int[] {181, 0, 206},
                new int[] {171, 0, 213},
                new int[] {160, 0, 220},
                new int[] {147, 0, 226},
                new int[] {133, 0, 232},
                new int[] {116, 0, 238},
                new int[] {96, 0, 244},
                new int[] {68, 0, 250},
                new int[] {0, 0, 255},
                new int[] {0, 0, 255},
                new int[] {0, 41, 255},
                new int[] {0, 60, 255},
                new int[] {0, 74, 255},
                new int[] {0, 86, 255},
                new int[] {0, 97, 255},
                new int[] {0, 106, 255},
                new int[] {0, 115, 255},
                new int[] {0, 123, 255},
                new int[] {0, 130, 255},
                new int[] {0, 137, 255},
                new int[] {0, 143, 255},
                new int[] {0, 149, 255},
                new int[] {0, 155, 255},
                new int[] {0, 160, 255},
                new int[] {0, 165, 255},
                new int[] {0, 170, 255},
                new int[] {0, 175, 255},
                new int[] {0, 179, 255},
                new int[] {0, 184, 255},
                new int[] {0, 188, 255},
                new int[] {0, 193, 255},
                new int[] {0, 197, 255},
                new int[] {0, 201, 254},
                new int[] {0, 205, 242},
                new int[] {0, 209, 229},
                new int[] {0, 213, 216},
                new int[] {0, 217, 203},
                new int[] {0, 221, 190},
                new int[] {0, 225, 176},
                new int[] {0, 228, 163},
                new int[] {0, 232, 149},
                new int[] {0, 235, 135},
                new int[] {0, 239, 121},
                new int[] {0, 242, 107},
                new int[] {0, 245, 93},
                new int[] {0, 248, 77},
                new int[] {0, 250, 60},
                new int[] {0, 253, 40},
                new int[] {0, 255, 0}
            };
            int RenderHeight;
            int RenderWidth;
            public AreaWaveRender(int renderHeight, int renderWidth)
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
                            if(Map[RenderStartY + y, RenderStartX + x].IsSolid)
                            {
                                frame[y, x] = Map[RenderStartY + y, RenderStartX + x].Texture;
                            }
                            else
                            {
                                int[] color = AnsiWaveColors[Map.WaveMap[RenderStartY + y, RenderStartX + x] % AnsiWaveColors.Count];
                                frame[y, x] = $"\u001b[48;2;{color[0]};{color[1]};{color[2]}m  \u001b[0m";
                                //printf "\x1b[48;2;255;100;0mTRUECOLOR\x1b[0m\n"
                            }
                        }
                        else
                        {
                            frame[y, x] = "  ";
                        }
                    }
                }

                frame[RenderHeight / 2, RenderWidth / 2] = $"\u001b[48;2;{AnsiWaveColors[0][0]};{AnsiWaveColors[0][1]};{AnsiWaveColors[0][2]}m{Map.Engine.Debchick.Texture}\u001b[0m";
                //frame[RenderHeight / 2, RenderWidth / 2] = Map.Engine.Debchick.Texture;
                // $"\u001b[48;2;{AnsiWaveColors[0][0]};{AnsiWaveColors[0][1]};{color[0][2]}m{Map.Engine.Debchick.Texture}\u001b[0m";
                // $"\u001b[48;2;{AnsiWaveColors[0][0]};{AnsiWaveColors[0][1]};{color[0][2]}m  \u001b[0m";

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