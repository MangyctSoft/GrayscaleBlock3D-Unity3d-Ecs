using System;
using UnityEngine;
using Leopotam.Ecs;
using GrayscaleBlock3D.AppSettings;
using GrayscaleBlock3D.Extensions;
using GrayscaleBlock3D.Components.Events.InputEvents;
using GrayscaleBlock3D.Components.Events;

namespace GrayscaleBlock3D.Systems.Controller
{
    public sealed class InputSystem : IEcsRunSystem
    {
        private readonly EcsWorld _world = null;
        private readonly GameContext _gameContext = null;

        private readonly EcsFilter<InputNonConstrainMoveEvent> _filter = null;
        //BlockDirection direction = BlockDirection.Awaite;
        bool cancel = false;
        public void Run()
        {
            #region old
            // foreach (var i in _filter)
            // {
            //     ref var blockMove = ref _filter.Get1(i);
            //     if (blockMove.isMove)
            //     {
            //         if (Input.GetKeyDown(KeyCode.Space) && !blockMove.isFalling)
            //         {
            //             direction = BlockDirection.Down;
            //             blockMove.isFalling = true;
            //         }

            //         if (!blockMove.isFalling && !blockMove.isInput)
            //         {
            //             blockMove.isInput = true;

            //             var x = Input.GetAxis("Horizontal");

            //             //direction = BlockDirection.Awaite;

            //             direction = x == 0 ? BlockDirection.Awaite : x > 0 ? BlockDirection.Right : BlockDirection.Left;

            //         }
            //         blockMove.direction = direction;
            //     }
            // }
            #endregion
            foreach (var i in _filter)
            {

                ref var move = ref _filter.Get1(i);


                if (Input.anyKeyDown)
                {

                    var direction = Input.GetAxis("Horizontal");
                    var fall = Input.GetAxis("Vertical");

                    if (direction != 0)
                    {
                        SendMessageInGame(new InputMoveStartedEvent() { Axis = direction >= 0 ? Vector2.right : Vector2.left });

                    }
                    if (fall != 0)
                    {
                        //SendMessageInGame(new InputMoveStartedEvent() { Axis = Vector2.down });
                        SendMessageInGame(new InputFallStartedEvent());

                        _filter.GetEntity(i).Del<InputNonConstrainMoveEvent>();
                    }
                    cancel = true;

                }
                else
                {
                    if (cancel)
                    {
                        SendMessageInGame(new InputMoveCanceledEvent());
                        cancel = false;
                    }

                }

            }

        }

        private void SendMessageInGame<T>(in T messageEvent)
            where T : struct
        {
            if (_gameContext.GameState != GameStates.Play) return;
            _world.SendMessage(messageEvent);
        }
    }
}