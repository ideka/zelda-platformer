namespace ZeldaPlatformerLibrary.Components
{
    using Artemis.Interface;
    using Microsoft.Xna.Framework;

    public class AABBComponent : IComponent
    {
        public AABBComponent(Rectangle rectangle)
            : base()
        {
            this.Rectangle = rectangle;
        }

        public Rectangle Rectangle { get; set; }
    }
}