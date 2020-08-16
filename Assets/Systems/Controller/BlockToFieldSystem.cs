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
        private readonly EcsFilter<MainBlockComponent, BlockInstallToFieldEvent> _filter = null;
        void IEcsRunSystem.Run()
        {
            foreach (var i in _filter)
            {
                ref var currentBlock = ref _filter.Get1(i).Blockube;

                var position = currentBlock.Position;
                var numberColor = currentBlock.NumberColor;

                ref var nextStep = ref _filter.GetEntity(i);
                nextStep.Get<ManagerBlockComponent>().Position = position;
                nextStep.Get<ManagerBlockComponent>().NumberColor = numberColor;
                nextStep.Get<ManagerBlockComponent>().Active = true;
                nextStep.Get<BlockInstallColorEvent>();
                nextStep.Get<SetRandomColorEvent>();
                nextStep.Get<MainBlockComponent>().Blockube.Position = _gameConfiguration.CurrentBlockPosition;
                nextStep.Get<MergeStartEvent>();
            }
        }
    }
}