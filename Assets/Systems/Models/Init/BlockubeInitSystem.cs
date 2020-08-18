using Leopotam.Ecs;
using UnityEngine;
using GrayscaleBlock3D.AppSettings;
using GrayscaleBlock3D.Components.Canvas;
using GrayscaleBlock3D.Components;
using GrayscaleBlock3D.Extensions.Components;
using GrayscaleBlock3D.Components.Player;
using GrayscaleBlock3D.Systems.Models.Data;

namespace GrayscaleBlock3D.Systems.Models.Init
{
    internal sealed class BlockubeInitSystem : IEcsInitSystem
    {
        // auto-injected fields.
        private readonly EcsWorld _world = null;
        private readonly SceneData _sceneData = null;
        private readonly GameConfiguration _gameConfiguration = null;

        void IEcsInitSystem.Init()
        {
            InitBlockube(_sceneData.CurrentBlockPrefab, _gameConfiguration);
        }

        private void InitBlockube(GameObject playerTransform, GameConfiguration _gameConfiguration)
        {
            // int numberColor = UnityEngine.Random.Range(_gameConfiguration.StartColor, _gameConfiguration.EndColor);
            // var number = _gameConfiguration.BlockColors[numberColor];
            // var newColor = new Color(number, number, number, 1f);
            var newColor = Additive.GetRandomColor(_gameConfiguration, out ushort numberColor);
            var player = CreateBlockube(playerTransform, newColor, numberColor);
        }

        private EcsEntity CreateBlockube(GameObject currentBlock, Color color, ushort numberColor)
        {
            if (currentBlock == null) return default;

            var entity = _world.NewEntity();
            entity.Get<MainBlockComponent>().Blockube = new Blockube(currentBlock, color, numberColor);
            entity.Get<WrapperUnityObjectComponent<Renderer>>().Value = currentBlock.GetComponent<Renderer>();
            //entity.Get<ManagerBlockComponent>();
            entity.Get<MoveComponent>();
            entity.Get<IsCanFallComponent>();
            entity.Get<TimerFallingSetupComponent>().FallTimeSec = _gameConfiguration.DelayFall;
            entity.Get<TimerFallingSetupComponent>().FallUnit = _gameConfiguration.SpeedFall;
            entity.Get<TimerMergingSetupComponent>().MergeTimeSec = _gameConfiguration.DelayMerge;
            entity.Get<TimerRemoveLineSetupComponent>().RemoveTimeSec = _gameConfiguration.DelayMerge * 2;
            return entity;
        }


    }
}