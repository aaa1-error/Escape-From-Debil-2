namespace Debil
{
    public partial class DebilEngine
    {
        public class Tile : BaseEntity
        {
            public enum StatusEnum {
                Free, Occupied, OccupiedButCanStep
            }
            public bool IsSolid;
            public StatusEnum Status;
            public Tile() : base()
            {
                IsSolid = false;
                Status = StatusEnum.Free;
            }
            public Tile(Coordinate position, string _texture, bool isSolid) : base(position, _texture)
            {
                IsSolid = isSolid;
                Status = StatusEnum.Free;
            }
        }
    }
}