namespace ZeldaPlatformerLibrary.Components
{
    using Artemis;
    using Artemis.Interface;
    using System;

    public class EventSenderComponent : IComponent
    {
        public EventSenderComponent()
            : base()
        {
        }

        public event GenericEventHandler GenericEvent;

        public void Trigger<T>(Entity entity, T e) where T : EventArgs
        {
            GenericEventHandler handler = this.GenericEvent;

            if (handler != null)
            {
                handler(entity, typeof(T), e);
            }
        }

        public delegate void GenericEventHandler(Entity entity, Type eventType, EventArgs e);
    }
}