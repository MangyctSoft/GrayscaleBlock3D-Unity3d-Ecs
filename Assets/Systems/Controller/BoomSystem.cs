using Leopotam.Ecs;
using GrayscaleBlock3D.Systems.Models.Data;
using GrayscaleBlock3D.Components.Events.FieldEevents;
using GrayscaleBlock3D.AppSettings;
using GrayscaleBlock3D.Components.Player;
using GrayscaleBlock3D.Extensions;
using GrayscaleBlock3D.Pooling;
using UnityEngine;

namespace GrayscaleBlock3D.Systems.Controller
{
    internal sealed class BoomSystem : IEcsRunSystem
    {
        private readonly PoolsObject _poolsObject = null;
        private readonly EcsFilter<IsBoomBlockEvent> _filter = null;
        void IEcsRunSystem.Run()
        {
            foreach (var i in _filter)
            {
                ref var positions = ref _filter.Get1(i).Position;
                foreach (var item in positions)
                {
                    var poolObjectBoom = (PoolObjectBoom)_poolsObject.Booms.Get();
                    var transformBoom = poolObjectBoom.PoolTransform;
                    transformBoom.position = new Vector3(item.x, item.y);
                    transformBoom.gameObject.SetActive(true);
                    var rigidbodyBoom = poolObjectBoom.ChildRigidbody;
                    var boom = new Boom(transformBoom, rigidbodyBoom, poolObjectBoom);
                    boom.SetBoom();
                }

            }
        }
    }
}