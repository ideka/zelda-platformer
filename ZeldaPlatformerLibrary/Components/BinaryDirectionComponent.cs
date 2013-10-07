namespace ZeldaPlatformerLibrary.Components
{
    using Artemis.Interface;

    public class BinaryDirectionComponent : IComponent
    {
        public BinaryDirectionComponent()
            : base()
        {
            this.Direction = BinaryDirection.Right;
        }

        public BinaryDirection Direction { get; set; }
    }

    public enum BinaryDirection
    {
        Left = -1,
        Right = 1
    }
}