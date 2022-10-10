namespace Debil
{
    public partial class DebilEngine
    {
        public abstract class LevelGenerationStrategy
        {
            public int Height;
            public int Width;
            public LevelGenerationStrategy(int _height, int _width)
            {
                Height = _height;
                Width = _width;
            }
            public abstract Tile[,] GenerateLevel();
            public abstract List<BaseMob> PlaceMobs(Level level);
            public abstract List<Pickup> PlacePickups(Level level);
        }

        public class Box : LevelGenerationStrategy
        {
            public Box(int _height, int _width) : base(_height, _width)
            {

            }
            public override Tile[,] GenerateLevel()
            {
                Tile[,] tiles = new Tile[Height, Width];

                for (int y = 0; y < Height; y++)
                {
                    for (int x = 0; x < Width; x++)
                    {
                        if (y == 0 || y == Height - 1 || x == 0 || x == Width - 1)
                        {
                            tiles[y, x] = new Tile(new Coordinate(y, x), WallTexture, true);
                        }
                        else
                        {
                            tiles[y, x] = new Tile(new Coordinate(y, x), "  ", false);
                        }
                    }
                }

                return tiles;
            }
            public override List<BaseMob> PlaceMobs(Level level)
            {
                List<BaseMob> result = new List<BaseMob>();

                int pointsCount = (int)(Math.Abs((Math.Sin(Height) * 100.0 + Math.Sin(Width) * 100.0)));
                System.Console.WriteLine(pointsCount);
                Console.ReadKey(true);
                List<Coordinate> freeCoordinates = new List<Coordinate>();

                for(int i = 1; i <= pointsCount; i++) 
                    freeCoordinates.Add(level.GetRandomPosition());

                for (int i = 1; i <= pointsCount; i++)
                    result.Add(new AdaptedPoint(new Samara.Point((double)freeCoordinates[i-1].y, (double)freeCoordinates[i-1].x), level.Engine));

                return result;
            }
            public override List<Pickup> PlacePickups(Level level)
            {
                List<Pickup> result = new List<Pickup>();
                return result;
            }
        }
    }
}