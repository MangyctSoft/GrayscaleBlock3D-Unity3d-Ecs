using Leopotam.Ecs;
using GrayscaleBlock3D.AppSettings;
using GrayscaleBlock3D.Components.Player;
using UnityEngine;

namespace GrayscaleBlock3D.Systems.Models.Init
{
    internal class TargetInitSystem : IEcsInitSystem
    {
        private readonly EcsWorld _world = null;
        private readonly SceneData _sceneData = null;

        void IEcsInitSystem.Init()
        {
            var target = _world.NewEntity();
            target.Get<TargetComponent>().Target = _sceneData.Target;
            target.Get<TargetComponent>().Renderer = _sceneData.Target.GetComponent<Renderer>();
        }
    }
}