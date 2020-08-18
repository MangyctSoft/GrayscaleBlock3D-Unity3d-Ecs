using GrayscaleBlock3D.AppSettings;
using Leopotam.Ecs;
using Leopotam.Ecs.UnityIntegration;
using UnityEngine;
using GrayscaleBlock3D.Systems.Models.Init;
using GrayscaleBlock3D.Systems.Controller;
using GrayscaleBlock3D.Components.Events.InputEvents;
using GrayscaleBlock3D.Components.Events.FieldEevents;
using GrayscaleBlock3D.Components.Events;

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
                //Add(new GameFieldTestInitSystem())
                .Add(new GameFieldInitSystem())
                .Add(new ScoreInitSystem())
                .Add(new BlockubeInitSystem())
                .Add(new GameStateInitSystem())

                // View


                // Controller (UiEvents, InputEvents, Init GameObjects on Scene(Create Entities))

                .Add(new InputSystem()) // TODO not all controls

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
                .Add(new RandomColorSystem())
                .Add(new EffectSystem())


                .Add(new FindLineSystem())
                .Add(new RemoveLineSystem())
                .Add(new RemoveLineTimerStartSystem())

                .Add(new MergeSystem())
                .Add(new MergeTimerStartSystem())




                .Add(new TimersSystem())

                // .Add(new MoveProcessing())
                // .Add(new FieldProcessing())
                // .Add(new TestFieldProcessing())
                // .Add(new InputProcessing())
                // .Add(new LineProcessing())
                // .Add(new MergeProcessing())
                // .Add(new MergeGlobalProcessing())

                // register one-frame components
                .OneFrame<InputMoveStartedEvent>()
                .OneFrame<InputMoveCanceledEvent>()
                .OneFrame<InputFallStartedEvent>()
                //.OneFrame<InputFallCanceledEvent>()
                .OneFrame<ChangePositionEvent>()
                .OneFrame<IsFallMadeEvent>()
                .OneFrame<BlockInstallToFieldEvent>()

                //.OneFrame<MergeStartEvent>()
                //.OneFrame<MergeExecuteEvent>()
                .OneFrame<IsRemoveLineMadeEvent>()

                .OneFrame<IsMergeMadeEvent>()

                //.OneFrame<BlockInstallColorEvent>()
                .OneFrame<SetRandomColorEvent>()

                // inject 
                .Inject(gameConfiguration)
                .Inject(GetComponent<SceneData>())
                .Inject(gameContext)

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