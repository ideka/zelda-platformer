namespace ZeldaPlatformerLibrary.Components
{
    using Artemis.Interface;
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Content;
    using Microsoft.Xna.Framework.Graphics;
    using System.Collections.Generic;

    public class SpriteComponent : IComponent
    {
        private string currentName;
        private Texture2D texture;

        public SpriteComponent(string name = null)
            : base()
        {
            this.Name = name;
            this.Index = 0;
            this.CSpeed = 1;
            this.Effects = SpriteEffects.None;

            this.currentName = null;

            this.texture = null;
            this.Speed = new double[1] { 0 };
            this.Anchor = Vector2.Zero;
        }

        public string Name { get; set; }
        public double Index { get; set; }
        public double CSpeed { get; set; }
        public SpriteEffects Effects { get; set; }

        public string CurrentName { get { return currentName; } }

        public Texture2D Texture { get { return texture; } set { currentName = Name; texture = value; } }
        public double[] Speed { get; set; }
        public Vector2 Anchor { get; set; }
    }

    public class MetaSprite
    {
        public static Dictionary<string, MetaSprite> MetaSpriteDict { get; set; }
        public double[] Speed { get; set; }
        public Vector2 Anchor { get; set; }

        public static void LoadContent(ContentManager contentManager)
        {
            MetaSpriteDict = contentManager.Load<Dictionary<string, MetaSprite>>("MetaSprite");
        }
    }
}