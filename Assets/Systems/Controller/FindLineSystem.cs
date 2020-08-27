using Leopotam.Ecs;
using GrayscaleBlock3D.AppSettings;
using GrayscaleBlock3D.Components.Player;
using GrayscaleBlock3D.Systems.Models.Data;
using GrayscaleBlock3D.Components.Events.FieldEevents;

namespace GrayscaleBlock3D.Systems.Controller
{
    internal sealed class FindLineSystem : IEcsRunSystem
    {
        private readonly GameContext _gameContext = null;
        private readonly EcsFilter<ManagerBlockComponent, FindLineStartEventX>.Exclude<BlockInstallColorEventX> _filterStart = null;

        void IEcsRunSystem.Run()
        {
            foreach (var i in _filterStart)
            {
                ref var block = ref _filterStart.Get1(i);
                ushort line = (ushort)block.Position.y;
                ref var nextStep = ref _filterStart.GetEntity(i);

                if (CheckLine(_gameContext.GameField, line))
                {
                    nextStep.Get<IsRemoveLineMadeEvent>();
                    nextStep.Get<RemoveLineEventX>();

                    nextStep.Del<FindLineStartEventX>();
                    return;
                }

                nextStep.Get<MergeStartEventX>();

                nextStep.Del<FindLineStartEventX>();
            }
        }

        private bool CheckLine(in Blockube[,] blockubes, in ushort y)
        {
            ushort currentColor = 0;
            ushort previosColor = 0;
            for (ushort x = 0; x < blockubes.GetLength(0); x++)
            {
                if (blockubes[x, y] == null)
                {
                    return false;
                }
                currentColor = blockubes[x, y].NumberColor;
                if (currentColor.Equals(0))
                {
                    return false;
                }

                if (x.Equals(0))
                {
                    previosColor = currentColor;
                    continue;
                }

                if (!currentColor.Equals(previosColor))
                {
                    return false;
                }
            }
            return true;
        }
    }
}