using Leopotam.Ecs;
using GrayscaleBlock3D.AppSettings;
using GrayscaleBlock3D.Components.Player;
using GrayscaleBlock3D.Components.Events.FieldEevents;

namespace GrayscaleBlock3D.Systems.Controller
{
    internal sealed class BlockInstallColorSystem : IEcsRunSystem
    {
        private readonly GameConfiguration _gameConfiguration;
        private readonly GameContext _gameContext = null;
        private readonly EcsFilter<ManagerBlockComponent, BlockInstallColorEventX> _filter = null;
        void IEcsRunSystem.Run()
        {
            foreach (var i in _filter)
            {

                ref var block = ref _filter.Get1(i);

                var position = block.Position;
                var numberColor = block.NumberColor;
                var active = block.Active;

                _gameContext.RedLine[(int)position.x] = (int)position.y + _gameContext.ONE_DIFF;

                var blockInField = _gameContext.GameField[(int)position.x, (int)position.y];
                blockInField.NumberColor = numberColor;

                if (active)
                {
                    blockInField.SetActive(active);
                }
                else
                {
                    var blockUp = _gameContext.GameField[(int)position.x, (int)position.y + _gameContext.ONE_DIFF];
                    blockUp.SetActive(active);
                }

                var color = Additive.SetColor(_gameConfiguration, numberColor);

                blockInField.Color = color;

                ref var nextStep = ref _filter.GetEntity(i);
                nextStep.Del<BlockInstallColorEventX>();
            }
        }
    }
}