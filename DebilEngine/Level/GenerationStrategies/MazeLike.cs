namespace Debil
{
    public partial class DebilEngine
    {
        public class MazeLike : LevelGenerationStrategy
        {
            Random Rand;
            int WallGenerationChance;
            public MazeLike(int _height, int _width, int _WallGenerationChance) : base(_height, _width)
            {
                Rand = new Random(Guid.NewGuid().GetHashCode());
                WallGenerationChance = _WallGenerationChance;
            }
            public override Tile[,] GenerateLevel()
            {
                Tile[,] tiles = new Tile[Height, Width];

                for (int y = 0; y < Height; y++)
                {
                    for (int x = 0; x < Width; x++)
                    {
                        if ((y == 0 || y == Height - 1 || x == 0 || x == Width - 1))
                        {
                            tiles[y, x] = new Tile(new Coordinate(y, x), "🟨", true);
                        }
                        else
                        {
                            tiles[y, x] = new Tile(new Coordinate(y, x), "  ", false);
                        }
                    }
                }

                for (int y = 0; y < Height; y++)
                {
                    for (int x = 0; x < Width; x++)
                    {
                        if (y % 2 == 0 && x % 2 == 0)
                        {
                            tiles[y, x].Texture = "🟨";
                            tiles[y, x].IsSolid = true;
                        }

                        if ((y + 1) % 2 == 0 ^ (x + 1) % 2 == 0)
                        {
                            if (Rand.Next(0, 100) <= WallGenerationChance)
                            {
                                tiles[y, x].Texture = "🟨";
                                tiles[y, x].IsSolid = true;
                            }
                        }
                    }
                }

                return tiles;
            }
            public override List<BaseMob> PlaceMobs(Level level)
            {
                List<BaseMob> result = new List<BaseMob>();

                for (int i = 1; i <= 0; i++)
                    result.Add(new PathFinderDurachock(level.GetRandomPosition(), "👽", level.Engine));

                for (int i = 1; i <= 10; i++)
                    result.Add(new PathFinderAndRandomDurachock(level.GetRandomPosition(), "😈", "🤬", level.Engine));

                for (int i = 1; i <= 0; i++)
                    result.Add(new RandomDurachock(level.GetRandomPosition(), "💩", level.Engine));

                return result;
            }
            public override List<Pickup> PlacePickups(Level level)
            {
                List<Pickup> result = new List<Pickup>();
                Random rand = new Random(Guid.NewGuid().GetHashCode());

                string[] textures = "🍙 🍕 🍟 🍔 🌭 🍗 🍒 🍎 🍏 🍆 🍓 🍅 🧁".Split(' ');
                int[] points = new int[] { 5000, 2000, 1000, 3000, 5000, 1000, 500, 1500, 2000, 500, 1000, 100, 500 };

                for (int i = 1; i <= 10; i++)
                {
                    result.Add(new Pickup(level.GetRandomPosition(), textures[Rand.Next(13)], points[Rand.Next(13)], level.Engine));
                }
                return result;
            }
        }
    }
}