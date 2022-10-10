using System.Collections.Generic;

namespace Debil
{
    public partial class DebilEngine
    {
        public partial class Level
        {
            public readonly int Height;
            public readonly int Width;
            protected Tile[,] Tiles;
            public int[,] WaveMap;
            public ILevelGenerator LevelGenerator;
            public IMobPlacer MobPlacer;
            public IPickupPlacer PickupPlacer;
            public List<BaseMob> Mobs;
            public List<Pickup> Pickups;
            public DebilEngine Engine;
            public Level(int _height, int _width, ILevelGenerator generator, IMobPlacer mobPlacer, IPickupPlacer pickupPlacer, DebilEngine engine)
            {
                if (_height <= 0) throw new ArgumentException("Height must be greater than zero");
                Height = _height;
                if (_width <= 0) throw new ArgumentException("Width must be greater than zero");
                Width = _width;

                Engine = engine;

                LevelGenerator = generator;
                MobPlacer = mobPlacer;
                PickupPlacer = pickupPlacer;

                Mobs = new List<BaseMob>();
                Pickups = new List<Pickup>();

                WaveMap = new int[Height, Width];
                Tiles = new Tile[Height, Width];
            }
            public void Generate()
            {
                Tiles = LevelGenerator.Generate(Height, Width);
                Mobs = MobPlacer.PlaceMobs(this);
                Pickups = PickupPlacer.PlacePickups(this);
            }
            public void UpdateWaveMap(Coordinate playerPosition)
            {
                WaveMap = new int[Height, Width];
                int startingValue = 1, index = 0;

                /* 
                * 0  -- unvisited 
                * 1  -- checked
                * >1 -- visited
                */

                WaveMap[playerPosition.y, playerPosition.x] = startingValue;

                Queue<Coordinate> Inner = new Queue<Coordinate>(), Outer = new Queue<Coordinate>();
                List<Coordinate> Neighbors;
                Inner.Enqueue(new Coordinate(playerPosition.y, playerPosition.x));

                while (Inner.Count > 0)
                {

                    Outer = new Queue<Coordinate>();

                    Coordinate Coordinate;

                    while (Inner.Count > 0)
                    {
                        Coordinate = Inner.Dequeue();

                        WaveMap[Coordinate.y, Coordinate.x] = startingValue + index;
                        Neighbors = StepableNeighbors(Coordinate);

                        foreach (Coordinate Neighbor in Neighbors)
                        {
                            if (WaveMap[Neighbor.y, Neighbor.x] > 0) continue;
                            WaveMap[Neighbor.y, Neighbor.x] = 1;

                            Outer.Enqueue(Neighbor);
                        }
                    }

                    index++;
                    Inner = new Queue<Coordinate>(Outer);
                }
            }

            public List<Coordinate> StepableNeighbors(int y, int x)
            {
                List<Coordinate> result = new List<Coordinate>();

                if (y > 0)
                {
                    if (!Tiles[y - 1, x].IsSolid)
                    {
                        result.Add(new Coordinate(y - 1, x));
                    }
                }

                if (y < Height - 1)
                {
                    if (!Tiles[y + 1, x].IsSolid)
                    {
                        result.Add(new Coordinate(y + 1, x));
                    }
                }

                if (x > 0)
                {
                    if (!Tiles[y, x - 1].IsSolid)
                    {
                        result.Add(new Coordinate(y, x - 1));
                    }
                }

                if (x < Width - 1)
                {
                    if (!Tiles[y, x + 1].IsSolid)
                    {
                        result.Add(new Coordinate(y, x + 1));
                    }
                }

                return result;
            }
            public List<Coordinate> StepableNeighbors(Coordinate position)
            {
                List<Coordinate> result = new List<Coordinate>();

                if (position.y > 0)
                {
                    if (!Tiles[position.y - 1, position.x].IsSolid)
                    {
                        result.Add(new Coordinate(position.y - 1, position.x));
                    }
                }

                if (position.y < Height - 1)
                {
                    if (!Tiles[position.y + 1, position.x].IsSolid)
                    {
                        result.Add(new Coordinate(position.y + 1, position.x));
                    }
                }

                if (position.x > 0)
                {
                    if (!Tiles[position.y, position.x - 1].IsSolid)
                    {
                        result.Add(new Coordinate(position.y, position.x - 1));
                    }
                }

                if (position.x < Width - 1)
                {
                    if (!Tiles[position.y, position.x + 1].IsSolid)
                    {
                        result.Add(new Coordinate(position.y, position.x + 1));
                    }
                }

                return result;
            }
            public List<Coordinate> PossibleMoves(Coordinate position)
            {
                List<Coordinate> result = new List<Coordinate>();

                if (position.y > 0)
                {
                    if (!Tiles[position.y - 1, position.x].IsSolid && Tiles[position.y - 1, position.x].Status != Tile.StatusEnum.Occupied)
                    {
                        result.Add(new Coordinate(position.y - 1, position.x));
                    }
                }

                if (position.y < Height - 1)
                {
                    if (!Tiles[position.y + 1, position.x].IsSolid && Tiles[position.y + 1, position.x].Status != Tile.StatusEnum.Occupied)
                    {
                        result.Add(new Coordinate(position.y + 1, position.x));
                    }
                }

                if (position.x > 0)
                {
                    if (!Tiles[position.y, position.x - 1].IsSolid && Tiles[position.y, position.x - 1].Status != Tile.StatusEnum.Occupied)
                    {
                        result.Add(new Coordinate(position.y, position.x - 1));
                    }
                }

                if (position.x < Width - 1)
                {
                    if (!Tiles[position.y, position.x + 1].IsSolid && Tiles[position.y, position.x + 1].Status != Tile.StatusEnum.Occupied)
                    {
                        result.Add(new Coordinate(position.y, position.x + 1));
                    }
                }

                return result;
            }
            public void TeleportRandom(ref BaseMob Mob)
            {
                Random rand = new Random(Guid.NewGuid().GetHashCode());
                Coordinate new_pos = new Coordinate(rand.Next(0, Height), rand.Next(0, Width));

                while (this[new_pos].Status == Tile.StatusEnum.Occupied || this[new_pos].IsSolid)
                {
                    new_pos = new Coordinate(rand.Next(0, Height), rand.Next(0, Width));
                }

                this[Mob.Position].Status = Tile.StatusEnum.Free;

                Mob.Position = new_pos;
            }
            public void TeleportRandom(ref Player Player)
            {
                Random rand = new Random(Guid.NewGuid().GetHashCode());
                Coordinate new_pos = new Coordinate(rand.Next(0, Height), rand.Next(0, Width));

                while (this[new_pos].Status == Tile.StatusEnum.Occupied || this[new_pos].IsSolid)
                {
                    Console.WriteLine("while teleport random");
                    new_pos = new Coordinate(rand.Next(0, Height), rand.Next(0, Width));
                }

                this[Player.Position].Status = Tile.StatusEnum.Free;

                Player.Position = new_pos;
            }
            public void TeleportRandom(ref Pickup Pickup)
            {
                Random rand = new Random(Guid.NewGuid().GetHashCode());
                Coordinate new_pos = new Coordinate(rand.Next(0, Height), rand.Next(0, Width));

                while (this[new_pos].Status != Tile.StatusEnum.Free || this[new_pos].IsSolid)
                {
                    new_pos = new Coordinate(rand.Next(0, Height), rand.Next(0, Width));
                }

                this[Pickup.Position].Status = Tile.StatusEnum.Free;

                Pickup.Position = new_pos;
            }
            public Coordinate GetRandomPosition()
            {
                Random rand = new Random(Guid.NewGuid().GetHashCode());
                Coordinate new_pos = new Coordinate(rand.Next(0, Height), rand.Next(0, Width));

                while (this[new_pos].Status == Tile.StatusEnum.Occupied || this[new_pos].IsSolid)
                {
                    new_pos = new Coordinate(rand.Next(0, Height), rand.Next(0, Width));
                }

                return new_pos;
            }
            public Tile this[int y, int x]
            {
                get { return Tiles[y, x]; }
                set { Tiles[y, x] = value; }
            }
            public Tile this[Coordinate coord]
            {
                get { return Tiles[coord.y, coord.x]; }
                set { Tiles[coord.y, coord.x] = value; }
            }
            public int Square
            {
                get { return Height * Width; }
            }
        }
    }
}