namespace Debil
{
    public partial class DebilEngine
    {
        public partial class Level
        {
            public class NoPickups : IPickupPlacer
            {
                public NoPickups()
                {

                }
                List<Pickup> IPickupPlacer.PlacePickups(Level lvl)
                {
                    List<Pickup> result = new List<Pickup>();

                    return result;
                }
            }
        }
    }
}