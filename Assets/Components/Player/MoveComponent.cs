using System;
using UnityEngine;

namespace GrayscaleBlock3D.Components.Player
{
    [Serializable]
    internal struct MoveComponent
    {
        public Vector2 Direction;
        public float Speed;
    }
}