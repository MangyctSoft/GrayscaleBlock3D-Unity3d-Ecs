using System;
using Leopotam.Ecs;
using GrayscaleBlock3D.AppSettings;
using GrayscaleBlock3D.Components;
using UnityEngine;

namespace GrayscaleBlock3D.Systems.Models.Init
{
    /// <summary>
    /// Инициализация игрового поля.
    /// </summary>
    internal sealed class GameFieldTestInitSystem : IEcsInitSystem
    {
        private readonly GameConfiguration _gameConfiguration = null;
        private readonly GameContext _gameContext = null;
        void IEcsInitSystem.Init() => SetGameFieldTest();
        private void SetGameFieldTest()
        {
            int x0 = (int)_gameConfiguration.SizeField.x;
            int y0 = (int)_gameConfiguration.SizeField.y;

            _gameContext.GameFieldTest = new GameField[x0, y0];
            var colors = _gameConfiguration.BlockColors;

            for (int x = 0; x < _gameContext.GameFieldTest.GetLength(0); x++)
            {
                for (int y = 0; y < _gameContext.GameFieldTest.GetLength(1); y++)
                {
                    int numberColor = UnityEngine.Random.Range(0, colors.Length);
                    float newNumColor = colors[numberColor];

                    var newColor = new Color(newNumColor, newNumColor, newNumColor, 1f);
                    var block = GameObject.Instantiate(_gameConfiguration.BlockubePrefab, new Vector3(x, y, 20f), Quaternion.identity);
                    block.GetComponent<Renderer>().material.SetColor("_Color", newColor);
                    var item = new GameField
                    {
                        Transform = block.transform,
                        Renderer = block.GetComponent<Renderer>(),
                        NumberColor = numberColor
                    };
                    _gameContext.GameFieldTest[x, y] = item;
                }
            }
        }
    }
}