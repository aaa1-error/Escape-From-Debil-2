using System.Drawing;

namespace Debil
{
    public partial class DebilEngine
    {
        public static void Main(string[] args)
        {
            int Height = 51, Width = 111;

            List<IRenderer> renderers = new List<IRenderer>();
            renderers.Add(new NormalRenderer());
            renderers.Add(new WaveMapRenderer());
            renderers.Add(new FogRenderer());
            renderers.Add(new LGBTRenderer1());
            renderers.Add(new LGBTRenderer2());

            DebilEngine engine = new DebilEngine(Height, Width, new Randomized(Height, Width, 30), renderers);
            engine.Menu();
        }
    }
}