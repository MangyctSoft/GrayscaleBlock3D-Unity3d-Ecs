using Leopotam.Ecs;
using GrayscaleBlock3D.AppSettings;
using GrayscaleBlock3D.Components.Player;
using GrayscaleBlock3D.Systems.Models.Data;
using GrayscaleBlock3D.Components.Events.FieldEevents;
using GrayscaleBlock3D.Components.Events.InputEvents;
using GrayscaleBlock3D.Extensions;
using UnityEngine;

namespace GrayscaleBlock3D.Systems.Controller
{
    internal sealed class MergeSystem : IEcsRunSystem
    {
        private readonly GameConfiguration _gameConfiguration;

        private readonly GameContext _gameContext = null;
        private readonly EcsFilter<ManagerBlockComponent, MergeStartEvent> _filterStart = null;
        private readonly EcsFilter<ManagerBlockComponent, MergeExecuteEvent> _filterExecute = null;

        private Blockube blockUp = null;
        private Blockube blockDown = null;
        void IEcsRunSystem.Run()
        {
            foreach (var i in _filterStart)
            {
                ref var block = ref _filterStart.Get1(i);

                var position = block.Position;
                Debug.Log("MergeSystem!!" + position);

                if ((int)position.y > 0)
                {
                    blockUp = _gameContext.GameField[(int)position.x, (int)position.y];
                    blockDown = _gameContext.GameField[(int)position.x, (int)position.y - _gameContext.ONE_DIFF];

                    if (blockUp.EqualsColor(blockDown) && blockDown.NumberColor > _gameContext.ONE_DIFF)
                    {
                        blockUp.SetActive(false);
                        _gameContext.RedLine[(int)position.x] = (int)position.y;
                        var newNumberColor = (ushort)(blockUp.NumberColor - _gameContext.ONE_DIFF);
                        var color = Additive.SetColor(_gameConfiguration, newNumberColor);
                        blockDown.Color = color;
                        blockDown.NumberColor = newNumberColor;
                        //block.Position = blockDown.Position;
                    }

                }
                ref var nextStep = ref _filterStart.GetEntity(i);
                // nextStep.Get<ManagerBlockComponent>().Position = position;
                // nextStep.Get<RedLineEvent>();
                //nextStep.Get<SetRandomColorEvent>();
                nextStep.Get<InputNonConstrainMoveEvent>();
                //nextStep.Get<MainBlockComponent>().Blockube.Position = _gameConfiguration.CurrentBlockPosition;

            }
        }

    }
}