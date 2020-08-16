using UnityEngine;
using Leopotam.Ecs;

namespace GrayscaleBlock3D
{
    public class FieldProcessing : IEcsRunSystem
    {
        EcsFilter<FieldComponent, MovableComponent, MoveBlockComponent, GameComponent> _fieldMoveFilter = null;
        public void Run()
        {

            // foreach (var i in _fieldMoveFilter)            
            // {
            //     ref var field = ref _fieldMoveFilter.Get1(i);
            //     ref var move = ref _fieldMoveFilter.Get2(i);
            //     ref var blockMove = ref _fieldMoveFilter.Get3(i);

            //     if (blockMove.isInput)
            //     {
            //         int currentBlockX = (int)move.currentBlock.transform.position.x;

            //         if (currentBlockX > 0 && currentBlockX < field.wight)
            //         {
            //             move.moving = new Vector3(1, 1, 1);
            //         }
            //         else if (currentBlockX >= field.wight)
            //         {
            //             move.moving = new Vector3(1, 1, 0);
            //         }
            //         else if (currentBlockX <= 0)
            //         {
            //             move.moving = new Vector3(0, 1, 1);
            //         }
            //     }

            //     if (blockMove.isFalling)
            //     {

            //         int currentBlockX = (int)move.currentBlock.transform.position.x;
            //         int currentBlockY = (int)move.currentBlock.transform.position.y;
            //         ref var game = ref _fieldMoveFilter.Get4(i);

            //         if (currentBlockY == field.heightLines[currentBlockX])
            //         {
            //             field.blocks[currentBlockX, currentBlockY] = move.currentBlock;
            //             field.heightLines[currentBlockX] = currentBlockY + 1;
            //             game.delayMerge = Time.time + Game.globalVar.delayMerge;

            //             blockMove.isMove = false;
            //             blockMove.isFalling = false;
            //             game.isLine = true;
            //         }
            //     }
            // }

        }
    }
}