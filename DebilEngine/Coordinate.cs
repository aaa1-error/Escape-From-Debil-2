namespace Debil {
    public partial class DebilEngine {
        public class Coordinate {
            public int y;
            public int x;
            public Coordinate() {
                y = 0;
                x = 0;
            }
            public Coordinate(int _y, int _x) {
                y = _y;
                x = _x;
            }
            public Coordinate(Coordinate coord) {
                this.y = coord.y;
                this.x = coord.x;
            }
            public static bool operator == (Coordinate c1, Coordinate c2) {
                return (c1.y == c2.y && c1.x == c2.x);
            }
            public static bool operator != (Coordinate c1, Coordinate c2) {
                return (c1.y != c2.y || c1.x != c2.x);
            }
            public override bool Equals(object? obj)
            {
                if(obj is null) return false;

                Coordinate? coord = obj as Coordinate;

                if(coord is null) return false;

                return this == coord;
            }
            //Cantor moment
            public override int GetHashCode() {
                int xx = x >= 0 ? 2 * x : -2 * x + 1;
                int yy = y >= 0 ? 2 * y : -2 * y + 1;
                
                return (((xx + yy) * (xx + yy + 1)) / 2) + yy;
            }
        }
    }
}