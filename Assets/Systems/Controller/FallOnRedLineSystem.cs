using Leopotam.Ecs;
using GrayscaleBlock3D.AppSettings;
using GrayscaleBlock3D.Components.Player;
using GrayscaleBlock3D.Components.Events.FieldEevents;
using GrayscaleBlock3D.Components.Events;
using GrayscaleBlock3D.Components.Timers;
using GrayscaleBlock3D.Extensions;
using UnityEngine;

namespace GrayscaleBlock3D.Systems.Controller
{
    internal sealed class FallOnRedLineSystem : IEcsRunSystem
    {
        // auto-injected fields.
        private readonly GameContext _gameContext = null;
        private readonly EcsFilter<MoveComponent, MainBlockComponent, TimerFallingComponent> _filter = null;
        void IEcsRunSystem.Run()
        {
            foreach (var i in _filter)
            {
                ref var moveComponent = ref _filter.Get1(i);
                ref var blockComponent = ref _filter.Get2(i);
                var position = blockComponent.CurrentBlock.Position;
                var hieght = _gameContext.RedLine[(int)position.x < 0 ? 0 : (int)position.x >= _gameContext.RedLine.Length ? _gameContext.RedLine.Length - 1 : (int)position.x];
                Debug.Log("Position = " + position);
                Debug.Log("hieght = " + hieght);

                if (hieght < _gameContext.GameField.GetLength(1))
                {
                    if (position.y <= hieght)
                    {
                        moveComponent.Speed = 0;
                        ref var nextStep = ref _filter.GetEntity(i);
                        nextStep.Del<TimerFallingComponent>();
                        nextStep.Get<BlockInstallToFieldEvent>();
                    }
                }
            }
        }
    }
}