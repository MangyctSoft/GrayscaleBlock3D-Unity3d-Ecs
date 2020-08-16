using Leopotam.Ecs;
using GrayscaleBlock3D.AppSettings;
using GrayscaleBlock3D.Components.Player;
using GrayscaleBlock3D.Components.Timers;
using GrayscaleBlock3D.Components.Events;

namespace GrayscaleBlock3D.Systems.Controller
{
    internal sealed class FallTimerStartSystem : IEcsRunSystem
    {
        private readonly EcsFilter<TimerFallingSetupComponent, IsFallMadeEvent> _filter = null;
        void IEcsRunSystem.Run()
        {
            foreach (var i in _filter)
            {
                ref var timeFallingComponent = ref _filter.Get1(i);
                ref var move = ref _filter.GetEntity(i);

                move.Get<TimerFallingComponent>().TimeLostSec = timeFallingComponent.FallTimeSec;
            }
        }
    }
}