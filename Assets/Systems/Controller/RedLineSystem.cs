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
    internal sealed class RedLinedSystem : IEcsRunSystem
    {
        private readonly GameConfiguration _gameConfiguration;
        private readonly GameContext _gameContext = null;
        private readonly EcsFilter<ManagerBlockComponent, RedLineEvent> _filter = null;


        void IEcsRunSystem.Run()
        {
            foreach (var i in _filter)
            {
                Debug.Log("RedLinedSystem!!");

                ref var block = ref _filter.Get1(i);

                var position = block.Position;
                _gameContext.RedLine[(int)position.x] = (int)position.y + _gameContext.ONE_DIFF;

                //ref var nextStep = ref _filter.GetEntity(i);
                //color.Get<SetRandomColorEvent>();
                //nextStep.Get<InputNonConstrainMoveEvent>();
                //color.Get<MainBlockComponent>().Blockube.Position = _gameConfiguration.CurrentBlockPosition;

            }
        }
    }
}