namespace ZeldaPlatformerLibrary.Systems
{
    using Artemis;
    using Artemis.System;
    using Components;
    using System;

    public class LinkOnAirStateSystem : EntityProcessingSystem
    {
        public LinkOnAirStateSystem()
            : base(
            Aspect.All(
            // State components.
            typeof(LinkOnAirStateComponent),
            typeof(LinkWalkSpeedStateComponent),
            // Regular components.
            typeof(FSMComponent),
            typeof(EventSenderComponent),
            typeof(SpeedComponent),
            typeof(GoalSpeedComponent),
            typeof(SpriteComponent)))
        {
        }

        public override void OnAdded(Entity entity)
        {
            base.OnAdded(entity);
            LinkOnAirStateComponent onAir = entity.GetComponent<LinkOnAirStateComponent>();
            SpeedComponent speed = entity.GetComponent<SpeedComponent>();
            GoalSpeedComponent goalSpeed = entity.GetComponent<GoalSpeedComponent>();
            EventSenderComponent eventSender = entity.GetComponent<EventSenderComponent>();

            goalSpeed.GoalSpeedY = onAir.MaxVerticalSpeed;
            goalSpeed.AccelY = onAir.Gravity;
            goalSpeed.GoalSpeedX = speed.Speed.X;

            eventSender.GenericEvent += Event;
        }

        public override void OnRemoved(Entity entity)
        {
            base.OnAdded(entity);
            EventSenderComponent eventSender = entity.GetComponent<EventSenderComponent>();

            eventSender.GenericEvent -= Event;
        }

        public override void Process(Entity entity)
        {
            SpeedComponent speed = entity.GetComponent<SpeedComponent>();
            SpriteComponent sprite = entity.GetComponent<SpriteComponent>();

            if (speed.Speed.Y < 0)
            {
                LinkWalkSpeedStateComponent walkSpeed = entity.GetComponent<LinkWalkSpeedStateComponent>();
                if (Math.Abs(speed.SpeedX) > walkSpeed.MaxWalkSpeed)
                {
                    sprite.Name = "spr/Link/JumpFwd";
                }
                else
                {
                    sprite.Name = "spr/Link/Jump";
                }
            }
            else
            {
                sprite.Name = "spr/Link/Fall";
            }
        }

        private void Event(Entity entity, Type eventType, EventArgs pe)
        {
            if (eventType == typeof(MapCollisionEventType))
            {
                MapCollisionEventType e = (MapCollisionEventType)pe;
                if (e.Type.HasFlag(MapCollisionType.Down))
                {
                    FSMComponent fsm = entity.GetComponent<FSMComponent>();
                    fsm.SetState(entity, "idle");
                }
            }
        }
    }
}