using UnityEngine;
using Leopotam.Ecs;
using GrayscaleBlock3D.AppSettings;
using GrayscaleBlock3D.Components.Player;
using GrayscaleBlock3D.Extensions;
using GrayscaleBlock3D.Components.Events;
using GrayscaleBlock3D.Components.Events.InputEvents;

namespace GrayscaleBlock3D.Systems.Controller
{
    internal sealed class RotateColorChangeSystem : IEcsRunSystem
    {
        private readonly GameConfiguration _gameConfiguration = null;
        private readonly EcsFilter<ColorChangeStartEvent>.Exclude<RotateColorChangeEventX> _filterChangeStart = null;
        private readonly EcsFilter<RotateComponent> _filterComponent = null;
        private readonly EcsFilter<RotateColorChangeEventX> _filterRotate = null;
        private readonly EcsFilter<MainBlockComponent, RotateComponent> _filterMainBlock = null;
        void IEcsRunSystem.Run()
        {
            foreach (var i in _filterChangeStart)
            {
                SetColorToFakes();
                RotateActivePlace();
                _filterComponent.GetEntity(i).Get<RotateColorChangeEventX>();
            }

            foreach (var i in _filterRotate)
            {
                RotateBlocks();
            }
        }

        private void RotateActivePlace()
        {
            foreach (var i in _filterComponent)
            {
                ref var component = ref _filterComponent.Get1(i);
                component.MainPlace.RotateActive();
                component.RotationPlace.RotateActive();
            }
        }
        private void SetColorToFakes()
        {
            foreach (var i in _filterMainBlock)
            {
                ref var blocks = ref _filterMainBlock.Get1(i);
                ref var fakes = ref _filterMainBlock.Get2(i);

                var currentColor = Additive.SetColor(_gameConfiguration, blocks.CurrentBlock.NumberColor);
                fakes.CurrentBlockRotate.Color = currentColor;

                var previewColor = Additive.SetColor(_gameConfiguration, blocks.PreviewBlock.NumberColor);
                fakes.PreviewBlockRotate.Color = previewColor;
            }
        }
        private void RotateBlocks()
        {
            foreach (var i in _filterComponent)
            {
                ref var component = ref _filterComponent.Get1(i);

                component.RotationPlace.transform.Rotate(0, 0, -25f * Time.deltaTime * 10f);

                if (component.RotationPlace.transform.rotation.eulerAngles.z < 180)
                {
                    RotateActivePlace();
                    ChangeBlocks();

                    _filterComponent.GetEntity(i).Del<RotateColorChangeEventX>();
                }
            }
        }

        private void ChangeBlocks()
        {
            foreach (var i in _filterMainBlock)
            {
                ref var blocks = ref _filterMainBlock.Get1(i);
                ref var fakes = ref _filterMainBlock.Get2(i);

                var currentNumberColor = blocks.CurrentBlock.NumberColor;
                var currentNewColor = Additive.SetColor(_gameConfiguration, blocks.PreviewBlock.NumberColor);
                blocks.CurrentBlock.Color = currentNewColor;
                blocks.CurrentBlock.NumberColor = blocks.PreviewBlock.NumberColor;
                fakes.CurrentBlockRotate.Color = currentNewColor;
                //fakes.CurrentBlockRotate.NumberColor = blocks.PreviewBlock.NumberColor;

                var previewNewColor = Additive.SetColor(_gameConfiguration, currentNumberColor);
                blocks.PreviewBlock.Color = previewNewColor;
                blocks.PreviewBlock.NumberColor = currentNumberColor;
                fakes.PreviewBlockRotate.Color = previewNewColor;
                //fakes.PreviewBlockRotate.NumberColor = currentNumberColor;

                var angel = fakes.RotationPlace.transform.rotation.eulerAngles.z;
                fakes.RotationPlace.transform.Rotate(0f, 0f, -angel);

                _filterMainBlock.GetEntity(i).Get<InputNonConstrainMoveEvent>();
            }
        }
    }
}