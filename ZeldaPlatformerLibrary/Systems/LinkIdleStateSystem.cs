namespace ZeldaPlatformerLibrary.Systems
{
    using Artemis;
    using Artemis.System;
    using ZeldaPlatformerLibrary.Components;

    public class LinkIdleStateSystem : EntityProcessingSystem
    {
        public LinkIdleStateSystem()
            : base(
            Aspect.All(
            // State components.
            typeof(LinkOnGroundStateComponent),
            typeof(LinkIdleStateComponent),
            // Regular components.
            typeof(GoalSpeedComponent),
            typeof(BinaryDirectionComponent),
            typeof(SpriteComponent)))
        {
        }

        public override void Process(Entity entity)
        {
            LinkOnGroundStateComponent onGround = entity.GetComponent<LinkOnGroundStateComponent>();
            GoalSpeedComponent goalSpeed = entity.GetComponent<GoalSpeedComponent>();
            BinaryDirectionComponent binaryDirection = entity.GetComponent<BinaryDirectionComponent>();
            SpriteComponent sprite = entity.GetComponent<SpriteComponent>();

            sprite.Name = "spr/Link/Stand";
            goalSpeed.AccelX = onGround.Accel * (int)binaryDirection.Direction * -1;
            goalSpeed.GoalSpeedX = 0;
        }
    }
}