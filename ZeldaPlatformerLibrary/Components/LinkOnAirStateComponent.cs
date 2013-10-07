namespace ZeldaPlatformerLibrary.Components
{
    using Artemis.Interface;

    public class LinkOnAirStateComponent : IComponent
    {
        public LinkOnAirStateComponent(float maxVerticalSpeed, float gravity)
            : base()
        {
            this.MaxVerticalSpeed = maxVerticalSpeed;
            this.Gravity = gravity;
        }

        public float MaxVerticalSpeed { get; set; }
        public float Gravity { get; set; }
    }
}