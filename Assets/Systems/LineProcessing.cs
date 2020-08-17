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


    }



}