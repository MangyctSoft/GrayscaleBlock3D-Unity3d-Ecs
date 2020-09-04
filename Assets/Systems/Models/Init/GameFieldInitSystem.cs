using Leopotam.Ecs;
using GrayscaleBlock3D.AppSettings;
using GrayscaleBlock3D.Systems.Models.Data;

namespace GrayscaleBlock3D.Systems.Models.Init
{
    internal sealed class GameFieldInitSystem : IEcsInitSystem
    {
        private readonly GameConfiguration _gameConfiguration = null;
        private readonly GameContext _gameContext = null;
        void IEcsInitSystem.Init() => SetGameField();
        private void SetGameField()
        {
            int x0 = (int)_gameConfiguration.SizeField.x;
            int y0 = (int)_gameConfiguration.SizeField.y;
            _gameContext.GameField = new Blockube[x0, y0];
            _gameContext.IdenticalBlocks = new int[x0, y0];
            _gameContext.RedLine = new int[x0];
        }
    }
}