using Leopotam.Ecs;
using GrayscaleBlock3D.AppSettings;
using GrayscaleBlock3D.Components.Player;
using GrayscaleBlock3D.Components.Events;
using GrayscaleBlock3D.Components.Events.FieldEevents;
using UnityEngine;

namespace GrayscaleBlock3D.Systems.Controller
{
    internal sealed class BlockToFieldSystem : IEcsRunSystem
    {
        private readonly GameConfiguration _gameConfiguration;

        private readonly GameContext _gameContext = null;
        private readonly EcsFilter<MainBlockComponent, BlockInstallToFieldEvent> _filter = null;
        void IEcsRunSystem.Run()
        {
            foreach (var i in _filter)
            {
                ref var block = ref _filter.Get1(i).Blockube;

                var position = block.Position;
                var blockInField = _gameContext.GameField[(int)position.x, (int)position.y];
                blockInField.SetActive(true);
                blockInField.NumberColor = block.NumberColor;
                var number = _gameConfiguration.BlockColors[blockInField.NumberColor];
                var color = new Color(number, number, number, 1f);
                blockInField.Color = color;

                ref var nextStep = ref _filter.GetEntity(i);
                nextStep.Get<ManagerBlockComponent>().Position = position;
                nextStep.Get<RedLineEvent>();
                nextStep.Get<SetRandomColorEvent>();
                // nextStep.Get<InputNonConstrainMoveEvent>();
                nextStep.Get<MainBlockComponent>().Blockube.Position = _gameConfiguration.CurrentBlockPosition;
                nextStep.Get<MergeStartEvent>();
                //
            }
        }
    }
}