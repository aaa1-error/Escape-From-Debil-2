namespace Debil
{
    public partial class DebilEngine
    {
        public abstract class BaseMob : BaseEntity
        {
            public DebilEngine Engine;
            public BaseMob(Coordinate position, string _texture, DebilEngine _engine) : base(position, _texture)
            {
                Engine = _engine;
                Engine.Map[Position].Status = Tile.StatusEnum.Occupied;
            }
            public abstract void Update(object? sender, System.Timers.ElapsedEventArgs? e);
            public abstract void Move(object? sender, System.Timers.ElapsedEventArgs? e);
        }
    }
}