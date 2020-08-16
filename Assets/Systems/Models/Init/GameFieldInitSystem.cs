using Leopotam.Ecs;
using GrayscaleBlock3D.AppSettings;
using GrayscaleBlock3D.Components;
using GrayscaleBlock3D.Systems.Models.Data;
using UnityEngine;

namespace GrayscaleBlock3D.Systems.Models.Init
{
    internal sealed class GameFieldInitSystem : IEcsInitSystem
    {
        private readonly GameConfiguration _gameConfiguration = null;
        private readonly GameContext _gameContext = null;
        private readonly SceneData _sceneData = null;
        void IEcsInitSystem.Init() => SetGameField();
        private void SetGameField()
        {
            int x0 = (int)_gameConfiguration.SizeField.x;
            int y0 = (int)_gameConfiguration.SizeField.y;
            _gameContext.GameField = new Blockube[x0, y0];
            _gameContext.RedLine = new int[x0];

            for (int x = 0; x < _gameContext.GameField.GetLength(0); x++)
            {
                for (int y = 0; y < _gameContext.GameField.GetLength(1); y++)
                {
                    var block = GameObject.Instantiate(_gameConfiguration.BlockubePrefab, new Vector3(x, y, 0), Quaternion.identity, _sceneData.PlaceBlocks);
                    block.SetActive(false);
                    var item = new Blockube(block, new Color(), 0);
                    _gameContext.GameField[x, y] = item;
                }
            }
        }
    }
}