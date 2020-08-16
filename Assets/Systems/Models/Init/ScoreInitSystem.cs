using Leopotam.Ecs;
using UnityEngine.UI;
using GrayscaleBlock3D.AppSettings;
using GrayscaleBlock3D.Components.Canvas;
using GrayscaleBlock3D.Extensions.Components;

namespace GrayscaleBlock3D.Systems.Models.Init
{
    internal class ScoreInitSystem : IEcsInitSystem
    {
        private readonly EcsWorld _world = null;
        private readonly SceneData _sceneData = null;

        void IEcsInitSystem.Init()
        {
            _world.NewEntity().Get<ScoreComponent>();
            _world.NewEntity().Get<WrapperUnityObjectComponent<Text>>().Value = _sceneData.ScoreText;
        }
    }
}