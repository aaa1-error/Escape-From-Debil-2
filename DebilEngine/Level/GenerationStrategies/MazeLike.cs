namespace Debil
{
    public partial class DebilEngine
    {
        private struct PickupIdentity
        {
            public string Texture;
            public int Score;
            public PickupIdentity(string t, int s) {
                Texture = t;
                Score = s;
            }
        }
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
                            tiles[y, x] = new Tile(new Coordinate(y, x), WallTexture, true);
                        }
                        else
                        {
                            tiles[y, x] = new Tile(new Coordinate(y, x), "  ", false);
                        }

                        System.Console.WriteLine($"Generated {y * Width + x}/{Height * Width}");
                        Console.SetCursorPosition(0, 0);
                        Console.CursorVisible = false;

                    }
                }

                for (int y = 0; y < Height; y++)
                {
                    for (int x = 0; x < Width; x++)
                    {
                        if (y % 2 == 0 && x % 2 == 0)
                        {
                            tiles[y, x].Texture = WallTexture;
                            tiles[y, x].IsSolid = true;
                        }

                        if ((y + 1) % 2 == 0 ^ (x + 1) % 2 == 0)
                        {
                            if (Rand.Next(0, 100) <= WallGenerationChance)
                            {
                                tiles[y, x].Texture = WallTexture;
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

                /* for (int i = 1; i <= 500; i++)
                    result.Add(new PathFinderDurachock(level.GetRandomPosition(), "👽", level.Engine));

                for (int i = 1; i <= 150; i++)
                    result.Add(new PathFinderAndRandomDurachock(level.GetRandomPosition(), "😈", "🤬", level.Engine));

                for (int i = 1; i <= 100; i++)
                    result.Add(new RandomDurachock(level.GetRandomPosition(), "💩", level.Engine)); */

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
                /* List<PickupIdentity> pickupIdentities = new List<PickupIdentity>()
                {
                    new PickupIdentity("🥝", 100),
                    new PickupIdentity("🍅", 200),
                    new PickupIdentity("🍉", 500),
                    new PickupIdentity("🥐", 500),
                    new PickupIdentity("🧀", 400),
                    new PickupIdentity("🍗", 1000),
                    new PickupIdentity("🍖", 1500),
                    new PickupIdentity("🌭", 2000),
                    new PickupIdentity("🍔", 4000),
                    new PickupIdentity("🍟", 1500),
                    new PickupIdentity("🍕", 3000),
                    new PickupIdentity("🍙", 1000),
                    new PickupIdentity("🍊", 600),
                    new PickupIdentity("🍎", 300)
                };
                Random rand = new Random(Guid.NewGuid().GetHashCode());
                PickupIdentity pickupData;

                int PickupCount = (level.Height * level.Width) / 256;
                for (int i = 1; i <= PickupCount; i++)
                {
                    pickupData = pickupIdentities[rand.Next(pickupIdentities.Count)];

                    result.Add(new Pickup(level.GetRandomPosition(), pickupData.Texture, pickupData.Score, level.Engine));
                } */
                return result;
            }
        }
    }
}