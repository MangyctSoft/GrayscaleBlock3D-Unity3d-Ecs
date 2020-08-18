using Leopotam.Ecs;
using GrayscaleBlock3D.AppSettings;
using GrayscaleBlock3D.Components.Player;
using GrayscaleBlock3D.Components.Timers;
using GrayscaleBlock3D.Components.Events;
using GrayscaleBlock3D.Extensions;

using UnityEngine;

namespace GrayscaleBlock3D.Systems.Controller
{
    internal sealed class TimersSystem : IEcsRunSystem
    {
        // auto-injected fields.
        private readonly EcsFilter<TimerFallingComponent, TimerFallingSetupComponent> _filterFalling = null;
        private readonly EcsFilter<TimerMergeComponent> _filterMerge = null;
        private readonly EcsFilter<TimerRemoveLineComponent> _filterRemoveLine = null;

        void IEcsRunSystem.Run()
        {
            MadeTimerFalling();
            MadeTimerMerge();
            MadeTimerRemoveLine();
        }
        private void MadeTimerFalling()
        {
            foreach (var i in _filterFalling)
            {
                ref var timer = ref _filterFalling.Get1(i);
                timer.TimeLostSec -= Time.deltaTime;

                if (timer.TimeLostSec <= 0)
                {
                    ref var option = ref _filterFalling.Get2(i);
                    _filterFalling.GetEntity(i).Get<MoveComponent>().Speed = option.FallUnit;
                    timer.TimeLostSec = option.FallTimeSec;
                    //_filterFalling.GetEntity(i).Del<TimerFallingComponent>();
                }
            }
        }
        private void MadeTimerMerge()
        {
            foreach (var i in _filterMerge)
            {
                ref var timer = ref _filterMerge.Get1(i);
                timer.TimeLostSec -= Time.deltaTime;

                if (timer.TimeLostSec <= 0)
                {
                    _filterMerge.GetEntity(i).Del<TimerMergeComponent>();
                }
            }
        }
        private void MadeTimerRemoveLine()
        {
            foreach (var i in _filterRemoveLine)
            {
                ref var timer = ref _filterRemoveLine.Get1(i);
                timer.TimeLostSec -= Time.deltaTime;

                if (timer.TimeLostSec <= 0)
                {
                    _filterRemoveLine.GetEntity(i).Del<TimerRemoveLineComponent>();
                }
            }
        }
    }
}