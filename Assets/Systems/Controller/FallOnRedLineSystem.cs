using Leopotam.Ecs;
using GrayscaleBlock3D.AppSettings;
using GrayscaleBlock3D.Components.Player;
using GrayscaleBlock3D.Components.Events.FieldEevents;
using GrayscaleBlock3D.Components.Events.InputEvents;
using GrayscaleBlock3D.Components.Timers;
using GrayscaleBlock3D.Extensions;

namespace GrayscaleBlock3D.Systems.Controller
{
    internal sealed class FallOnRedLineSystem : IEcsRunSystem
    {
        // auto-injected fields.
        private readonly GameContext _gameContext = null;
        private readonly EcsWorld _world = null;

        private readonly EcsFilter<MoveComponent, MainBlockComponent, TimerFallingComponent> _filter = null;

        void IEcsRunSystem.Run()
        {
            foreach (var i in _filter)
            {
                ref var moveComponent = ref _filter.Get1(i);
                ref var blockComponent = ref _filter.Get2(i);
                var position = blockComponent.Blockube.Position;

                if (position.y.Equals(_gameContext.RedLine[(int)position.x < 0 ? 0 : (int)position.x >= _gameContext.RedLine.Length ? _gameContext.RedLine.Length - 1 : (int)position.x]))
                {
                    moveComponent.Speed = 0;
                    ref var nextStep = ref _filter.GetEntity(i);
                    nextStep.Del<TimerFallingComponent>();
                    nextStep.Get<BlockInstallToFieldEvent>();
                    // cancel.Get<InputFallCanceledEvent>();
                    // cancel.Get<InputNonConstrainMoveEvent>();
                    //_world.SendMessage(new InputFallCanceledEvent());
                    //_world.SendMessage(new InputNonConstrainMoveEvent());
                    //cancel.Get<InputFallCanceledEvent>();

                }
            }
        }
    }
}