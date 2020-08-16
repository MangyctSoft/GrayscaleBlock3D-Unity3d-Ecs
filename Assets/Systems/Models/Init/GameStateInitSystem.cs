using Leopotam.Ecs;
using GrayscaleBlock3D.Components.Requests;
using GrayscaleBlock3D.Extensions;
using GrayscaleBlock3D.AppSettings;
using GrayscaleBlock3D.Components.Events.InputEvents;

namespace GrayscaleBlock3D.Systems.Models.Init
{
    internal class GameStateInitSystem : IEcsInitSystem
    {
        private readonly EcsWorld _world = null;

        void IEcsInitSystem.Init()
        {
            _world.SendMessage(new ChangeGameStateRequest() { State = GameStates.Pause });
            _world.SendMessage(new InputNonConstrainMoveEvent());
        }
    }
}