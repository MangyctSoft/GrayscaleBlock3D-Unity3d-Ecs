using Leopotam.Ecs;
using GrayscaleBlock3D.AppSettings;
using GrayscaleBlock3D.Components.Player;
using GrayscaleBlock3D.Components.Events;
using GrayscaleBlock3D.Components.Events.FieldEevents;
using GrayscaleBlock3D.Extensions;
using UnityEngine;
using System.Collections.Generic;

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
                ref var currentBlock = ref _filter.Get1(i).CurrentBlock;

                var position = currentBlock.Position;
                var numberColor = currentBlock.NumberColor;

                ref var nextStep = ref _filter.GetEntity(i);
                nextStep.Get<ManagerBlockComponent>().Position = position;
                nextStep.Get<ManagerBlockComponent>().NumberColor = numberColor;
                nextStep.Get<ManagerBlockComponent>().Active = true;
                nextStep.Get<ManagerBlockComponent>().ScanQueuePositions = new Queue<Vector2Int>(); ;
                nextStep.Get<BlockInstallColorEventX>();
                nextStep.Get<SetNextColorEvent>();
                nextStep.Get<FindLineStartEventX>();

                currentBlock.Position = _gameConfiguration.CurrentBlockPosition.GetIntVector2();
            }
        }
    }
}