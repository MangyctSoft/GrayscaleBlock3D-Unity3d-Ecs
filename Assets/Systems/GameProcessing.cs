using UnityEngine;
using Leopotam.Ecs;

namespace GrayscaleBlock3D
{
    public class GameProcessing : IEcsInitSystem, IEcsRunSystem
    {
        readonly EcsWorld _world = null;
        EcsFilter<GameComponent, MovableComponent, MoveBlockComponent> _gameFilter = null;
        public void Init()
        {

            // var spawnPosition = Game.globalVar.spawnPosition;
            // var worldEntity = _world.NewEntity();
            // ref MovableComponent movable = ref worldEntity.Get<MovableComponent>();
            // ref FieldComponent field = ref worldEntity.Get<FieldComponent>();
            // ref BlockMoveComponent blockMove = ref worldEntity.Get<BlockMoveComponent>();
            // ref GameComponent game = ref worldEntity.Get<GameComponent>();

            // blockMove.isMove = false;

            // field.wight = Game.globalVar.fieldWidth;
            // field.height = Game.globalVar.fieldHeight;
            // field.blocks = new Blockube[field.wight, field.height];
            // field.testBlocks = new Blockube[field.wight, field.height];

            // field.heightLines = new int[field.wight];
            // field.wight = Game.globalVar.fieldWidth - 1;

            // game.isRespawn = true;

            // for (int x = 0; x < field.testBlocks.GetLength(0); x++)
            //     for (int y = 0; y < field.testBlocks.GetLength(1); y++)
            //     {
            //         var spawnBlock = GameObject.Instantiate(BlockData.LoadFromAssets().blockPrefab, new Vector3(x, y, +10), Quaternion.identity);

            //         var currentBlock = new Blockube
            //         {
            //             transform = spawnBlock.transform,
            //             renderer = spawnBlock.GetComponent<Renderer>(),
            //             color = 0
            //         };
            //         field.testBlocks[x, y] = currentBlock;
            //     }

        }
        public void Run()
        {
            // foreach (var i in _gameFilter)
            // {
            //     ref var game = ref _gameFilter.Get1(i);

            //     if (game.isRespawn)
            //     {
            //         game.isRespawn = false;

            //         ref var movable = ref _gameFilter.Get2(i);
            //         ref var blockMove = ref _gameFilter.Get3(i);

            //         var spawnBlock = GameObject.Instantiate(BlockData.LoadFromAssets().blockPrefab, Game.globalVar.spawnPosition, Quaternion.identity);

            //         movable.moveSpeed = BlockData.LoadFromAssets().defaultSpeed;
            //         var currentBlock = new Blockube
            //         {
            //             transform = spawnBlock.transform,
            //             renderer = spawnBlock.GetComponent<Renderer>(),
            //             color = GetRandomColor(Game.globalVar.startColor, Game.globalVar.endColor)
            //         };
            //         // Debug.LogFormat("currentColor = {0}, colors[x] = {1}", currentBlock.color, colors[currentBlock.color]);
            //         var currentColor = new Color(colors[currentBlock.color],
            //                                     colors[currentBlock.color],
            //                                     colors[currentBlock.color], 1f);

            //         currentBlock.renderer.material.SetColor("_Color", currentColor);

            //         movable.currentBlock = currentBlock;
            //         movable.moving = Vector3.one;

            //         blockMove.isMove = true;
            //         blockMove.isInput = false;
            //         blockMove.isFalling = false;

            //         game.isMerge = false;
            //         game.isMergeGlobal = false;
            //         game.isNextMerge = false;
            //         game.isLine = false;
            //         game.delayMerge = 0f;
            //         game.delayMergeGlobal = 0f;
            //     }
            // }
        }
        private int GetRandomColor(int start, int end)
        {
            return Random.Range(start, end);
        }
    }


}