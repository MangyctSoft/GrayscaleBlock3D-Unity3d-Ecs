using UnityEngine;
using Leopotam.Ecs;
using GrayscaleBlock3D.AppSettings;
using GrayscaleBlock3D.Components.Player;
using GrayscaleBlock3D.Systems.Models.Data;
using GrayscaleBlock3D.Components.Events.FieldEevents;
using GrayscaleBlock3D.Components.Timers;
using System.Collections.Generic;

namespace GrayscaleBlock3D.Systems.Controller
{
    internal sealed class RemoveManyIdenticalBlocksSystem : IEcsRunSystem
    {
        private readonly GameContext _gameContext = null;
        private readonly EcsFilter<ManagerBlockComponent, RemoveLineEventX>.Exclude<IsRemoveLineMadeEvent, TimerRemoveLineComponent> _filter = null;
        void IEcsRunSystem.Run()
        {
            foreach (var i in _filter)
            {
                ref var block = ref _filter.Get1(i);
                ref var positions = ref _filter.Get2(i).Positions;

                var queue = block.ScanQueuePositions;

                ushort line = (ushort)block.Position.y;
                ref var nextStep = ref _filter.GetEntity(i);

                RemoveManyIdenticalBlocks(ref _gameContext.GameField, ref queue, ref _gameContext.RedLine, positions);
                nextStep.Get<IsBoomBlockEvent>().Position = positions;
                nextStep.Get<ManagerBlockComponent>().NeedScanField = true;
                nextStep.Get<ManagerBlockComponent>().ScanQueuePositions = queue;
                nextStep.Get<MergeStartEventX>();

                nextStep.Del<RemoveLineEventX>();
            }
        }
        private void RemoveManyIdenticalBlocks(ref Blockube[,] gameField, ref Queue<Vector2Int> queue, ref int[] redLine, in IEnumerable<Vector2Int> positions)
        {
            var moveLine = new int[redLine.Length];

            foreach (var item in positions)
            {
                if (gameField[item.x, item.y] != null)
                {
                    gameField[item.x, item.y].Destroy();
                    gameField[item.x, item.y] = null;
                    if (!redLine[item.x].Equals(0))
                    {
                        redLine[item.x]--;
                        moveLine[item.x]++;
                    }
                }
            }
            var height = 0;
            for (var x = 0; x < moveLine.Length; x++)
            {
                //Debug.Log($"x = {x} || moveLine[x] = {moveLine[x]}");
                if (moveLine[x] > 0)
                {
                    height = 0;
                    for (int y = 1; y < gameField.GetLength(1); y++)
                    {
                        //Debug.Log($"== CURSOR x = {x} || y = {y}");
                        if (gameField[x, y - 1] == null)
                        {
                            height++;
                            //Debug.Log($"=== height = {height} || {x} x {y - 1}");
                        }
                        if (gameField[x, y] != null && gameField[x, y - height] == null)
                        {
                            //Debug.Log($"====== COLOR = {gameField[x, y].NumberColor}");
                            //Debug.Log($"======================== DOWN = {height}");
                            queue.Enqueue(new Vector2Int(x, y - height));
                            gameField[x, y - height] = gameField[x, y];
                            gameField[x, y].MoveTo(new Vector2(0, -height));
                            gameField[x, y] = null;
                            y -= height;
                            height = 0;
                        }
                    }
                }
            }
        }
    }
}