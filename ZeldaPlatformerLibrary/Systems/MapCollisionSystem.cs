namespace ZeldaPlatformerLibrary.Systems
{
    using Artemis;
    using Artemis.System;
    using FuncWorks.XNA.XTiled;
    using Microsoft.Xna.Framework;
    using System;
    using ZeldaPlatformerLibrary.Components;

    public class MapCollisionSystem : EntityProcessingSystem
    {
        public MapCollisionSystem()
            : base(
            Aspect.All(
            typeof(PositionComponent),
            typeof(SpeedComponent),
            typeof(AABBComponent)))
        {
        }

        public override void Process(Entity entity)
        {
            PositionComponent position = entity.GetComponent<PositionComponent>();
            SpeedComponent speed = entity.GetComponent<SpeedComponent>();
            AABBComponent aabb = entity.GetComponent<AABBComponent>();

            Vector2 new_position = speed.PreviousPosition;
            MapCollisionType type = MoveContactMap(
                ref new_position,
                position.Position - speed.PreviousPosition,
                aabb.Rectangle,
                EntitySystem.BlackBoard.GetEntry<Map>("Map"));
            position.Position = new_position;

            EventSenderComponent eventSender = entity.GetComponent<EventSenderComponent>();
            if (eventSender != null)
            {
                eventSender.Trigger<MapCollisionEventType>(entity, new MapCollisionEventType(type));
            }
        }

        public Rectangle GetAbsoluteAABB(Vector2 position, Rectangle rectangle)
        {
            // Round down Left and Top, round up Right and Down.
            return new Rectangle((int)Math.Floor(position.X) - rectangle.X, (int)Math.Floor(position.Y) - rectangle.Y,
                rectangle.Width + (int)Math.Ceiling(position.X - (int)position.X),
                rectangle.Height + (int)Math.Ceiling(position.Y - (int)position.Y));
        }

        public MapCollisionType MoveContactMap(ref Vector2 position, Vector2 vector, Rectangle rectangle, Map map)
        {
            // TODO: Optimize this method.
            if (vector.X != 0 && vector.Y != 0)
            {
                return MoveContactMap(ref position, vector * Vector2.UnitY, rectangle, map) |
                    MoveContactMap(ref position, vector * Vector2.UnitX, rectangle, map);
            }

            Rectangle prevRect = GetAbsoluteAABB(position, rectangle);
            Rectangle nextRect = GetAbsoluteAABB(position + vector, rectangle);
            MapCollisionType collisionType = MapCollisionType.None;
            position += vector;

            foreach (TileData tileData in map.GetTilesInRegion(new Rectangle(nextRect.X, nextRect.Y, nextRect.Width - 1, nextRect.Height - 1)))
            {
                Tile tile = map.SourceTiles[tileData.SourceID];
                Rectangle cellRect = tileData.Target;

                /*
                 * This must be done because TileData.Target always returns a
                 * rectangle with the center coordinates of the tile, instead
                 * of the top left corner.
                 */
                cellRect.Offset(-cellRect.Width / 2, -cellRect.Height / 2);

                if (vector.X != 0)
                {
                    if ((bool)tile.Properties["left"].AsBoolean && prevRect.Right <= cellRect.Left && nextRect.Right > cellRect.Left)
                    {
                        position.X = cellRect.Left - rectangle.Width + rectangle.X;
                        collisionType |= MapCollisionType.Right;
                    }
                    else if ((bool)tile.Properties["right"].AsBoolean && prevRect.Left >= cellRect.Right && nextRect.Left < cellRect.Right)
                    {
                        position.X = cellRect.Right + rectangle.X;
                        collisionType |= MapCollisionType.Left;
                    }
                }
                if (vector.Y != 0)
                {
                    if ((bool)tile.Properties["top"].AsBoolean && prevRect.Bottom <= cellRect.Top && nextRect.Bottom > cellRect.Top)
                    {
                        position.Y = cellRect.Top - rectangle.Height + rectangle.Y;
                        collisionType |= MapCollisionType.Down;
                    }
                    else if ((bool)tile.Properties["bottom"].AsBoolean && prevRect.Top >= cellRect.Bottom && nextRect.Top < cellRect.Bottom)
                    {
                        position.Y = cellRect.Bottom + rectangle.Y;
                        collisionType |= MapCollisionType.Up;
                    }
                }
            }

            return collisionType;
        }
    }

    public class MapCollisionEventType : EventArgs
    {
        private MapCollisionType type;

        public MapCollisionEventType(MapCollisionType type)
        {
            this.type = type;
        }

        public MapCollisionType Type { get { return type; } }
    }

    [Flags]
    public enum MapCollisionType
    {
        None = 0,
        Up = 1,
        Down = 2,
        Left = 4,
        Right = 8
    }
}