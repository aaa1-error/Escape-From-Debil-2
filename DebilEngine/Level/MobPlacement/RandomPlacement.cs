namespace Debil
{
    public partial class DebilEngine
    {
        public partial class Level
        {
            public class RandomMobPlacement : IMobPlacer
            {
                public RandomMobPlacement()
                {

                }
                List<BaseMob> IMobPlacer.PlaceMobs(Debil.DebilEngine.Level lvl)
                {
                    List<BaseMob> result = new List<BaseMob>();

                    for (int i = 1; i <= 25; i++)
                        result.Add(new RandomDurachock(lvl.GetRandomPosition(), "🤯", lvl.Engine));

                    for(int i = 1; i <= 15; i++)
                        result.Add(new PathFinderAndRandomDurachock(lvl.GetRandomPosition(), "😈", "🤬", lvl.Engine));

                    for(int i = 1; i <= 5; i++)
                        result.Add(new PathFinderDurachock(lvl.GetRandomPosition(), "🤖", lvl.Engine));

                    return result;
                }
            }
        }
    }
}