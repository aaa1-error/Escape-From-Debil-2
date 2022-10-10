namespace Debil
{
    public partial class DebilEngine
    {
        public partial class Level
        {
            public interface ILevelGenerator
            {
                public Tile[,] Generate(int Height, int Width);
            }
        }
    }
}