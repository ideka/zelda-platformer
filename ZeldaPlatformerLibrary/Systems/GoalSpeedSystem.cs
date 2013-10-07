namespace ZeldaPlatformerLibrary.Systems
{
    using Artemis;
    using Artemis.System;
    using Components;
    using System;

    public class GoalSpeedSystem : EntityProcessingSystem
    {
        public GoalSpeedSystem()
            : base(
            Aspect.All(
            typeof(SpeedComponent),
            typeof(GoalSpeedComponent)))
        {
        }

        public override void Process(Entity entity)
        {
            float dt = (float)EntitySystem.BlackBoard.GetEntry<double>("dt");
            GoalSpeedComponent goalSpeed = entity.GetComponent<GoalSpeedComponent>();
            SpeedComponent speed = entity.GetComponent<SpeedComponent>();

            speed.SpeedX = Approach(speed.SpeedX, Math.Abs(goalSpeed.AccelX * dt), goalSpeed.GoalSpeedX);
            speed.SpeedY = Approach(speed.SpeedY, Math.Abs(goalSpeed.AccelY * dt), goalSpeed.GoalSpeedY);
        }

        public static float Approach(float start, float step, float end)
        {
            if (start > end)
            {
                return Math.Max(start - step, end);
            }
            else
            {
                return Math.Min(start + step, end);
            }
        }
    }
}
