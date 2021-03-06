using UnityEngine;
using Leopotam.Ecs;
using GrayscaleBlock3D.Components.Player;
using GrayscaleBlock3D.Components.Timers;
using GrayscaleBlock3D.Components.Events.FieldEevents;

namespace GrayscaleBlock3D.Systems.Controller
{
    internal sealed class MergeTimerStartSystem : IEcsRunSystem
    {
        private readonly EcsFilter<TimerMergingSetupComponent, IsMergeMadeEvent> _filter = null;
        void IEcsRunSystem.Run()
        {
            foreach (var i in _filter)
            {
                ref var timeMergingComponent = ref _filter.Get1(i);
                ref var nextStep = ref _filter.GetEntity(i);
                nextStep.Get<TimerMergeComponent>().TimeLostSec = timeMergingComponent.MergeTimeSec;
            }
        }
    }
}