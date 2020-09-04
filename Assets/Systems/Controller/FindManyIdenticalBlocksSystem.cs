using System.Collections.Generic;
using UnityEngine;
using Leopotam.Ecs;
using GrayscaleBlock3D.AppSettings;
using GrayscaleBlock3D.Components.Player;
using GrayscaleBlock3D.Components.Events.FieldEevents;
using GrayscaleBlock3D.Extensions;

namespace GrayscaleBlock3D.Systems.Controller
{
    internal sealed class FindManyIdenticalBlocksSystem : IEcsRunSystem
    {
        private readonly GameContext _gameContext = null;
        private readonly EcsFilter<ManagerBlockComponent, FindLineStartEventX>.Exclude<BlockInstallColorEventX> _filterStart = null;

        void IEcsRunSystem.Run()
        {
            foreach (var i in _filterStart)
            {
                ref var block = ref _filterStart.Get1(i);
                var position = block.Position.GetIntVector2();
                ref var nextStep = ref _filterStart.GetEntity(i);

                if (FindFiveIdenticalBlocks(position, out List<Vector2Int> listPositions))
                {
                    nextStep.Get<IsRemoveLineMadeEvent>();
                    nextStep.Get<RemoveLineEventX>().Positions = listPositions;

                    nextStep.Del<FindLineStartEventX>();
                    return;
                }

                nextStep.Get<MergeStartEventX>();

                nextStep.Del<FindLineStartEventX>();
            }
        }

        private bool FindFiveIdenticalBlocks(Vector2Int position, out List<Vector2Int> listPositions)
        {
            listPositions = new List<Vector2Int> { position };
            //Debug.Log($"==================================== ADD START nextPosition = {position}");
            ushort currentColor = 0;
            ushort previosColor = 0;
            ushort counter = 1;
            var gameField = _gameContext.GameField;
            var identicalBlocks = new int[_gameContext.GameField.GetLength(0), _gameContext.GameField.GetLength(1)];
            //Debug.Log($"===========================START================================");
            bool check = true;
            Vector2Int nextPosition = position;
            while (check)
            {
                //Debug.Log($"START counter = {counter}");
                check = false;
                var tempPosition = new List<Vector2Int>();

                foreach (var listItem in listPositions)
                {
                    if (identicalBlocks[listItem.x, listItem.y].Equals(0))
                    {
                        identicalBlocks[listItem.x, listItem.y] = 1;

                        foreach (var item in _gameContext.FindPosition)
                        {
                            var x = listItem.x + item.x;
                            var y = listItem.y + item.y;
                            //Debug.Log($"CURSOR x = {x} || y = {y}");
                            if (x >= 0 && x < gameField.GetLength(0) && y >= 0)
                            {
                                //Debug.Log($"NORMAL x1 = {x} || y1 = {y}");
                                if (gameField[x, y] == null || gameField[listItem.x, listItem.y] == null)
                                {
                                    //Debug.Log($"EMPTY x1 = {x} || y1 = {y}");
                                    continue;
                                }
                                currentColor = gameField[listItem.x, listItem.y].NumberColor;
                                previosColor = gameField[x, y].NumberColor;
                                //Debug.Log($"currentColor = {currentColor} || previosColor = {previosColor}");
                                if (currentColor == previosColor)
                                {
                                    //Debug.Log($"IdenticalBlocks");
                                    //Debug.Log($"identicalBlocks[x, y] = {identicalBlocks[x, y]}");
                                    if (identicalBlocks[x, y].Equals(0))
                                    {
                                        nextPosition = new Vector2Int(x, y);
                                        //Debug.Log($"==================================== ADD nextPosition = {nextPosition}");
                                        if (!tempPosition.Contains(nextPosition))
                                        {
                                            tempPosition.Add(nextPosition);
                                            counter++;
                                        }
                                        check = true;
                                    }
                                }
                            }
                        }
                    }
                }
                if (tempPosition != null)
                {
                    listPositions.AddRange(tempPosition);
                }
            }
            //Debug.Log($"============ END counter = {counter} ================");
            if (counter >= 5)
                return true;
            else
                return false;
        }
    }
}