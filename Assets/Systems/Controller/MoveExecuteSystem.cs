using UnityEngine;
using Leopotam.Ecs;
using GrayscaleBlock3D.Components.Player;
using GrayscaleBlock3D.Components.Events;
using GrayscaleBlock3D.Components.Events.InputEvents;

namespace GrayscaleBlock3D.Systems.Controller
{
    public class MoveExecuteSystem : IEcsRunSystem
    {
        private readonly EcsFilter<MoveComponent, MainBlockComponent> _filter = null;

        void IEcsRunSystem.Run()
        {
            foreach (var i in _filter)
            {
                ref var moveComponent = ref _filter.Get1(i);
                ref var blockComponent = ref _filter.Get2(i);

                blockComponent.CurrentBlock.MoveTo(moveComponent.Direction * moveComponent.Speed);
                var positionNew = blockComponent.CurrentBlock.Position;

                // ref var entity = ref _filter.GetEntity(i);
                // entity.Get<InputMoveCanceledEvent>();
            }
        }
    }
}