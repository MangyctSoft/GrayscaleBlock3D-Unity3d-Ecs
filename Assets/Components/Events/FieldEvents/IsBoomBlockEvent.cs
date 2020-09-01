using UnityEngine;
using Leopotam.Ecs;
using System.Collections.Generic;

namespace GrayscaleBlock3D.Components.Events.FieldEevents
{
    internal struct IsBoomBlockEvent
    {
        public IEnumerable<Vector2> Position;
    }
}