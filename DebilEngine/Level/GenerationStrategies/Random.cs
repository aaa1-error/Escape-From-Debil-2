namespace Debil
{
    public partial class DebilEngine
    {
        public class Randomized : LevelGenerationStrategy
        {
            Random Rand;
            int WallGenerationChance;
            public Randomized(int _height, int _width, int _WallGenerationChance) : base(_height, _width)
            {
                WallGenerationChance = _WallGenerationChance;
                Rand = new Random(Guid.NewGuid().GetHashCode());
            }
            public override Tile[,] GenerateLevel()
            {
                Tile[,] tiles = new Tile[Height, Width];

                for (int y = 0; y < Height; y++)
                {
                    for (int x = 0; x < Width; x++)
                    {
                        if ((y == 0 || y == Height - 1 || x == 0 || x == Width - 1) || Rand.Next(0, 101) <= WallGenerationChance)
                        {
                            tiles[y, x] = new Tile(new Coordinate(y, x), "🟨", true);
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

                for(int i = 1; i <= 0; i++)
                    result.Add(new PathFinderDurachock(level.GetRandomPosition(), "🤖", level.Engine));

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