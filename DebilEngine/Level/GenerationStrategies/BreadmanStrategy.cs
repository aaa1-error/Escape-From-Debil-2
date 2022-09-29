namespace Debil
{
    public partial class DebilEngine
    {
        public class BreadmanStrategy : LevelGenerationStrategy
        {
            public BreadmanStrategy(int _height, int _width) : base(_height, _width)
            {

            }
            public override Tile[,] GenerateLevel()
            {
                Tile[,] tiles = new Tile[Height, Width];

                for (int y = 0; y < Height; y++)
                {
                    for (int x = 0; x < Width; x++)
                    {
                        if ((y == 0 || y == Height - 1 || x == 0 || x == Width - 1) || x % 2 == 0)
                        {
                            tiles[y, x] = new Tile(new Coordinate(y, x), "⬛", true);
                            continue;
                        }
                        else
                        {
                            tiles[y, x] = new Tile(new Coordinate(y, x), "  ", false);
                            continue;
                        }
                    }
                }

                for (int x = 2; x < Width; x += 4)
                {
                    tiles[1, x].Texture = "  ";
                    tiles[1, x].IsSolid = false;
                }

                for (int x = 4; x < Width; x += 4)
                {
                    tiles[Height - 2, x].Texture = "  ";
                    tiles[Height - 2, x].IsSolid = false;
                }

                int middle = Height / 2;

                for (int x = 1; x < Width; x++)
                {
                    tiles[middle, x].Texture = "  ";
                    tiles[middle, x].IsSolid = false;
                }

                return tiles;
            }
            public override List<BaseMob> PlaceMobs(Level level)
            {
                List<BaseMob> result = new List<BaseMob>();

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