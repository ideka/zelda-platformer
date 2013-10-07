namespace ZeldaPlatformerLibrary.Systems
{
    using Artemis;
    using Artemis.System;
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;
    using ZeldaPlatformerLibrary.Components;

    public class SpriteRenderSystem : EntityProcessingSystem
    {
        public SpriteRenderSystem()
            : base(
            Aspect.All(
            typeof(SpriteComponent),
            typeof(PositionComponent)))
        {
        }

        public override void Process(Entity entity)
        {
            SpriteComponent sprite = entity.GetComponent<SpriteComponent>();

            if (sprite.Texture == null)
            {
                return;
            }

            SpriteBatch spriteBatch = EntitySystem.BlackBoard.GetEntry<SpriteBatch>("SpriteBatch");
            PositionComponent position = entity.GetComponent<PositionComponent>();

            int width = sprite.Texture.Width / sprite.Speed.Length;
            int height = sprite.Texture.Height;
            int column = (int)sprite.Index;

            spriteBatch.Draw(
                sprite.Texture,
                position.Position,
                new Rectangle(width * column, 0, width, height),
                Color.White,
                0,
                sprite.Anchor,
                1,
                sprite.Effects,
                0);
        }
    }
}