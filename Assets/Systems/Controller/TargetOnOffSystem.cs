using Leopotam.Ecs;
using GrayscaleBlock3D.Components.Player;
using GrayscaleBlock3D.Components.Events.FieldEevents;
using GrayscaleBlock3D.Components.Events.InputEvents;

namespace GrayscaleBlock3D.Systems.Controller
{
    internal sealed class TargetOnOffSystem : IEcsRunSystem
    {
        private readonly EcsFilter<TargetEvent> _filterTargetEvent = null;
        private readonly EcsFilter<InputFallStartedEvent> _filterFallEvent = null;
        private readonly EcsFilter<TargetComponent> _filterTarget = null;
        void IEcsRunSystem.Run()
        {
            foreach (var i in _filterTargetEvent)
            {
                SetTargetActive(true);
            }

            foreach (var i in _filterFallEvent)
            {
                SetTargetActive(false);
            }
        }
        private void SetTargetActive(in bool active)
        {
            foreach (var i in _filterTarget)
            {
                ref var target = ref _filterTarget.Get1(i);

                target.Target.SetActive(active);
            }
        }
    }
}