using System;
using Leopotam.Ecs;

namespace GrayscaleBlock3D.Components.Player
{
    [Serializable]
    internal struct TimerFallingSetupComponent
    {
        public float FallTimeSec;
        public float FallUnit;
    }
}