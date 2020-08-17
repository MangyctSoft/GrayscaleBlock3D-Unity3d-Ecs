using Leopotam.Ecs;
using GrayscaleBlock3D.AppSettings;
using GrayscaleBlock3D.Components.Player;
using GrayscaleBlock3D.Systems.Models.Data;
using GrayscaleBlock3D.Components.Events.FieldEevents;
using GrayscaleBlock3D.Components.Events.InputEvents;
using GrayscaleBlock3D.Extensions;
using GrayscaleBlock3D.Components.Timers;

namespace GrayscaleBlock3D.Systems.Controller
{
    internal sealed class MergeSystem : IEcsRunSystem
    {
        private readonly GameConfiguration _gameConfiguration;

        private readonly GameContext _gameContext = null;
        private readonly EcsFilter<ManagerBlockComponent, MergeStartEventX> _filterStart = null;
        private readonly EcsFilter<ManagerBlockComponent, MergeExecuteEvent>.Exclude<TimerMergeComponent> _filterExecute = null;

        private Blockube blockUp = null;
        private Blockube blockDown = null;
        void IEcsRunSystem.Run()
        {
            foreach (var i in _filterStart)
            {
                ref var block = ref _filterStart.Get1(i);

                var position = block.Position;

                ref var nextStep = ref _filterStart.GetEntity(i);

                if ((int)position.y > 0)
                {
                    blockUp = _gameContext.GameField[(int)position.x, (int)position.y];
                    blockDown = _gameContext.GameField[(int)position.x, (int)position.y - _gameContext.ONE_DIFF];

                    if (blockUp.EqualsColor(blockDown) && blockDown.NumberColor > _gameContext.ONE_DIFF)
                    {
                        nextStep.Get<IsMergeMadeEvent>();
                        nextStep.Get<MergeExecuteEvent>();

                        nextStep.Del<MergeStartEventX>();
                        return;
                    }

                }
                nextStep.Get<InputNonConstrainMoveEvent>();

                nextStep.Del<MergeStartEventX>();
            }

            foreach (var i in _filterExecute)
            {
                ref var block = ref _filterExecute.Get1(i);
                var position = block.Position;
                ref var nextStep = ref _filterStart.GetEntity(i);

                nextStep.Get<ManagerBlockComponent>().Position = blockDown.Position;
                nextStep.Get<ManagerBlockComponent>().NumberColor = --blockUp.NumberColor;
                nextStep.Get<ManagerBlockComponent>().Active = false;
                nextStep.Get<BlockInstallColorEventX>();
                nextStep.Get<FindLineStartEventX>();

                nextStep.Del<MergeExecuteEvent>();
            }
        }
    }
}