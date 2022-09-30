using System.Drawing;

namespace Debil
{
    public partial class DebilEngine
    {
        public static void Main(string[] args)
        {
            int Height = 21, Width = 51;

            List<IRenderer> renderers = new List<IRenderer>();
            renderers.Add(new NormalRenderer());
            renderers.Add(new WaveMapRenderer());
            renderers.Add(new FogRenderer());

            DebilEngine engine = new DebilEngine(Height, Width, new MazeLike(Height, Width, 30), renderers);
            engine.Menu();
        }
    }
}