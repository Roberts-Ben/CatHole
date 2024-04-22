using Unity.Entities;
using Unity.Burst;
using Unity.Collections;
using Unity.Transforms;

[BurstCompile]
[UpdateInGroup(typeof(InitializationSystemGroup))]
public partial struct AISpawnSystem : ISystem
{
    [BurstCompile]
    public void OnCreate(ref SystemState state)
    {
        state.RequireForUpdate<AgentProperties>();
    }
    [BurstCompile]
    public void OnDestroy(ref SystemState state)
    {

    }
    [BurstCompile]
    public void OnUpdate(ref SystemState state)
    {
        state.Enabled = false;
        var agentEntity = SystemAPI.GetSingletonEntity<AgentProperties>();
        var AIAspect = SystemAPI.GetAspectRW<AIAspect>(agentEntity);

        var ecb = new EntityCommandBuffer(Allocator.Temp);

        for (int i = 0; i < AIAspect.NumberOfAgentsToSpawn; i++)
        {
            var newAgent = ecb.Instantiate(AIAspect.AgentPrefab);
            var newAgentTransform = AIAspect.getRandomAgentTransform();
            ecb.SetComponent(newAgent, newAgentTransform);
        }

        ecb.Playback(state.EntityManager);
        ecb.Dispose();
    }
}
