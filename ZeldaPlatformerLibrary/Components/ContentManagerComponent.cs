namespace ZeldaPlatformerLibrary.Components
{
    using Artemis.Interface;
    using Microsoft.Xna.Framework.Content;

    public class ContentManagerComponent : IComponent
    {
        public ContentManagerComponent(ContentManager contentManager)
            : base()
        {
            this.ContentManager = contentManager;
        }

        public ContentManager ContentManager { get; set; }
    }
}