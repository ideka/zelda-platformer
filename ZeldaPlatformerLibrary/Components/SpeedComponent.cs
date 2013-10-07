namespace ZeldaPlatformerLibrary.Components
{
    using Artemis.Interface;
    using Microsoft.Xna.Framework;

    public class SpeedComponent : IComponent
    {
        private Vector2 speed;

        public SpeedComponent(Vector2 speed)
            : base()
        {
            this.speed = speed;
            this.PreviousPosition = Vector2.Zero;
        }

        public Vector2 Speed
        {
            get { return speed; }
            set { speed = value; }
        }

        public float SpeedX
        {
            get { return speed.X; }
            set { speed.X = value; }
        }

        public float SpeedY
        {
            get { return speed.Y; }
            set { speed.Y = value; }
        }

        public Vector2 PreviousPosition;
    }
}
