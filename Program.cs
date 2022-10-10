using System.Timers;

namespace Debil
{
    public partial class DebilEngine
    {
        public static void Main(string[] args)
        {
            int Height = 51, Width = 111;   

            DebilEngine engine = new DebilEngine(Height, Width,
                                 new Level.DecoratorTest(new Level.Simplex(30, 0.1f)),
                                 //new Level.MazeLike(30),
                                 new Level.NoMobs(),
                                 new Level.NoPickups());

            engine.Renderers.Add(new NormalRenderer());
            engine.Renderers.Add(new WaveMapRenderer());

            engine.Menu();
        }
    }
}