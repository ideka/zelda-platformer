namespace ZeldaPlatformerLibrary.Components
{
    using Artemis.Interface;

    public class LinkWalkSpeedStateComponent : IComponent
    {
        public LinkWalkSpeedStateComponent(float maxWalkSpeed)
            : base()
        {
            this.MaxWalkSpeed = maxWalkSpeed;
        }

        public float MaxWalkSpeed { get; set; }
    }
}