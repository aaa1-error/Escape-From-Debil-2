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
            }
            public BaseMob(DebilEngine _engine) : base()
            {
                Engine = _engine;
            }
            public abstract void Update(object? sender, System.Timers.ElapsedEventArgs? e);
            public abstract void Move(object? sender, System.Timers.ElapsedEventArgs? e);
        }
    }
}