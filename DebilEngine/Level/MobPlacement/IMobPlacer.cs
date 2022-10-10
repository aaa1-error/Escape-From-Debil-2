namespace Debil
{
    public partial class DebilEngine
    {
        public partial class Level
        {
            public interface IMobPlacer
            {
                public List<BaseMob> PlaceMobs(Level lvl);
            }
        }
    }
}