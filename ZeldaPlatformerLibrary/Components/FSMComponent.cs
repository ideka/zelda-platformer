namespace ZeldaPlatformerLibrary.Components
{
    using Artemis;
    using Artemis.Interface;
    using Artemis.Manager;
    using System.Collections.Generic;

    public class FSMComponent : IComponent
    {
        public FSMComponent(IDictionary<string, IComponent> components, IDictionary<string, List<string>> states, Entity entity, string stateName)
            : base()
        {
            this.Components = (Dictionary<string, IComponent>)components;
            this.States = (Dictionary<string, List<string>>)states;
            this.CurrentState = new List<string>();
            this.SetState(entity, stateName);
        }

        public Dictionary<string, IComponent> Components { get; set; }
        public Dictionary<string, List<string>> States { get; set; }
        public List<string> CurrentState { get; set; }

        public void SetState(Entity entity, string stateName)
        {
            List<string> newState = this.States[stateName];

            if (this.CurrentState == newState)
            {
                return;
            }

            foreach (string componentName in this.CurrentState)
            {
                if (newState.IndexOf(componentName) == -1)
                {
                    entity.RemoveComponent(ComponentTypeManager.GetTypeFor(this.Components[componentName].GetType()));
                }
            }

            foreach (string componentName in newState)
            {
                if (this.CurrentState.IndexOf(componentName) == -1)
                {
                    entity.AddComponent(this.Components[componentName]);
                }
            }

            this.CurrentState = newState;
        }
    }
}