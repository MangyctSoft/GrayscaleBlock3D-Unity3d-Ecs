using UnityEngine;
using Leopotam.Ecs;
using GrayscaleBlock3D.Components.Player;
using GrayscaleBlock3D.Components.Timers;
using GrayscaleBlock3D.Components.Events.FieldEevents;

namespace GrayscaleBlock3D.Systems.Controller
{
    internal sealed class RemoveLineTimerStartSystem : IEcsRunSystem
    {
        private readonly EcsFilter<TimerRemoveLineSetupComponent, IsRemoveLineMadeEvent> _filter = null;
        void IEcsRunSystem.Run()
        {
            foreach (var i in _filter)
            {
                ref var timeRemoveLineComponent = ref _filter.Get1(i);
                ref var nextStep = ref _filter.GetEntity(i);
                nextStep.Get<TimerRemoveLineComponent>().TimeLostSec = timeRemoveLineComponent.RemoveTimeSec;
            }
        }
    }
}