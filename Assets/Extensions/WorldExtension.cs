using Leopotam.Ecs;
using GrayscaleBlock3D.Systems.Models.Data;
using UnityEngine;

namespace GrayscaleBlock3D.Extensions
{
    public static class WorldExtension
    {
        public static void SendMessage<T>(this EcsWorld world, in T messageEvent)
            where T : struct
        {
            ref var eventComponent = ref world.NewEntity().Get<T>();
            eventComponent = messageEvent;
        }

        public static bool EqualsColor(this Blockube blockube1, Blockube blockube2)
        {
            return blockube1.NumberColor.Equals(blockube2.NumberColor);
        }

        public static Vector2Int GetIntVector2(this Vector2 vector)
        {
            return new Vector2Int((int)vector.x, (int)vector.y);
        }
    }
}