namespace ZeldaPlatformerLibrary.Systems
{
    using Artemis;
    using Artemis.System;
    using ZeldaPlatformerLibrary.Components;

    public class LinkWalkStateSystem : EntityProcessingSystem
    {
        public LinkWalkStateSystem()
            : base(
            Aspect.All(
            // State components.
            typeof(LinkOnGroundStateComponent),
            typeof(LinkWalkStateComponent),
            typeof(LinkWalkSpeedStateComponent),
            // Regular components.
            typeof(GoalSpeedComponent),
            typeof(BinaryDirectionComponent),
            typeof(SpriteComponent)))
        {
        }

        public override void OnAdded(Entity entity)
        {
            base.OnAdded(entity);
            SpriteComponent sprite = entity.GetComponent<SpriteComponent>();

            sprite.Name = "spr/Link/Walk";
        }

        public override void Process(Entity entity)
        {
            LinkOnGroundStateComponent onGround = entity.GetComponent<LinkOnGroundStateComponent>();
            LinkWalkSpeedStateComponent walkSpeed = entity.GetComponent<LinkWalkSpeedStateComponent>();
            GoalSpeedComponent goalSpeed = entity.GetComponent<GoalSpeedComponent>();
            BinaryDirectionComponent binaryDirection = entity.GetComponent<BinaryDirectionComponent>();

            goalSpeed.AccelX = onGround.Accel * (int)binaryDirection.Direction;
            goalSpeed.GoalSpeedX = walkSpeed.MaxWalkSpeed * (int)binaryDirection.Direction;
        }
    }
}