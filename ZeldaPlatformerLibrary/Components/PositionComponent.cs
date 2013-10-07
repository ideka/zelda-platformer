namespace ZeldaPlatformerLibrary.Components
{
    using Artemis.Interface;
    using Microsoft.Xna.Framework;

    public class PositionComponent : IComponent
    {
        public PositionComponent(Vector2 position)
            : base()
        {
            this.Position = position;
        }

        public Vector2 Position { get; set; }
    }
}