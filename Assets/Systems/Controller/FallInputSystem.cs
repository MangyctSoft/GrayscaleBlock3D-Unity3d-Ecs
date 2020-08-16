using Leopotam.Ecs;
using GrayscaleBlock3D.AppSettings;
using GrayscaleBlock3D.Components.Player;
using GrayscaleBlock3D.Components.Events.InputEvents;
using GrayscaleBlock3D.Components.Events;
using GrayscaleBlock3D.Components.Timers;
using UnityEngine;

namespace GrayscaleBlock3D.Systems.Controller
{
    internal sealed class FallInputSystem : IEcsRunSystem
    {
        private readonly GameConfiguration _gameConfiguration;
        private readonly EcsFilter<InputFallStartedEvent> _filterFallStarted = null;
        // private readonly EcsFilter<InputFallCanceledEvent> _filterFallCanceled = null;
        private readonly EcsFilter<IsCanFallComponent> _filterFall = null;


        void IEcsRunSystem.Run()
        {
            foreach (var i in _filterFallStarted)
            {
                var fallStart = _filterFallStarted.Get1(i);
                ProcessShootEvent(true);
            }

            // foreach (var i in _filterFallCanceled)
            // {
            //     Debug.Log("Cancel Move");
            //     ref var fallCancel = ref _filterFallCanceled.Get1(i);
            //     ProcessShootEvent(false);
            // }

        }

        private void ProcessShootEvent(in bool isPressed)
        {
            foreach (var i in _filterFall)
            {
                ref var fall = ref _filterFall.GetEntity(i);

                if (isPressed)
                {
                    MakeFalling(ref fall, Vector2.down, 1f);
                    fall.Get<IsFallMadeEvent>();
                }
                // else
                // {
                //     // fall.Del<TimerFallingComponent>();
                //     Debug.Log("FFFFFFFFFFFFFFFFFF");
                //     MakeFalling(ref fall, Vector2.zero, 0f);
                //     fall.Get<MainBlockComponent>().Blockube.Position = _gameConfiguration.CurrentBlockPosition;

                // }
            }
        }

        private void MakeFalling(ref EcsEntity fall, in Vector2 direction, in float speed)
        {
            fall.Get<MoveComponent>().Direction = direction;
            fall.Get<MoveComponent>().Speed = speed;
        }

    }
}