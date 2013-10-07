namespace ZeldaPlatformerLibrary.Systems
{
    using Artemis;
    using Artemis.System;
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;
    using ZeldaPlatformerLibrary.Components;

    public class SpriteSystem : EntityProcessingSystem
    {
        public SpriteSystem()
            : base(
            Aspect.All(
            typeof(SpriteComponent),
            typeof(ContentManagerComponent)))
        {
        }

        public override void Process(Entity entity)
        {
            SpriteComponent sprite = entity.GetComponent<SpriteComponent>();

            if (sprite.Name == null)
            {
                sprite.Index = 0;
                sprite.Texture = null;
                sprite.Speed = new double[1] { 0 };
                sprite.Anchor = Vector2.Zero;
                return;
            }

            if (sprite.Name == sprite.CurrentName)
            {
                return;
            }

            Texture2D newTexture = entity.GetComponent<ContentManagerComponent>().ContentManager.Load<Texture2D>(sprite.Name);
            MetaSprite metaSprite = MetaSprite.MetaSpriteDict[sprite.Name];

            sprite.Index = 0;
            sprite.Texture = newTexture;
            sprite.Speed = metaSprite.Speed;
            sprite.Anchor = metaSprite.Anchor;
        }
    }
}