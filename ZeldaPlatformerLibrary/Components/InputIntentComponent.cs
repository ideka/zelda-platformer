namespace ZeldaPlatformerLibrary.Components
{
    using Artemis.Interface;

    public class InputIntentComponent : IComponent
    {
        public InputIntentComponent()
            : base()
        {
            this.Left = false;
            this.Right = false;
            this.Up = false;
            this.Down = false;
            this.Run = false;
        }

        public bool Left { get; set; }
        public bool Right { get; set; }
        public bool Up { get; set; }
        public bool Down { get; set; }
        public bool Run { get; set; }
    }
}