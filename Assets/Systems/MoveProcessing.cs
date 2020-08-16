using UnityEngine;
using Leopotam.Ecs;
using System.Collections;
using GrayscaleBlock3D.Components;

namespace GrayscaleBlock3D
{
    enum BlockDirection
    {
        Awaite,
        Right,
        Left,
        Down
    }
    public class MoveProcessing : IEcsRunSystem
    {
        EcsFilter<MovableComponent, MoveBlockComponent> _blockMoveFilter = null;
        float _nextUpdateTime;

        public void Run()
        {

            if (Time.time < _nextUpdateTime)
            {
                return;
            }

            foreach (var i in _blockMoveFilter)
            {
                var move = _blockMoveFilter.Get1(i);
                ref var block = ref _blockMoveFilter.Get2(i);

                // if (block.isInput)
                // {
                //     _nextUpdateTime = Time.time + Game.globalVar.speedBlockMoving;
                // }

                // if (block.isFalling)
                // {
                //     _nextUpdateTime = Time.time + Game.globalVar.speedBlockFalling;
                // }

                // if (block.isInput || block.isFalling)
                // {
                //     block.isInput = false;
                //     var coords = GetForwardCoords(move.moving, block.direction);
                //     move.currentBlock.transform.position += new Vector3(coords.X, coords.Y, 0f);
                // }
            }

        }

        static Coords GetForwardCoords(Vector3 moving, BlockDirection direction)
        {
            Coords coords = default;

            switch (direction)
            {

                case BlockDirection.Right:
                    if (moving.z == 1)
                        coords.X++;
                    break;

                case BlockDirection.Left:
                    if (moving.x == 1)
                        coords.X--;
                    break;
                case BlockDirection.Down:
                    coords.Y--;
                    break;
            }


            return coords;
        }
    }
}