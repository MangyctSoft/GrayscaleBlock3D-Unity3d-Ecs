using System.Collections.Generic;
using UnityEngine;
using Leopotam.Ecs;

namespace GrayscaleBlock3D.Components.Events.FieldEevents
{
    internal struct RemoveLineEventX
    {
        public IEnumerable<Vector2Int> Positions;
    }
}