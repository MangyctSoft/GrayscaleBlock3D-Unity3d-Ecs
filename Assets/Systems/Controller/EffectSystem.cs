using Leopotam.Ecs;
using GrayscaleBlock3D.AppSettings;
using GrayscaleBlock3D.Components.Player;
using GrayscaleBlock3D.Components.Events.FieldEevents;
using UnityEngine;

namespace GrayscaleBlock3D.Systems.Controller
{
    internal sealed class EffectSystem : IEcsRunSystem
    {
        private readonly GameConfiguration _gameConfiguration;
        private readonly GameContext _gameContext = null;
        private readonly SceneData _sceneData = null;
        private readonly EcsFilter<ManagerBlockComponent, BlockInstallColorEventX> _filter = null;
        void IEcsRunSystem.Run()
        {
            foreach (var i in _filter)
            {

                ref var block = ref _filter.Get1(i);

                var position = block.Position;
                float angel = 0;

                for (int e = 0; e < 5; e++)
                {
                    angel += 5;
                    var x = (0.02f * Mathf.Cos(angel)) + position.x + 0.5f;
                    var z = (0.02f * Mathf.Sin(angel)) - .5f;
                    var boom = GameObject.Instantiate(_sceneData.ExplosionPrefab, new Vector3(x, position.y + 1.5f, z), Quaternion.identity);
                    boom.GetComponent<Rigidbody>().AddForce(Vector3.up * 30f);


                }
            }
        }
    }
}