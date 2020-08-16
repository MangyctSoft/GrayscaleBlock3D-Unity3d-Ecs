using Leopotam.Ecs;
using GrayscaleBlock3D.AppSettings;
using GrayscaleBlock3D.Components;
using GrayscaleBlock3D.Components.Player;
using GrayscaleBlock3D.Extensions;
using GrayscaleBlock3D.Components.Events.InputEvents;
using UnityEngine;

namespace GrayscaleBlock3D.Systems.Controller
{
    internal sealed class MoveBesideBorderSystem : IEcsRunSystem
    {
        // auto-injected fields.
        private readonly GameConfiguration _gameConfiguration = null;
        private readonly GameContext _gameContext = null;
        private readonly EcsWorld _world = null;

        private readonly EcsFilter<MoveComponent, MainBlockComponent> _filter = null;

        void IEcsRunSystem.Run()
        {
            foreach (var i in _filter)
            {
                ref var moveComponent = ref _filter.Get1(i);
                ref var blockComponent = ref _filter.Get2(i);

                var position = blockComponent.Blockube.Position;

                if (IsOutBorder(position, out var borderPosition))
                {
                    blockComponent.Blockube.Position = borderPosition;
                    //moveComponent.Speed = 0;
                }

            }
        }

        private bool IsOutBorder(in Vector2 position, out Vector2 borderPosition)
        {
            borderPosition = position;
            var isOutBorder = false;
            if (position.x > _gameConfiguration.SizeField.x - 1)
            {
                borderPosition.x = _gameConfiguration.SizeField.x - 1;
                isOutBorder = true;
            }

            if (position.x <= 0)
            {
                borderPosition.x = 0;
                isOutBorder = true;
            }

            return isOutBorder;
        }
    }
}