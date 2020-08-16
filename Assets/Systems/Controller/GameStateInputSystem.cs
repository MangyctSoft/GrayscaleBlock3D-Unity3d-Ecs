using Leopotam.Ecs;
using GrayscaleBlock3D.AppSettings;
using GrayscaleBlock3D.Components.Events.InputEvents;
using GrayscaleBlock3D.Components.Requests;
using GrayscaleBlock3D.Extensions;

namespace GrayscaleBlock3D.Systems.Controller
{
    internal sealed class GameStateInputSystem : IEcsRunSystem
    {
        // auto-injected fields.
        private readonly EcsWorld _world = null;
        private readonly GameContext _gameContext = null;

        private readonly EcsFilter<InputPlayEvent> _filterPlay = null;
        private readonly EcsFilter<InputPauseEvent> _filterPause = null;
        private readonly EcsFilter<InputQuitEvent> _filterQuit = null;

        void IEcsRunSystem.Run()
        {
            if (!_filterPause.IsEmpty())
            {
                if (_gameContext.GameState == GameStates.Pause) SetGameState(GameStates.Exit);
                if (_gameContext.GameState == GameStates.Play) SetGameState(GameStates.Pause);
            }
            else
            {
                if (!_filterQuit.IsEmpty())
                {
                    if (_gameContext.GameState == GameStates.Pause) SetGameState(GameStates.Play);
                    if (_gameContext.GameState == GameStates.GameOver) SetGameState(GameStates.Restart);
                }
            }
        }
        private void SetGameState(in GameStates gameState) => _world.SendMessage(new ChangeGameStateRequest() { State = gameState });
    }
}