namespace ZeldaPlatformerLibrary.Components
{
    using Artemis.Interface;

    public class LinkRunStateComponent : IComponent
    {
        public LinkRunStateComponent(float maxRunSpeed)
            : base()
        {
            this.MaxRunSpeed = maxRunSpeed;
        }

        public float MaxRunSpeed { get; set; }
    }
}