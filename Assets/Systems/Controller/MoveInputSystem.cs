using System;
using Leopotam.Ecs;
using GrayscaleBlock3D.AppSettings;
using GrayscaleBlock3D.Components.Player;
using GrayscaleBlock3D.Components.Events.InputEvents;
using UnityEngine;

namespace GrayscaleBlock3D.Systems.Controller
{
    internal sealed class MoveInputSystem : IEcsRunSystem
    {
        // auto-injected fields.
        private readonly GameConfiguration _gameConfiguration = null;

        private readonly EcsFilter<InputMoveStartedEvent> _filterMoveStart = null;
        private readonly EcsFilter<InputMoveCanceledEvent> _filterMoveCanceled = null;
        private readonly EcsFilter<MoveComponent> _filterMove = null;

        void IEcsRunSystem.Run()
        {
            foreach (var i in _filterMoveStart)
            {
                ref var inputMoveStartedEvent = ref _filterMoveStart.Get1(i);
                ProcessMove(true, inputMoveStartedEvent.Axis);
            }

            foreach (var i in _filterMoveCanceled)
            {
                ref var inputMoveCanceledEvent = ref _filterMoveCanceled.Get1(i);
                ProcessMove(false);
            }
        }

        private void ProcessMove(in bool doMove, in Vector2 direction = new Vector2())
        {
            foreach (var i in _filterMove)
            {
                var speed = _gameConfiguration.SpeedMove;
                ref var move = ref _filterMove.Get1(i);

                if (doMove)
                {
                    MoveBlock(ref move, direction, speed);
                }
                else
                {
                    StopBlock(ref move);
                }
            }
        }

        private void MoveBlock(ref MoveComponent component, in Vector2 direction, in float speed)
        {
            component.Speed = speed;
            component.Direction = direction;
        }

        private void StopBlock(ref MoveComponent component)
        {
            component.Speed = 0;
        }

    }
}