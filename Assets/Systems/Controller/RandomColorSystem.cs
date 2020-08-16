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
    internal sealed class RandomColorSystem : IEcsRunSystem
    {
        private readonly GameConfiguration _gameConfiguration;
        private readonly GameContext _gameContext = null;
        private readonly EcsFilter<MainBlockComponent, SetRandomColorEvent> _filter = null;


        void IEcsRunSystem.Run()
        {
            foreach (var i in _filter)
            {
                ref var block = ref _filter.Get1(i);

                var newColor = Additive.GetRandomColor(_gameConfiguration, out ushort numberColor);
                block.Blockube.Color = newColor;
                block.Blockube.NumberColor = numberColor;

            }
        }
    }
}