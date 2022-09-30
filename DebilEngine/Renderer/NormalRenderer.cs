namespace Debil
{
    public partial class DebilEngine
    {
        public class NormalRenderer : IRenderer
        {
            public NormalRenderer() {
            }
            void IRenderer.Draw(Level Map)
            {
                Console.WriteLine(
                $"Health: {string.Join("", Enumerable.Repeat("❤️", Map.Engine.Debchick.Health).ToArray())}  Score: {Map.Engine.Debchick.Score}".PadRight(Console.WindowWidth - 2, ' '));

                string[,] frame = new string[Map.Height, Map.Width];

                for (int i = 0; i < Map.Height; i++)
                {
                    for (int j = 0; j < Map.Width; j++)
                    {
                        frame[i, j] = Map[i, j].Texture;
                    }
                }

                frame[Map.Engine.Debchick.Position.y, Map.Engine.Debchick.Position.x] = Map.Engine.Debchick.Texture;

                foreach (var mob in Map.Mobs)
                {
                    frame[mob.Position.y, mob.Position.x] = mob.Texture;
                }

                foreach (var pickup in Map.Pickups)
                {
                    frame[pickup.Position.y, pickup.Position.x] = pickup.Texture;
                }

                for (int i = 0; i < Map.Height; i++)
                {
                    for (int j = 0; j < Map.Width; j++)
                    {
                        Console.Write(frame[i, j]);
                    }
                    Console.WriteLine();
                }
            }
        }
    }
}