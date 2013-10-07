namespace ZeldaPlatformerLibrary.Systems
{
    using Artemis;
    using Artemis.System;
    using ZeldaPlatformerLibrary.Components;

    public class SpeedSystem : EntityProcessingSystem
    {
        public SpeedSystem()
            : base(
            Aspect.All(
            typeof(PositionComponent),
            typeof(SpeedComponent)))
        {
        }

        public override void Process(Entity entity)
        {
            float dt = (float)EntitySystem.BlackBoard.GetEntry<double>("dt");
            PositionComponent position = entity.GetComponent<PositionComponent>();
            SpeedComponent speed = entity.GetComponent<SpeedComponent>();

            speed.PreviousPosition = position.Position;
            position.Position += speed.Speed * dt;
        }
    }
}