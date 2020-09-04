using System.Collections.Generic;
using UnityEngine;
using Leopotam.Ecs;
using GrayscaleBlock3D.AppSettings;
using GrayscaleBlock3D.Components.Player;
using GrayscaleBlock3D.Components.Events.FieldEevents;
using GrayscaleBlock3D.Systems.Models.Data;
using GrayscaleBlock3D.Extensions;
using GrayscaleBlock3D.Pooling;

namespace GrayscaleBlock3D.Systems.Controller
{
    internal sealed class BlockInstallColorSystem : IEcsRunSystem
    {
        private readonly GameConfiguration _gameConfiguration;
        private readonly GameContext _gameContext = null;
        private readonly PoolsObject _poolsObject = null;

        private readonly EcsFilter<ManagerBlockComponent, BlockInstallColorEventX> _filter = null;
        void IEcsRunSystem.Run()
        {
            foreach (var i in _filter)
            {
                ref var block = ref _filter.Get1(i);

                var position = block.Position.GetIntVector2();
                var numberColor = block.NumberColor;
                var active = block.Active;

                var blockInField = _gameContext.GameField[position.x, position.y];
                ref var nextStep = ref _filter.GetEntity(i);

                if (active)
                {
                    var poolsObject = _poolsObject.Blocks.Get();
                    var transform = poolsObject.PoolTransform;
                    transform.position = new Vector3(position.x, position.y);
                    transform.gameObject.SetActive(true);
                    blockInField = new Blockube(poolsObject.PoolTransform.gameObject, new Color(), 0, poolsObject);
                    _gameContext.GameField[position.x, position.y] = blockInField;
                    _gameContext.RedLine[position.x]++;
                }
                else
                {
                    var blockUp = _gameContext.GameField[position.x, position.y + _gameContext.ONE_DIFF];
                    if (blockUp != null)
                    {
                        var positionScan = blockUp.Position.GetIntVector2();
                        blockUp.Destroy();

                        nextStep.Get<IsBoomBlockEvent>().Position = new List<Vector2Int> { positionScan };

                        _gameContext.GameField[position.x, position.y + _gameContext.ONE_DIFF] = null;
                        if (MoveDownBlocks(ref _gameContext.GameField, blockUp.Position))
                        {
                            block.Position = positionScan;
                        }
                    }
                    _gameContext.RedLine[position.x]--;
                }

                blockInField.NumberColor = numberColor;
                var color = Additive.SetColor(_gameConfiguration, numberColor);
                blockInField.Color = color;

                nextStep.Del<BlockInstallColorEventX>();
            }
        }

        private bool MoveDownBlocks(ref Blockube[,] blockubes, in Vector2 position)
        {
            var result = false;
            var x = (ushort)position.x;
            var line = (ushort)position.y;

            for (int y = line; y < blockubes.GetLength(1) - 1; y++)
            {
                if (blockubes[x, y + 1] != null)
                {
                    blockubes[x, y] = blockubes[x, y + 1];
                    blockubes[x, y + 1].MoveTo(new Vector2(0, -1));
                    blockubes[x, y + 1] = null;
                    result = true;
                }
            }
            return result;
        }
    }
}