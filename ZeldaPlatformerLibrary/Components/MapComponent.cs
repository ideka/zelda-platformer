namespace ZeldaPlatformerLibrary.Components
{
    using Artemis.Interface;
    using FuncWorks.XNA.XTiled;

    public class MapComponent : IComponent
    {
        public MapComponent(Map map)
            : base()
        {
            this.Map = map;
        }

        public Map Map { get; set; }
    }
}