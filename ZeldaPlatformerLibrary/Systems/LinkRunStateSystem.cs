namespace ZeldaPlatformerLibrary.Systems
{
    using Artemis;
    using Artemis.System;
    using System;
    using ZeldaPlatformerLibrary.Components;

    public class LinkRunStateSystem : EntityProcessingSystem
    {
        public LinkRunStateSystem()
            : base(
            Aspect.All(
            // State components.
            typeof(LinkOnGroundStateComponent),
            typeof(LinkRunStateComponent),
            // Regular components.
            typeof(SpeedComponent),
            typeof(GoalSpeedComponent),
            typeof(BinaryDirectionComponent),
            typeof(SpriteComponent)))
        {
        }

        public override void Process(Entity entity)
        {
            LinkOnGroundStateComponent onGround = entity.GetComponent<LinkOnGroundStateComponent>();
            LinkRunStateComponent run = entity.GetComponent<LinkRunStateComponent>();
            SpeedComponent speed = entity.GetComponent<SpeedComponent>();
            GoalSpeedComponent goalSpeed = entity.GetComponent<GoalSpeedComponent>();
            BinaryDirectionComponent binaryDirection = entity.GetComponent<BinaryDirectionComponent>();
            SpriteComponent sprite = entity.GetComponent<SpriteComponent>();

            goalSpeed.AccelX = onGround.Accel * (int)binaryDirection.Direction;
            goalSpeed.GoalSpeedX = run.MaxRunSpeed * (int)binaryDirection.Direction;

            sprite.Name = "spr/Link/Run";
            if (Math.Sign(-speed.SpeedX) == (int)binaryDirection.Direction)
            {
                sprite.Name = "spr/Link/Skid";
            }
        }
    }
}