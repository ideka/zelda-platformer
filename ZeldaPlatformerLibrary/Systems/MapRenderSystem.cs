namespace ZeldaPlatformerLibrary.Systems
{
    using Artemis;
    using Artemis.System;
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;
    using ZeldaPlatformerLibrary.Components;

    public class MapRenderSystem : EntityProcessingSystem
    {
        public MapRenderSystem()
            : base(
            Aspect.All(
            typeof(MapComponent)))
        {
        }

        public override void Process(Entity entity)
        {
            SpriteBatch spriteBatch = EntitySystem.BlackBoard.GetEntry<SpriteBatch>("SpriteBatch");
            MapComponent map = entity.GetComponent<MapComponent>();

            map.Map.Draw(
                spriteBatch,
                new Rectangle(0, 0, map.Map.Width * map.Map.TileWidth, map.Map.Height * map.Map.TileHeight));
        }
    }
}