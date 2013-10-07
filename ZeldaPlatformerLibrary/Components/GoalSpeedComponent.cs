namespace ZeldaPlatformerLibrary.Components
{
    using Artemis.Interface;
    using Microsoft.Xna.Framework;

    public class GoalSpeedComponent : IComponent
    {
        private Vector2 goalSpeed;
        private Vector2 accel;

        public GoalSpeedComponent(Vector2 goalSpeed, Vector2 accel)
            : base()
        {
            this.GoalSpeed = goalSpeed;
            this.Accel = accel;
        }

        public Vector2 GoalSpeed
        {
            get { return goalSpeed; }
            set { goalSpeed = value; }
        }

        public float GoalSpeedX
        {
            get { return goalSpeed.X; }
            set { goalSpeed.X = value; }
        }

        public float GoalSpeedY
        {
            get { return goalSpeed.Y; }
            set { goalSpeed.Y = value; }
        }

        public Vector2 Accel
        {
            get { return accel; }
            set { accel = value; }
        }

        public float AccelX
        {
            get { return accel.X; }
            set { accel.X = value; }
        }

        public float AccelY
        {
            get { return accel.Y; }
            set { accel.Y = value; }
        }
    }
}
