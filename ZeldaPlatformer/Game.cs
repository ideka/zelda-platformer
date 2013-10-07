namespace ZeldaPlatformer
{
    using Artemis;
    using Artemis.Interface;
    using Artemis.System;
    using FuncWorks.XNA.XTiled;
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;
    using Microsoft.Xna.Framework.Input;
    using System.Collections.Generic;
    using ZeldaPlatformerLibrary;
    using ZeldaPlatformerLibrary.Components;

    public class Game : Microsoft.Xna.Framework.Game
    {
        private GraphicsDeviceManager graphics;
        private SpriteBatch spriteBatch;
        private EntityWorld world;

        public Game()
            : base()
        {
            // Graphics.
            this.graphics = new GraphicsDeviceManager(this);
            this.graphics.PreferredBackBufferWidth = 640;
            this.graphics.PreferredBackBufferHeight = 480;

            // Content.
            this.Content.RootDirectory = "Content";

            // World.
            this.world = new EntityWorld();
            AllSystems.AddSystems(this.world);
        }

        protected override void Initialize()
        {
            base.Initialize();
            this.world.InitializeAll(true);
        }

        protected override void LoadContent()
        {
            base.LoadContent();
            this.spriteBatch = new SpriteBatch(this.GraphicsDevice);
            EntitySystem.BlackBoard.SetEntry<SpriteBatch>("SpriteBatch", this.spriteBatch);
            EntitySystem.BlackBoard.SetEntry<Map>("Map", Content.Load<Map>("Test"));
            MetaSprite.LoadContent(this.Content);

            // Link.
            Entity link = this.world.CreateEntity();
            link.AddComponent(new ContentManagerComponent(this.Content));
            link.AddComponent(new EventSenderComponent());
            link.AddComponent(new SpriteComponent());
            link.AddComponent(new PositionComponent(new Vector2(250, 100)));  // Default position.
            link.AddComponent(new SpeedComponent(Vector2.Zero));
            link.AddComponent(new GoalSpeedComponent(Vector2.Zero, Vector2.Zero));
            link.AddComponent(new AABBComponent(new Rectangle(8, 49, 16, 49)));  // AABB boundaries.
            link.AddComponent(new PlayerInputComponent());
            link.AddComponent(new InputIntentComponent());
            link.AddComponent(new BinaryDirectionComponent());
            link.AddComponent(new FSMComponent(
                new Dictionary<string, IComponent>()
                {
                    // walkSpeed is a separate component because two states have to access it.
                    {"walkSpeed", new LinkWalkSpeedStateComponent(60)},  // Max walking speed.
                    {"onGround", new LinkOnGroundStateComponent(370, 270)},  // Running and walking acceleration/deceleration, jump force.
                    {"idle", new LinkIdleStateComponent()},
                    {"walk", new LinkWalkStateComponent()},
                    {"run", new LinkRunStateComponent(150)},  // Max running speed.
                    {"onAir", new LinkOnAirStateComponent(450, 630)},  // Max vertical speed and gravity.
                },
                new Dictionary<string, List<string>>()
                {
                    {"idle", new List<string>() {"onGround", "idle"}},
                    {"walk", new List<string>() {"onGround", "walk", "walkSpeed"}},
                    {"run", new List<string>() {"onGround", "run"}},
                    {"onAir", new List<string>() {"onAir", "walkSpeed"}}
                }, link, "idle"));

            // Map.
            Entity map = this.world.CreateEntity();
            map.AddComponent(new MapComponent(EntitySystem.BlackBoard.GetEntry<Map>("Map")));
        }

        protected override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            if (Keyboard.GetState().IsKeyDown(Keys.Escape))
            {
                this.Exit();
            }

            EntitySystem.BlackBoard.SetEntry<GameTime>("GameTime", gameTime);
            EntitySystem.BlackBoard.SetEntry<double>("dt", gameTime.ElapsedGameTime.TotalSeconds);
            this.world.Update();
        }

        protected override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);
            this.GraphicsDevice.Clear(Color.CornflowerBlue);

            EntitySystem.BlackBoard.SetEntry<GameTime>("GameTime", gameTime);
            EntitySystem.BlackBoard.SetEntry<double>("dt", gameTime.ElapsedGameTime.TotalSeconds);
            this.spriteBatch.Begin();
            this.world.Draw();
            this.spriteBatch.End();
        }
    }
}