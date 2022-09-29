namespace Debil
{
    public partial class DebilEngine
    {
        public class BaseEntity
        {
            public Coordinate Position;
            public string Texture;
            public BaseEntity()
            {
                Position = new Coordinate();
                Texture = "🫥";
            }
            public BaseEntity(Coordinate position, string texture)
            {
                Position = position;
                Texture = texture;
            }
        }
    }
}