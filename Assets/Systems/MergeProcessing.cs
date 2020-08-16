using UnityEngine;
using Leopotam.Ecs;
using GrayscaleBlock3D.Components;
using GrayscaleBlock3D.Systems.Models.Data;

namespace GrayscaleBlock3D
{
    public class MergeProcessing : IEcsRunSystem
    {
        EcsFilter<FieldComponent, MovableComponent, GameComponent> _mergeFilter = null;
        public void Run()
        {
            foreach (var i in _mergeFilter)
            {

                ref var game = ref _mergeFilter.Get3(i);


                if (Time.time < game.delayMerge)
                {
                    return;
                }

                ref var field = ref _mergeFilter.Get1(i);
                ref var move = ref _mergeFilter.Get2(i);

                if (game.isMerge)
                {
                    game.isMerge = false;

                    // int currentBlockX = (int)move.currentBlock.transform.position.x;
                    // int currentBlockY = (int)move.currentBlock.transform.position.y;

                    // if (currentBlockY > 0)
                    // {
                    //     var currentColor = move.currentBlock.color;
                    //     var downColor = field.blocks[currentBlockX, currentBlockY - 1].color;
                    //     Debug.LogFormat("currentColor = {0}, downColor = {1}", currentColor, downColor);
                    //     if (currentColor == 1)
                    //     {
                    //         Debug.Log("Black!");
                    //         game.isRespawn = true;
                    //         return;
                    //     }
                    //     if (currentColor.Equals(downColor))
                    //     {
                    //         if (downColor > 0)
                    //         {
                    //             var newNumColor = --downColor;
                    //             var newColor = new Color(Game.globalVar.colors[newNumColor],
                    //                                         Game.globalVar.colors[newNumColor],
                    //                                         Game.globalVar.colors[newNumColor], 1f);
                    //             field.blocks[currentBlockX, currentBlockY - 1].renderer.material.SetColor("_Color", newColor);
                    //             field.blocks[currentBlockX, currentBlockY - 1].color = newNumColor;
                    //             field.blocks[currentBlockX, currentBlockY].color = 0;

                    //             move.currentBlock.transform.position = new Vector3(40, 40, 40);
                    //             move.currentBlock = field.blocks[currentBlockX, currentBlockY - 1];

                    //             if (field.blocks[currentBlockX, currentBlockY + 1].color > 0)
                    //             {
                    //                 Debug.Log("Column full!");

                    //                 Debug.LogFormat("currentBlockX = {0}, currentBlockY = {1}", currentBlockX, currentBlockY + 1);

                    //                 ColumnDown(ref field.blocks, currentBlockX, currentBlockY, field.heightLines[currentBlockX]);
                    //                 field.heightLines[currentBlockX]--;
                    //                 game.delayMergeGlobal = Time.time + 1.6f;
                    //                 game.isMergeGlobal = true;
                    //                 return;
                    //             }
                    //             field.heightLines[currentBlockX] = currentBlockY;
                    //             game.isLine = true;
                    //             return;
                    //         }
                    //     }
                    // }

                    if (game.isNextMerge)
                    {
                        game.delayMergeGlobal = Time.time + 1.6f;
                        game.isMergeGlobal = true;
                    }
                    else
                    {
                        game.isRespawn = true;
                    }
                }
            }
        }

        private void ColumnDown(ref Blockube[,] blockubes, int x, int y, int height)
        {
            for (int i = y; i < height; i++)
            {
                Debug.LogFormat("x = {0}, i = {1}", x, i);

                // blockubes[x, i] = blockubes[x, i + 1];
                // blockubes[x, i + 1].color = 0;
                // blockubes[x, i].transform.position += Vector3.down;

            }
        }
    }

}