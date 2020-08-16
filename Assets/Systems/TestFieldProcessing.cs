using UnityEngine;
using Leopotam.Ecs;
using GrayscaleBlock3D.Components;

namespace GrayscaleBlock3D
{
    public class TestFieldProcessing : IEcsRunSystem
    {
        EcsFilter<FieldComponent> _testFilter = null;
        public void Run()
        {
            // foreach (var i in _testFilter)
            // {
            //     ref var field = ref _testFilter.Get1(i);


            //     for (int x = 0; x < field.testBlocks.GetLength(0); x++)
            //         for (int y = 0; y < field.testBlocks.GetLength(1); y++)
            //         {
            //             var newNumColor = field.blocks[x, y].color;
            //             var newColor = new Color(Game.globalVar.colors[newNumColor],
            //                                         Game.globalVar.colors[newNumColor],
            //                                         Game.globalVar.colors[newNumColor], 1f);
            //             field.testBlocks[x, y].renderer.material.SetColor("_Color", newColor);
            //             field.testBlocks[x, y].color = newNumColor;
            //         }


            // }
        }
    }
}