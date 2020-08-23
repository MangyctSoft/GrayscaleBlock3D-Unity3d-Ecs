using System.Collections.Generic;
using Leopotam.Ecs;
using GrayscaleBlock3D.AppSettings;
using GrayscaleBlock3D.Components.Player;
using GrayscaleBlock3D.Components.Events.InputEvents;
using GrayscaleBlock3D.Components.Events.FieldEevents;
using GrayscaleBlock3D.Components.Events;
using GrayscaleBlock3D.Components.Timers;
using UnityEngine;

namespace GrayscaleBlock3D.Systems.Controller
{
    internal sealed class NextColorSystem : IEcsRunSystem
    {
        private readonly GameConfiguration _gameConfiguration;
        private readonly EcsFilter<SetNextColorEvent> _filter = null;
        private readonly EcsFilter<MainBlockComponent> _filterComponent = null;
        void IEcsRunSystem.Run()
        {
            foreach (var i in _filter)
            {
                SetNextColor();
            }
        }
        private void SetNextColor()
        {
            foreach (var i in _filterComponent)
            {
                ref var blocks = ref _filterComponent.Get1(i);

                var newColor = Additive.GetRandomColor(_gameConfiguration, out ushort numberNewColor);

                blocks.CurrentBlock.Color = Additive.SetColor(_gameConfiguration, blocks.PreviewBlock.NumberColor);
                blocks.CurrentBlock.NumberColor = blocks.PreviewBlock.NumberColor;

                blocks.PreviewBlock.Color = newColor;
                blocks.PreviewBlock.NumberColor = numberNewColor;

            }
        }
    }
}