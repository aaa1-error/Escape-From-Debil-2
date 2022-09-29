namespace Debil
{
    public partial class DebilEngine
    {
        public class Tile : BaseEntity
        {
            public bool IsSolid;
            public bool IsFree;
            public Tile() : base()
            {
                IsSolid = false;
                IsFree = false;
            }
            public Tile(Coordinate position, string _texture, bool isSolid) : base(position, _texture)
            {
                IsSolid = isSolid;
                if (!IsSolid)
                {
                    IsFree = true;
                }
                else
                {
                    IsFree = false;
                }
            }
        }
    }
}