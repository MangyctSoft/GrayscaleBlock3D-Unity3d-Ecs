using UnityEngine;
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
    internal sealed class RemoveLineSystem : IEcsRunSystem
    {
        private readonly GameConfiguration _gameConfiguration;

        private readonly GameContext _gameContext = null;
        private readonly EcsFilter<ManagerBlockComponent, RemoveLineEventX> _filter = null;
        void IEcsRunSystem.Run()
        {
            foreach (var i in _filter)
            {
                ref var block = ref _filter.Get1(i);
                ushort line = (ushort)block.Position.y;
                Debug.Log("RemoveLineSystem " + line);
                ref var nextStep = ref _filter.GetEntity(i);

                RemoveLine(_gameContext.GameField, line, _gameContext.RedLine);

                nextStep.Del<RemoveLineEventX>();
            }


        }
        private void RemoveLine(in Blockube[,] blockubes, in ushort line, in int[] redLine)
        {
            for (int x = 0; x < blockubes.GetLength(0); x++)
            {
                blockubes[x, line].NumberColor = 0;
                blockubes[x, line].SetActive(false);
            }

            for (int x = 0; x < blockubes.GetLength(0); x++)
            {
                if (!redLine[x].Equals(0))
                {
                    redLine[x]--;
                }
                for (int y = line; y < blockubes.GetLength(1) - 1; y++)
                {
                    Debug.Log("---------------------");
                    Debug.Log(blockubes[x, y + 1].NumberColor);
                    Debug.Log(blockubes[x, y + 1].Transform.gameObject.activeSelf);

                    if (!blockubes[x, y + 1].Transform.gameObject.activeSelf)
                    {
                        break;
                    }
                    else
                    {
                        Debug.LogFormat("DOWN {0} x {1}", x, y);

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