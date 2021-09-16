//using Unity.Burst;
//using Unity.Collections;
//using Unity.Entities;
//using Unity.Jobs;
//using Unity.Mathematics;
//using Unity.Physics;
//using Unity.Physics.Systems;
//using Unity.Tiny;
//using Unity.Transforms;

//public class StarSystem : JobComponentSystem
//{
//    private BuildPhysicsWorld buildPhysicWorld;
//    private StepPhysicsWorld stepPhysicsWorld;

//    protected override void OnCreate()
//    {
//        buildPhysicWorld = World.GetOrCreateSystem<BuildPhysicsWorld>();
//        stepPhysicsWorld = World.GetOrCreateSystem<StepPhysicsWorld>();
//    }

//    [BurstCompile]
//    private struct ApplicationJob : ITriggerEventsJob
//    {
//        public ComponentDataFromEntity<Star> starGroup;
//        public ComponentDataFromEntity<PlayerEntity> playerGroup;
//        public ComponentDataFromEntity<PhysicsVelocity> velocityGroup;

//        public void Execute(TriggerEvent triggerEvent)
//        {
//            if (starGroup.HasComponent(triggerEvent.EntityA))
//            {
//                if (playerGroup.HasComponent(triggerEvent.EntityB))
//                {
//                    if (velocityGroup.HasComponent(triggerEvent.EntityB))
//                    {
//                        PhysicsVelocity velocity = velocityGroup[triggerEvent.EntityB];
//                        velocity.Linear = float3.zero;
//                        velocityGroup[triggerEvent.EntityB] = velocity;
//                    }
//                }
//            }

//            if (starGroup.HasComponent(triggerEvent.EntityB))
//            {
//                if (playerGroup.HasComponent(triggerEvent.EntityA))
//                {
//                    if (velocityGroup.HasComponent(triggerEvent.EntityA))
//                    {
//                        PhysicsVelocity velocity = velocityGroup[triggerEvent.EntityA];
//                        velocity.Linear = float3.zero;
//                        velocityGroup[triggerEvent.EntityA] = velocity;
//                    }
//                }
//            }
//        }
//    }

//    protected override JobHandle OnUpdate(JobHandle inputDeps)
//    {
//        var applicationJob = new ApplicationJob
//        {
//            starGroup = GetComponentDataFromEntity<Star>(),
//            playerGroup = GetComponentDataFromEntity<PlayerEntity>()
//        };

//        return applicationJob.Schedule(stepPhysicsWorld.Simulation, ref buildPhysicWorld.PhysicsWorld, inputDeps);
//    }
//}