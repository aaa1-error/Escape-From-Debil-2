namespace Debil
{
    public partial class DebilEngine
    {
        public partial class Level
        {
            public class Box : ILevelGenerator
            {
                Tile[,] ILevelGenerator.Generate(int Height, int Width)
                {
                    Tile[,] result = new Tile[Height, Width];

                    for(int y = 0; y < Height; y++)
                    {
                        for(int x = 0; x < Width; x++)
                        {
                            if(y == 0 || x == 0 || y == Height - 1 || x == Width - 1)                        
                                result[y, x] = new Tile(new Coordinate(y, x), DebilEngine.WallTexture, true);
                            else
                                result[y, x] = new Tile(new Coordinate(y, x), "  ", false);
                        }
                    }
                    
                    return result;
                }
            }
        }
    }
}