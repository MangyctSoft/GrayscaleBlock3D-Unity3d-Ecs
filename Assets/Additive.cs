using UnityEngine;
using GrayscaleBlock3D.AppSettings;

namespace GrayscaleBlock3D
{
    public class Additive
    {
        public static Color GetRandomColor(GameConfiguration _gameConfiguration, out ushort randomColor)
        {
            randomColor = (ushort)UnityEngine.Random.Range(_gameConfiguration.StartColor, _gameConfiguration.EndColor);
            return SetColor(_gameConfiguration, randomColor);
            // var number = _gameConfiguration.BlockColors[randomColor];
            // return new Color(number, number, number, 1f);
        }

        public static Color SetColor(GameConfiguration _gameConfiguration, ushort numberColor)
        {
            var number = _gameConfiguration.BlockColors[numberColor];
            return new Color(number, number, number, 1f);
        }
    }
}