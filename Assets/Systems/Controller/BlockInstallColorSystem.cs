using UnityEngine;
using Leopotam.Ecs;
using GrayscaleBlock3D.AppSettings;
using GrayscaleBlock3D.Components.Player;
using GrayscaleBlock3D.Components.Events.FieldEevents;
using GrayscaleBlock3D.Systems.Models.Data;

namespace GrayscaleBlock3D.Systems.Controller
{
    internal sealed class BlockInstallColorSystem : IEcsRunSystem
    {
        private readonly GameConfiguration _gameConfiguration;
        private readonly GameContext _gameContext = null;
        private readonly EcsFilter<ManagerBlockComponent, BlockInstallColorEventX> _filter = null;
        void IEcsRunSystem.Run()
        {
            foreach (var i in _filter)
            {
                ref var block = ref _filter.Get1(i);

                var position = block.Position;
                var numberColor = block.NumberColor;
                var active = block.Active;

                var blockInField = _gameContext.GameField[(int)position.x, (int)position.y];
                blockInField.NumberColor = numberColor;
                ref var nextStep = ref _filter.GetEntity(i);

                if (active)
                {
                    blockInField.SetActive(active);
                    _gameContext.RedLine[(int)position.x]++;
                }
                else
                {
                    var blockUp = _gameContext.GameField[(int)position.x, (int)position.y + _gameContext.ONE_DIFF];
                    blockUp.SetActive(active);
                    blockUp.NumberColor = 0;
                    if (MoveDownBlocks(_gameContext.GameField, blockUp.Position))
                    {
                        nextStep.Get<ManagerBlockComponent>().Position = blockUp.Position;
                    }
                    _gameContext.RedLine[(int)position.x]--;
                }
                var color = Additive.SetColor(_gameConfiguration, numberColor);
                blockInField.Color = color;

                nextStep.Del<BlockInstallColorEventX>();
            }
        }

        private bool MoveDownBlocks(in Blockube[,] blockubes, in Vector2 position)
        {
            var result = false;
            var x = (ushort)position.x;
            var line = (ushort)position.y;

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
                    result = true;
                }
            }
            return result;
        }
    }
}