namespace ZeldaPlatformerLibrary.Systems
{
    using Artemis;
    using Artemis.System;
    using ZeldaPlatformerLibrary.Components;

    public class SpriteAnimationSystem : EntityProcessingSystem
    {
        public SpriteAnimationSystem()
            : base(
            Aspect.All(
            typeof(SpriteComponent)))
        {
        }

        public override void Process(Entity entity)
        {
            double dt = EntitySystem.BlackBoard.GetEntry<double>("dt");
            SpriteComponent sprite = entity.GetComponent<SpriteComponent>();

            sprite.Index += sprite.Speed[(int)sprite.Index] * sprite.CSpeed * dt;
            sprite.Index %= sprite.Speed.Length;
        }
    }
}