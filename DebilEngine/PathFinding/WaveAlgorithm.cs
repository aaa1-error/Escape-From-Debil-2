namespace Debil {
    public partial class DebilEngine {
        public void Wave(int y_start, int x_start) {
            int[,] WaveMap = new int[map.Height, map.Width];
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

            while(Inner.Count > 0) {

                Outer = new Queue<Coordinate>();

                Coordinate Coordinate;

                while(Inner.Count > 0) {
                    Coordinate = Inner.Dequeue();

                    if(Coordinate.y == map.Height - 1 && Coordinate.x == map.Width - 1) return;

                    WaveMap[Coordinate.y, Coordinate.x] = startingValue + index;
                    Neighbors = map.FreeNeighbors(Coordinate);

                    foreach(Coordinate Neighbor in Neighbors) {
                        if(WaveMap[Neighbor.y, Neighbor.x] > 0 ) continue;
                        WaveMap[Neighbor.y, Neighbor.x] = 1;

                        Outer.Enqueue(Neighbor);
                    }
                }

                index++;

                Inner = new Queue<Coordinate>(Outer);


                /*
                System.Console.WriteLine();

                for(int y = 0; y < map.Height; y++) {
                    for(int x = 0; x < map.Width; x++) {
                        //System.Console.Write(WaveMap[y,x].ToString().PadLeft(3, ' '));

                        if(WaveMap[y, x] == 1) {
                            System.Console.Write("⬜");
                        } else if(WaveMap[y, x] > 0) {
                            System.Console.Write("  ");
                        } else {
                            System.Console.Write("⬛");
                        }
                    }
                    System.Console.WriteLine();
                }
                System.Console.WriteLine(Inner.Count.ToString().PadRight(10, ' '));

                Console.SetCursorPosition(0, 1);
                Console.CursorVisible = false;

                Thread.Sleep(50);
                */
            }
        }
    }
}