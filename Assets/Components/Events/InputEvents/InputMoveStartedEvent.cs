using UnityEngine;

namespace GrayscaleBlock3D.Components.Events.InputEvents
{
    internal struct InputMoveStartedEvent
    {
        public Vector2 Axis;
        public bool isFall;
    }
}