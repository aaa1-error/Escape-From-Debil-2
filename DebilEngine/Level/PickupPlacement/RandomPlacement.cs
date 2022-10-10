namespace Debil
{
    public partial class DebilEngine
    {
        public partial class Level
        {
            public class RandomPickupPlacement : IPickupPlacer
            {
                private struct PickupIdentity
                {
                    public string Texture;
                    public int Score;
                    public PickupIdentity(string t, int s)
                    {
                        Texture = t;
                        Score = s;
                    }
                    public PickupIdentity(PickupIdentity other)
                    {
                        Texture = other.Texture;
                        Score = other.Score;
                    }
                }
                static List<PickupIdentity> PickupInfos = new List<PickupIdentity>()
                {
                    new PickupIdentity("🍎", 300),
                    new PickupIdentity("🧀", 500),
                    new PickupIdentity("🍔", 3000),
                    new PickupIdentity("🌭", 2000),
                    new PickupIdentity("🍟", 1500),
                    new PickupIdentity("🍕", 4000),
                    new PickupIdentity("🍪", 500)
                };
                public RandomPickupPlacement()
                {

                }
                List<Pickup> IPickupPlacer.PlacePickups(Level lvl)
                {
                    Random rand = new Random(Guid.NewGuid().GetHashCode());
                    List<Pickup> result = new List<Pickup>();

                    for (int i = 1; i <= 10; i++)
                    {
                        PickupIdentity id = PickupInfos.ElementAt(rand.Next(PickupInfos.Count));
                        result.Add(new Pickup(lvl.GetRandomPosition(), id.Texture, id.Score, lvl.Engine));
                    }
                    
                    return result;
                }
            }
        }
    }
}