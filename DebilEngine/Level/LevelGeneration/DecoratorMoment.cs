namespace Debil
{
    public partial class DebilEngine
    {
        public partial class Level
        {
            public class DecoratorTest : ILevelGenerator
            {
                ILevelGenerator Generator;
                public DecoratorTest(ILevelGenerator generator)
                {
                    Generator = generator;
                }
                Tile[,] ILevelGenerator.Generate(int Height, int Width)
                {
                    Random rand = new Random(Guid.NewGuid().GetHashCode());
                    Tile[,] result = Generator.Generate(Height, Width);

                    int y_middle = Height / 2 + 1;
                    int x_middle = Width / 2 + 1;

                    for(int y = 1; y < Height - 1; y++)
                    {
                        for(int x = x_middle - 1; x < x_middle + 2; x++)
                        {
                            result[y, x].Texture = "  ";
                            result[y, x].IsSolid = false;
                        }
                    }

                    for(int x = 1; x < Width - 1; x++)
                    {
                        for(int y = y_middle - 1; y < y_middle + 2; y++)
                        {
                            result[y, x].Texture = "  ";
                            result[y, x].IsSolid = false;
                        }
                    }

                    for(int y = 1; y < Height - 1; y++)
                    {
                        for(int x = 1; x < Width - 1; x++)
                        {
                            if((int)Math.Pow(y - y_middle, 2) + (int)Math.Pow(x - x_middle, 2) <= 15*15)
                            {
                                result[y, x].Texture = "  ";
                                result[y, x].IsSolid = false;
                            }
                        }
                    }

                    return result;
                }
            }
        }
    }
}