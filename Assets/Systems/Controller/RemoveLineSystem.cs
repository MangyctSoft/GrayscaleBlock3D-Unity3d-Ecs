using UnityEngine;
using Leopotam.Ecs;
using GrayscaleBlock3D.AppSettings;
using GrayscaleBlock3D.Components.Player;
using GrayscaleBlock3D.Systems.Models.Data;
using GrayscaleBlock3D.Components.Events.FieldEevents;
using GrayscaleBlock3D.Components.Timers;

namespace GrayscaleBlock3D.Systems.Controller
{
    internal sealed class RemoveLineSystem : IEcsRunSystem
    {
        private readonly GameConfiguration _gameConfiguration;
        private readonly GameContext _gameContext = null;
        private readonly SceneData _sceneData = null;

        private readonly EcsFilter<ManagerBlockComponent, RemoveLineEventX>.Exclude<IsRemoveLineMadeEvent, TimerRemoveLineComponent> _filter = null;
        void IEcsRunSystem.Run()
        {
            foreach (var i in _filter)
            {
                ref var block = ref _filter.Get1(i);
                ushort line = (ushort)block.Position.y;
                ref var nextStep = ref _filter.GetEntity(i);

                RemoveLine(_gameContext.GameField, line, _gameContext.RedLine);
                nextStep.Get<ManagerBlockComponent>().NeedScanField = true;
                nextStep.Get<ManagerBlockComponent>().ScanPosition = new Vector2(0, line);
                nextStep.Get<MergeStartEventX>();

                nextStep.Del<RemoveLineEventX>();
            }
        }
        private void RemoveLine(in Blockube[,] blockubes, in ushort line, in int[] redLine)
        {
            for (int x = 0; x < blockubes.GetLength(0); x++)
            {
                blockubes[x, line].NumberColor = 0;
                blockubes[x, line].SetActive(false, _sceneData.ExplosionPrefab);
            }

            for (int x = 0; x < blockubes.GetLength(0); x++)
            {
                if (!redLine[x].Equals(0))
                {
                    redLine[x]--;
                }
                for (int y = line; y < blockubes.GetLength(1) - 1; y++)
                {
                    if (!blockubes[x, y + 1].Transform.gameObject.activeSelf)
                    {
                        break;
                    }
                    else
                    {
                        var color = Additive.SetColor(_gameConfiguration, blockubes[x, y + 1].NumberColor);
                        blockubes[x, y].Color = color;
                        blockubes[x, y].NumberColor = blockubes[x, y + 1].NumberColor;
                        blockubes[x, y].SetActive(true);

                        blockubes[x, y + 1].NumberColor = 0;
                        blockubes[x, y + 1].SetActive(false);
                    }
                }
            }
        }
    }
}