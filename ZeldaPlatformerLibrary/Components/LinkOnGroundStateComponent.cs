namespace ZeldaPlatformerLibrary.Components
{
    using Artemis.Interface;

    public class LinkOnGroundStateComponent : IComponent
    {
        public LinkOnGroundStateComponent(float accel, float jumpForce)
            : base()
        {
            this.Accel = accel;
            this.JumpForce = jumpForce;
        }

        public float Accel { get; set; }
        public float JumpForce { get; set; }
    }
}