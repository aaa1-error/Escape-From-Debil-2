namespace Debil
{
    public partial class DebilEngine
    {
        public void Wave(int y_start, int x_start)
        {
            int[,] WaveMap = new int[Map.Height, Map.Width];
            int startingValue = 2, index = 0;

            /* 
             * 0  -- unvisited 
             * 1  -- checked
             * >1 -- visited
             */

            WaveMap[y_start, x_start] = startingValue;

            Queue<Coordinate> Inner = new Queue<Coordinate>(), Outer = new Queue<Coordinate>();
            List<Coordinate> Neighbors;
            Inner.Enqueue(new Coordinate(y_start, x_start));

            while (Inner.Count > 0)
            {

                Outer = new Queue<Coordinate>();

                Coordinate Coordinate;

                while (Inner.Count > 0)
                {
                    Coordinate = Inner.Dequeue();

                    if (Coordinate.y == Map.Height - 1 && Coordinate.x == Map.Width - 1) return;

                    WaveMap[Coordinate.y, Coordinate.x] = startingValue + index;
                    Neighbors = Map.StepableNeighbors(Coordinate);

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
    }
}