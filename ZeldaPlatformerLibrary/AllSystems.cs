namespace ZeldaPlatformerLibrary
{
    using Artemis;
    using ZeldaPlatformerLibrary.Systems;

    public class AllSystems
    {
        public static void AddSystems(EntityWorld world)
        {
            // Update.
            // Input.
            world.SystemManager.SetSystem<PlayerInputSystem>(new PlayerInputSystem(), Artemis.Manager.GameLoopType.Update);
            // States.
            world.SystemManager.SetSystem<LinkOnGroundStateSystem>(new LinkOnGroundStateSystem(), Artemis.Manager.GameLoopType.Update);
            world.SystemManager.SetSystem<LinkIdleStateSystem>(new LinkIdleStateSystem(), Artemis.Manager.GameLoopType.Update);
            world.SystemManager.SetSystem<LinkWalkStateSystem>(new LinkWalkStateSystem(), Artemis.Manager.GameLoopType.Update);
            world.SystemManager.SetSystem<LinkRunStateSystem>(new LinkRunStateSystem(), Artemis.Manager.GameLoopType.Update);
            world.SystemManager.SetSystem<LinkOnAirStateSystem>(new LinkOnAirStateSystem(), Artemis.Manager.GameLoopType.Update);
            // Physics.
            world.SystemManager.SetSystem<GoalSpeedSystem>(new GoalSpeedSystem(), Artemis.Manager.GameLoopType.Update);
            world.SystemManager.SetSystem<SpeedSystem>(new SpeedSystem(), Artemis.Manager.GameLoopType.Update);
            world.SystemManager.SetSystem<MapCollisionSystem>(new MapCollisionSystem(), Artemis.Manager.GameLoopType.Update);
            // Sprite.
            world.SystemManager.SetSystem<SpriteSystem>(new SpriteSystem(), Artemis.Manager.GameLoopType.Update);
            world.SystemManager.SetSystem<SpriteBinaryDirectionSystem>(new SpriteBinaryDirectionSystem(), Artemis.Manager.GameLoopType.Update);
            world.SystemManager.SetSystem<SpriteAnimationSystem>(new SpriteAnimationSystem(), Artemis.Manager.GameLoopType.Update);

            // Draw.
            world.SystemManager.SetSystem<MapRenderSystem>(new MapRenderSystem(), Artemis.Manager.GameLoopType.Draw);
            world.SystemManager.SetSystem<SpriteRenderSystem>(new SpriteRenderSystem(), Artemis.Manager.GameLoopType.Draw);
        }
    }
}