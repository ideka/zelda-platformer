namespace ZeldaPlatformerLibrary.Systems
{
    using Artemis;
    using Artemis.System;
    using Microsoft.Xna.Framework.Input;
    using ZeldaPlatformerLibrary.Components;

    public class PlayerInputSystem : EntityProcessingSystem
    {
        public PlayerInputSystem()
            : base(
            Aspect.All(
            typeof(PlayerInputComponent),
            typeof(InputIntentComponent)))
        {
        }

        public override void Process(Entity entity)
        {
            InputIntentComponent inputIntent = entity.GetComponent<InputIntentComponent>();
            KeyboardState keyboard = Keyboard.GetState();

            inputIntent.Left = keyboard.IsKeyDown(Keys.S);
            inputIntent.Right = keyboard.IsKeyDown(Keys.F);
            inputIntent.Up = keyboard.IsKeyDown(Keys.E);
            inputIntent.Down = keyboard.IsKeyDown(Keys.D);
            inputIntent.Run = keyboard.IsKeyDown(Keys.Space);
        }
    }
}