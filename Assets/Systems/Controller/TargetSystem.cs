using Leopotam.Ecs;
using GrayscaleBlock3D.AppSettings;
using GrayscaleBlock3D.Components.Player;
using GrayscaleBlock3D.Components.Events.FieldEevents;
using GrayscaleBlock3D.Components.Events.InputEvents;
using GrayscaleBlock3D.Extensions;
using UnityEngine;

namespace GrayscaleBlock3D.Systems.Controller
{
    internal sealed class TargetSystem : IEcsRunSystem
    {
        private readonly GameContext _gameContext = null;
        private readonly GameConfiguration _gameConfiguration = null;
        private readonly EcsFilter<TargetEvent> _filter = null;
        private readonly EcsFilter<InputMoveStartedEvent> _filterMoveEvent = null;
        private readonly EcsFilter<MainBlockComponent> _filterBlock = null;
        private readonly EcsFilter<TargetComponent> _filterTarget = null;
        void IEcsRunSystem.Run()
        {
            RunFilter(_filterMoveEvent);
            RunFilter(_filter);
        }
        private void RunFilter(in EcsFilter filter)
        {
            foreach (var i in filter)
            {
                SetTarget();
            }
        }
        private void SetTarget()
        {
            foreach (var i in _filterBlock)
            {
                ref var block = ref _filterBlock.Get1(i);

                var posX = (int)block.CurrentBlock.Position.x;
                var posY = _gameContext.RedLine[posX];
                var material = _gameConfiguration.Normal;
                if (posY > 0)
                {
                    var numberColor = block.CurrentBlock.NumberColor;
                    material = SetMaterial(numberColor, new Vector2(posX, posY).GetIntVector2());
                }
                SetPosition(material, new Vector2(posX, posY));
            }
        }
        private void SetPosition(in Material material, in Vector2 position)
        {
            foreach (var i in _filterTarget)
            {
                ref var target = ref _filterTarget.Get1(i);

                target.Target.transform.position = position;
                target.Renderer.material = material;
            }
        }
        private Material SetMaterial(in int numberColor, in Vector2Int position)
        {
            var numberColorToField = _gameContext.GameField[position.x, position.y - 1].NumberColor;
            if (numberColorToField == numberColor)
            {
                return _gameConfiguration.Green;
            }
            else if (numberColorToField < numberColor)
            {
                return _gameConfiguration.Normal;
            }
            return _gameConfiguration.Warning;
        }
    }
}