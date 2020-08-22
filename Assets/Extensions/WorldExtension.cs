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

        public static bool EqualsColor(this Blockube blockube1, Blockube blockube2) => blockube1.NumberColor.Equals(blockube2.NumberColor);
        public static Vector2Int GetIntVector2(this Vector2 vector) => new Vector2Int((int)vector.x, (int)vector.y);
        public static Vector2Int GetIntVector2(this Vector3 vector) => new Vector2Int((int)vector.x, (int)vector.y);
        public static void RotateActive(this GameObject gameObject) => gameObject.SetActive(gameObject.activeSelf ? false : true);
    }
}