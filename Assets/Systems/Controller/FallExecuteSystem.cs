using Leopotam.Ecs;
using GrayscaleBlock3D.Components.Player;
using GrayscaleBlock3D.Components.Timers;

namespace GrayscaleBlock3D.Systems.Controller
{
    internal sealed class FallExecuteSystem : IEcsRunSystem
    {
        // auto-injected fields.
        private readonly EcsFilter<MoveComponent, TimerFallingComponent> _filter = null;

        void IEcsRunSystem.Run()
        {
            foreach (var i in _filter)
            {
                _filter.GetEntity(i).Get<MoveComponent>().Speed = 0;
            }
        }
    }
}