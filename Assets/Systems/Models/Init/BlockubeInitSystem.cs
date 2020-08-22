using Leopotam.Ecs;
using UnityEngine;
using GrayscaleBlock3D.AppSettings;
using GrayscaleBlock3D.Components.Canvas;
using GrayscaleBlock3D.Components.Events;
using GrayscaleBlock3D.Extensions;
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
            var entity = _world.NewEntity();

            var color1 = Additive.GetRandomColor(_gameConfiguration, out ushort numberColor1);
            var color2 = Additive.GetRandomColor(_gameConfiguration, out ushort numberColor2);

            entity.Get<MainBlockComponent>().CurrentBlock = new Blockube(_sceneData.CurrentBlock, color1, numberColor1);
            entity.Get<MainBlockComponent>().PreviewBlock = new Blockube(_sceneData.PreviewBlock, color2, numberColor2);

            entity.Get<RotateComponent>().CurrentBlockRotate = new Blockube(_sceneData.CurrentBlockRotate, color1, numberColor1);
            entity.Get<RotateComponent>().PreviewBlockRotate = new Blockube(_sceneData.PreviewBlockRotate, color2, numberColor2);
            entity.Get<RotateComponent>().MainPlace = _sceneData.MainBlocksPlace;
            entity.Get<RotateComponent>().RotationPlace = _sceneData.RotatePlace;

            //entity.Get<ColorChargeComponent>().Current = _sceneData.CurrentBlock.transform.position;
            //entity.Get<ColorChargeComponent>().Preview = _sceneData.PreviewBlock.transform.position;
            entity.Get<MoveComponent>();
            entity.Get<IsCanFallComponent>();
            entity.Get<TimerFallingSetupComponent>().FallTimeSec = _gameConfiguration.DelayFall;
            entity.Get<TimerFallingSetupComponent>().FallUnit = _gameConfiguration.SpeedFall;
            entity.Get<TimerMergingSetupComponent>().MergeTimeSec = _gameConfiguration.DelayMerge;
            entity.Get<TimerRemoveLineSetupComponent>().RemoveTimeSec = _gameConfiguration.DelayMerge * 2;
        }
    }
}