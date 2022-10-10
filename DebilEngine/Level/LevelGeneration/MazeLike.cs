namespace Debil
{
    public partial class DebilEngine
    {
        public partial class Level
        {
            public class MazeLike : ILevelGenerator
            {
                public int WallGenerationChance;
                public MazeLike(int wallGenerationChance)
                {
                    WallGenerationChance = wallGenerationChance;
                }
                Tile[,] ILevelGenerator.Generate(int Height, int Width)
                {
                    Random rand = new Random(Guid.NewGuid().GetHashCode());
                    Tile[,] result = new Tile[Height, Width];

                    for(int y = 0; y < Height; y++)
                    {
                        for(int x = 0; x < Width; x++)
                        {
                            if((y == 0 || x == 0 || y == Height - 1 || x == Width - 1) || (y % 2 == 0 && x % 2 == 0))
                                result[y, x] = new Tile(new Coordinate(y, x), DebilEngine.WallTexture, true);
                            else
                                result[y, x] = new Tile(new Coordinate(y, x), "  ", false);
                        }
                    }

                    for(int y = 0; y < Height; y++)
                    {
                        for(int x = 0; x < Width; x++)
                        {
                            if((y + 1) % 2 == 0 ^ (x + 1) % 2 == 0 && rand.Next(0, 100) <= WallGenerationChance)
                                result[y, x] = new Tile(new Coordinate(y, x), DebilEngine.WallTexture, true);
                        }
                    }

                    return result;
                }
            }
        }
    }
}