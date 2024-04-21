using UnityEngine;
using Unity.Mathematics;
using Unity.Entities;

public class AgentAuthoring : MonoBehaviour
{
    public GameObject agentPrefab;
    public int numberOfAgentsToSpawn;
    public float2 spawnRange;
    public uint randomSeed;
}

public class AgentBaker : Baker<AgentAuthoring>
{
    public override void Bake(AgentAuthoring authoring)
    {
        var agentEntity = GetEntity(TransformUsageFlags.Dynamic);
        AddComponent(agentEntity, new AgentProperties
        {
            spawnRange = authoring.spawnRange,
            numberOfAgentsToSpawn = authoring.numberOfAgentsToSpawn,
            agentPrefab = GetEntity(authoring.agentPrefab, TransformUsageFlags.Dynamic)
        });

        var spawnEntity = GetEntity(TransformUsageFlags.Dynamic);
        AddComponent(spawnEntity, new AIRandom
        {
            value = Unity.Mathematics.Random.CreateFromIndex(authoring.randomSeed)
        });
    }
}