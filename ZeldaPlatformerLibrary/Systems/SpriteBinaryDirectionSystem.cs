namespace ZeldaPlatformerLibrary.Systems
{
    using Artemis;
    using Artemis.System;
    using Microsoft.Xna.Framework.Graphics;
    using ZeldaPlatformerLibrary.Components;

    public class SpriteBinaryDirectionSystem : EntityProcessingSystem
    {
        public SpriteBinaryDirectionSystem()
            : base(
            Aspect.All(
            typeof(BinaryDirectionComponent),
            typeof(SpriteComponent)))
        {
        }

        public override void Process(Entity entity)
        {
            BinaryDirectionComponent binaryDirection = entity.GetComponent<BinaryDirectionComponent>();
            SpriteComponent sprite = entity.GetComponent<SpriteComponent>();

            sprite.Effects = SpriteEffects.None;
            if (binaryDirection.Direction == BinaryDirection.Left)
	        {
                sprite.Effects = SpriteEffects.FlipHorizontally;
	        }
        }
    }
}