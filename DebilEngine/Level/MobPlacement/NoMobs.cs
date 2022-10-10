namespace Debil
{
    public partial class DebilEngine
    {
        public partial class Level
        {
            public class NoMobs : IMobPlacer
            {
                public NoMobs()
                {

                }
                List<BaseMob> IMobPlacer.PlaceMobs(Debil.DebilEngine.Level lvl)
                {
                    List<BaseMob> result = new List<BaseMob>();

                    return result;
                }
            }
        }
    }
}