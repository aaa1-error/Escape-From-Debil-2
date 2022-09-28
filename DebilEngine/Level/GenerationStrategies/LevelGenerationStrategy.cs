namespace Debil {
    public partial class DebilEngine {
        public abstract class LevelGenerationStrategy {
            public int Height;
            public int Width;
            public LevelGenerationStrategy(int _height, int _width) {
                Height = _height;
                Width = _width;
            }
            public abstract Tile[,] GenerateLevel();
            public abstract List<BaseMob> PlaceMobs(Level level);
            public abstract List<Pickup> PlacePickups(Level level);
        }

        public class Box : LevelGenerationStrategy {
            public Box(int _height, int _width) : base(_height, _width) {
                
            }
            public override Tile[,] GenerateLevel() {
                Tile[,] tiles = new Tile[Height, Width];

                for(int y = 0; y < Height; y++) {
                    for(int x = 0; x < Width; x++) {
                        if(y == 0 || y == Height - 1 || x == 0 || x == Width - 1) {
                            tiles[y, x] = new Tile(new Coordinate(y, x), "🟨", true);
                        } else {
                            tiles[y, x] = new Tile(new Coordinate(y, x), "  ", false);
                        }
                    }
                }

                return tiles;
            }
            public override List<BaseMob> PlaceMobs(Level level)
            {
                List<BaseMob> result = new List<BaseMob>();

                /* result.Add(new PathFindingDebchick(level.GetRandomPosition(), "🗿", level.Engine));
                result.Add(new PathFindingDebchick(level.GetRandomPosition(), "🗿", level.Engine));
                result.Add(new PathFindingDebchick(level.GetRandomPosition(), "🗿", level.Engine));
                result.Add(new PathFindingDebchick(level.GetRandomPosition(), "🗿", level.Engine));
                result.Add(new PathFindingDebchick(level.GetRandomPosition(), "🗿", level.Engine));
                result.Add(new PathFindingDebchick(level.GetRandomPosition(), "🗿", level.Engine));
                result.Add(new PathFindingDebchick(level.GetRandomPosition(), "🗿", level.Engine));
                result.Add(new PathFindingDebchick(level.GetRandomPosition(), "🗿", level.Engine));
                result.Add(new PathFindingDebchick(level.GetRandomPosition(), "🗿", level.Engine));
                result.Add(new PathFindingDebchick(level.GetRandomPosition(), "🗿", level.Engine));
                result.Add(new PathFindingDebchick(level.GetRandomPosition(), "🗿", level.Engine));
                result.Add(new PathFindingDebchick(level.GetRandomPosition(), "🗿", level.Engine)); */
                
                return result;
            }
            public override List<Pickup> PlacePickups(Level level)
            {
                List<Pickup> result = new List<Pickup>();

                result.Add(new Pickup(level.GetRandomPosition(), "🍔", 1000, level.Engine));
                return result;
            }
        }
    }
}