using System.Threading;
using UnityEngine;
using Leopotam.Ecs;
using GrayscaleBlock3D.Components;
using GrayscaleBlock3D.Systems.Models.Data;

namespace GrayscaleBlock3D
{
    public class LineProcessing : IEcsRunSystem
    {
        EcsFilter<FieldComponent, MovableComponent, GameComponent> _lineFilter = null;
        public void Run()
        {
            foreach (var i in _lineFilter)
            {
                ref var game = ref _lineFilter.Get3(i);

                if (Time.time < game.delayMerge)
                {
                    return;
                }
                //game.delay = Time.time + Game.globalVar.delayMerge;

                if (game.isLine)
                {
                    game.isLine = false;

                    ref var field = ref _lineFilter.Get1(i);
                    ref var move = ref _lineFilter.Get2(i);

                    // int currentBlockX = (int)move.currentBlock.transform.position.x;
                    // int currentBlockY = (int)move.currentBlock.transform.position.y;
                    // //Debug.LogFormat("currentBlockX = {0}, currentBlockY = {1}", currentBlockX, currentBlockY);

                    // if (CheckLine(field.blocks, currentBlockY))
                    // {
                    //     //Thread.Sleep(3000);
                    //     Debug.Log("Equale!");
                    //     RemoveLine(ref field.blocks, currentBlockY, field.heightLines);

                    //     game.delayMergeGlobal = Time.time + 1.6f;

                    //     game.isMergeGlobal = true;

                    // }
                    // else
                    // {
                    //     game.delayMerge = Time.time + Game.globalVar.delayMerge;
                    //     game.isMerge = true;
                    // }
                }
            }
        }
        private bool CheckLine(Blockube[,] blockubes, int y)
        {
            int currentColor = 0;
            int previosColor = 0;
            for (int x = 0; x < blockubes.GetLength(0); x++)
            {
                // currentColor = blockubes[x, y].color;
                //Debug.LogFormat("i = {0}, currentColor = {1}", i, currentColor);
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
        private void RemoveLine(ref Blockube[,] blockubes, int line, int[] hightLines)
        {
            for (int x = 0; x < blockubes.GetLength(0); x++)
            {
                // blockubes[x, line].transform.position = new Vector3(40, 40, 40);
                // blockubes[x, line].color = 0;
            }

            for (int x = 0; x < blockubes.GetLength(0); x++)
            {
                if (!hightLines[x].Equals(0))
                {
                    hightLines[x]--;
                }
                for (int y = line; y < blockubes.GetLength(1) - 1; y++)
                {
                    // if (blockubes[x, y + 1].color.Equals(0))
                    // {
                    //     break;
                    // }
                    // else
                    // {
                    //     // blockubes[x, y] = blockubes[x, y + 1];
                    //     // blockubes[x, y + 1].color = 0;
                    //     // blockubes[x, y].transform.position += Vector3.down;

                    // }

                }
            }
        }
    }



}