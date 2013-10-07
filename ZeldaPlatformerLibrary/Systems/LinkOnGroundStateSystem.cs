namespace ZeldaPlatformerLibrary.Systems
{
    using Artemis;
    using Artemis.System;
    using System;
    using System.Collections.Generic;
    using ZeldaPlatformerLibrary.Components;

    public class LinkOnGroundStateSystem : EntityProcessingSystem
    {
        public LinkOnGroundStateSystem()
            : base(
            Aspect.All(
            // State components.
            typeof(LinkOnGroundStateComponent),
            // Regular components.
            typeof(FSMComponent),
            typeof(EventSenderComponent),
            typeof(InputIntentComponent),
            typeof(BinaryDirectionComponent)))
        {
        }

        public override void OnAdded(Entity entity)
        {
            base.OnAdded(entity);
            SpeedComponent speed = entity.GetComponent<SpeedComponent>();
            EventSenderComponent eventSender = entity.GetComponent<EventSenderComponent>();

            speed.SpeedY = 0;
            eventSender.GenericEvent += Event;
        }

        public override void OnRemoved(Entity entity)
        {
            base.OnAdded(entity);
            EventSenderComponent eventSender = entity.GetComponent<EventSenderComponent>();

            eventSender.GenericEvent -= Event;
        }

        protected override void ProcessEntities(IDictionary<int, Entity> entities)
        {
            // This is needed because an entity may unregister from this system while it's processing.
            Dictionary<int, Entity> entitiesCopy = new Dictionary<int, Entity>(entities);
            base.ProcessEntities(entitiesCopy);
        }

        public override void Process(Entity entity)
        {
            FSMComponent fsm = entity.GetComponent<FSMComponent>();
            InputIntentComponent inputIntent = entity.GetComponent<InputIntentComponent>();
            BinaryDirectionComponent binaryDirection = entity.GetComponent<BinaryDirectionComponent>();

            if (inputIntent.Up)
            {
                LinkOnGroundStateComponent onGround = entity.GetComponent<LinkOnGroundStateComponent>();
                SpeedComponent speed = entity.GetComponent<SpeedComponent>();
                speed.SpeedY = -onGround.JumpForce;
                fsm.SetState(entity, "onAir");
                return;
            }
            if (inputIntent.Left ^ inputIntent.Right)
            {
                if (inputIntent.Left)
                {
                    binaryDirection.Direction = BinaryDirection.Left;
                }
                if (inputIntent.Right)
                {
                    binaryDirection.Direction = BinaryDirection.Right;
                }

                if (inputIntent.Run)
                {
                    fsm.SetState(entity, "run");
                    return;
                }
                fsm.SetState(entity, "walk");
                return;
            }
            fsm.SetState(entity, "idle");
        }

        private void Event(Entity entity, Type eventType, EventArgs pe)
        {
            if (eventType == typeof(MapCollisionEventType))
            {
                MapCollisionEventType e = (MapCollisionEventType)pe;
                if (!e.Type.HasFlag(MapCollisionType.Down))
                {
                    FSMComponent fsm = entity.GetComponent<FSMComponent>();
                    fsm.SetState(entity, "onAir");
                }
                else
                {
                    SpeedComponent speed = entity.GetComponent<SpeedComponent>();
                    speed.SpeedY = 0;
                }
            }
        }
    }
}