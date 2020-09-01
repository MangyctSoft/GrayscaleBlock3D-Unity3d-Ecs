using GrayscaleBlock3D.AppSettings;
using Leopotam.Ecs;
using Leopotam.Ecs.UnityIntegration;
using UnityEngine;
using GrayscaleBlock3D.Systems.Models.Init;
using GrayscaleBlock3D.Systems.Controller;
using GrayscaleBlock3D.Components.Events.InputEvents;
using GrayscaleBlock3D.Components.Events.FieldEevents;
using GrayscaleBlock3D.Components.Events;
using GrayscaleBlock3D.Components.Requests;

namespace GrayscaleBlock3D
{
    internal sealed class Game : MonoBehaviour
    {
        public GameConfiguration gameConfiguration = null;
        EcsWorld _world;
        EcsSystems _systems;


        void Start()
        {
            var gameContext = new GameContext();


            _world = new EcsWorld();
            _systems = new EcsSystems(_world);

#if UNITY_EDITOR
            EcsWorldObserver.Create(_world);
            EcsSystemsObserver.Create(_systems);
#endif

            _systems

                // Model
                .Add(new GameFieldInitSystem())
                .Add(new ScoreInitSystem())
                .Add(new BlockubeInitSystem())
                .Add(new GameStateInitSystem())
                .Add(new TargetInitSystem())

                // View


                // Controller
                .Add(new InputSystem())

                .Add(new GameStateInputSystem()) // TODO : implement input

                .Add(new MoveInputSystem())
                .Add(new MoveExecuteSystem())
                .Add(new MoveBesideBorderSystem())

                .Add(new FallInputSystem())
                .Add(new FallOnRedLineSystem())
                .Add(new FallExecuteSystem())
                .Add(new FallTimerStartSystem())

                .Add(new BlockToFieldSystem())
                .Add(new BlockInstallColorSystem())
                .Add(new NextColorSystem())

                .Add(new FindLineSystem())
                .Add(new RemoveLineSystem())
                .Add(new RemoveLineTimerStartSystem())
                .Add(new BoomSystem())

                .Add(new MergeSystem())
                .Add(new MergeTimerStartSystem())

                .Add(new RotateColorChangeSystem())

                .Add(new TargetSystem())
                .Add(new TargetOnOffSystem())

                .Add(new TimersSystem())

                // register one-frame components
                .OneFrame<InputMoveStartedEvent>()
                .OneFrame<InputMoveCanceledEvent>()
                .OneFrame<InputFallStartedEvent>()
                .OneFrame<ChangePositionEvent>()
                .OneFrame<IsFallMadeEvent>()


                .OneFrame<BlockInstallToFieldEvent>()

                .OneFrame<IsRemoveLineMadeEvent>()

                .OneFrame<IsMergeMadeEvent>()

                .OneFrame<SetNextColorEvent>()
                .OneFrame<GameOverEvent>()
                .OneFrame<ColorChangeStartEvent>()
                .OneFrame<TargetEvent>()
                .OneFrame<IsBoomBlockEvent>()

                // inject 
                .Inject(gameConfiguration)
                .Inject(GetComponent<SceneData>())
                .Inject(gameContext)
                .Inject(new PoolsObject())
                .Init();
        }

        void Update()
        {
            _systems?.Run();
        }
        void OnDestroy()
        {
            if (_systems != null)
            {
                _systems.Destroy();
                _systems = null;
                _world.Destroy();
                _world = null;
            }
        }
    }

}