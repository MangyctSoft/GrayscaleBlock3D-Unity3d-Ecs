using UnityEngine;
using Leopotam.Ecs;
using GrayscaleBlock3D.AppSettings;
using GrayscaleBlock3D.Extensions;
using GrayscaleBlock3D.Components.Events;
using GrayscaleBlock3D.Components.Events.InputEvents;
using GrayscaleBlock3D.Components.Player;

namespace GrayscaleBlock3D.Systems.Controller
{
    public sealed class InputSystem : IEcsRunSystem
    {
        private readonly EcsWorld _world = null;
        private readonly GameContext _gameContext = null;
        private readonly EcsFilter<InputNonConstrainMoveEvent> _filter = null;
        private readonly EcsFilter<MainBlockComponent> _filterMainBlock = null;
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

                    if (direction != 0)
                    {
                        SendMessageInGame(new InputMoveStartedEvent() { Axis = direction >= 0 ? Vector2.right : Vector2.left });
                        cancel = true;
                        return;
                    }

                    var fall = Input.GetAxis("Vertical");
                    if (fall != 0)
                    {
                        foreach (var m in _filterMainBlock)
                        {
                            ref var mainBlock = ref _filterMainBlock.Get1(m);

                            var position = mainBlock.CurrentBlock.Position.GetIntVector2();

                            if (position.y > _gameContext.RedLine[position.x])
                            {
                                SendMessageInGame(new InputFallStartedEvent());
                                _filter.GetEntity(i).Del<InputNonConstrainMoveEvent>();
                            }
                            else
                            {
                                Debug.Log("GAme OVER");
                                SendMessageInGame(new GameOverEvent());
                            }
                        }
                        return;
                    }

                    if (Input.GetKeyDown(KeyCode.Space))
                    {
                        if (IsColorChangeBlocks())
                        {
                            SendMessageInGame(new ColorChangeStartEvent());
                            _filter.GetEntity(i).Del<InputNonConstrainMoveEvent>();
                            return;
                        }
                    }
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
        private bool IsColorChangeBlocks()
        {
            foreach (var m in _filterMainBlock)
            {
                ref var mainBlock = ref _filterMainBlock.Get1(m);
                var position = mainBlock.CurrentBlock.Position.GetIntVector2();
                if (position.x.Equals(_gameContext.GameField.GetLength(0) / 2))
                {
                    return true;
                }
            }
            return false;
        }
        private void SendMessageInGame<T>(in T messageEvent) where T : struct
        {
            if (_gameContext.GameState != GameStates.Play) return;
            _world.SendMessage(messageEvent);
        }
    }
}