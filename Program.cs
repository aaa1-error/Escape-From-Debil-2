using System.Drawing;

namespace Debil
{
    public partial class DebilEngine
    {
        public static void Main(string[] args)
        {
            int Height = 51, Width = 51 ;

            List<IRenderer> renderers = new List<IRenderer>();
        
            //for(int i = 51; i >= 11; i--) 
            renderers.Add(new AreaRender(31, 31));
            renderers.Add(new AreaRender(51, 51));
            renderers.Add(new NormalRenderer());
            
            DebilEngine engine = new DebilEngine(Height, Width, new MazeLike(Height, Width, 30), renderers);
            //DebilEngine engine = new DebilEngine(Height, Width, new Box(Height, Width), renderers);
            engine.Menu();
        }
    }
}