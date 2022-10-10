namespace Debil
{
    public partial class DebilEngine
    {
        public partial class Level
        {
            public class Simplex : ILevelGenerator
            {
                public int WallGenerationChance;
                public float Scale;
                public Simplex(int wallGenerationChance, float scale)
                {
                    WallGenerationChance = 256 * wallGenerationChance / 100;
                    Scale = scale;
                }
                Tile[,] ILevelGenerator.Generate(int Height, int Width)
                {
                    Tile[,] result = new Tile[Height, Width];
                    SimplexNoise.Noise.Seed = Guid.NewGuid().GetHashCode();
                    float[,] floats = SimplexNoise.Noise.Calc2D(Height, Width, Scale);

                    for (int y = 0; y < Height; y++)
                    {
                        for (int x = 0; x < Width; x++)
                        {
                            if ((y == 0 || x == 0 || y == Height - 1 || x == Width - 1) || floats[y, x] <= WallGenerationChance)
                                result[y, x] = new Tile(new Coordinate(y, x), DebilEngine.WallTexture, true);
                            else
                                result[y, x] = new Tile(new Coordinate(y, x), "  ", false);
                        }
                    }

                    SimplexNoise.Noise.Seed = Guid.NewGuid().GetHashCode();
                    floats = SimplexNoise.Noise.Calc2D(Height, Width, Scale);

                    int w = 255 - WallGenerationChance;

                    for (int y = 0; y < Height; y++)
                    {
                        for (int x = 0; x < Width; x++)
                        {
                            if (y == 0 || x == 0 || y == Height - 1 || x == Width - 1) continue;

                            if (floats[y, x] >= w)
                                result[y, x] = new Tile(new Coordinate(y, x), WallTexture, true);
                        }
                    }

                    return result;
                }
            }
        }
    }
}