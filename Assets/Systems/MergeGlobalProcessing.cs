using System.Threading;
using UnityEngine;
using Leopotam.Ecs;
using GrayscaleBlock3D.Components;

namespace GrayscaleBlock3D
{
    public class MergeGlobalProcessing : IEcsRunSystem
    {
        EcsFilter<FieldComponent, MovableComponent, GameComponent> _lineFilter = null;
        public void Run()
        {
            foreach (var i in _lineFilter)
            {
                ref var game = ref _lineFilter.Get3(i);

                if (Time.time < game.delayMergeGlobal)
                {
                    return;
                }


                //game.delay = Time.time + Game.globalVar.delayMerge;

                if (game.isMergeGlobal)
                {
                    game.isMergeGlobal = false;

                    ref var field = ref _lineFilter.Get1(i);
                    ref var move = ref _lineFilter.Get2(i);
                    game = ref _lineFilter.Get3(i);
                    Debug.Log("GLOBAL MERGE!");

                    // for (int x = 0; x < field.blocks.GetLength(0); x++)
                    // {
                    //     for (int y = field.blocks.GetLength(1) - 1; y >= 1; y--)
                    //     {
                    //         //
                    //         if (!field.blocks[x, y].color.Equals(0))
                    //         {
                    //             Debug.LogFormat("x = {0}, y = {1}, color ={2}", x, y, field.blocks[x, y].color);
                    //             if (field.blocks[x, y].color.Equals(field.blocks[x, y - 1].color))
                    //             {
                    //                 Debug.LogFormat("GLOBAL Finder!");

                    //                 move.currentBlock = field.blocks[x, y];
                    //                 game.delayMerge = Time.time + Game.globalVar.delayMerge;
                    //                 game.isLine = true;
                    //                 game.isNextMerge = true;
                    //                 return;
                    //             }
                    //         }

                    //     }
                    // }
                    game.isRespawn = true;
                }

            }
        }
    }

}
