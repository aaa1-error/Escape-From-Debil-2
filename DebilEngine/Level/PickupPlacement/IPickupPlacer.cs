namespace Debil
{
    public partial class DebilEngine
    {
        public partial class Level
        {
            public interface IPickupPlacer
            {
                public List<Pickup> PlacePickups(Level lvl);
            }
        }
    }
}