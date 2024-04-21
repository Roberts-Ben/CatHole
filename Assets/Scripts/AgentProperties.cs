using Unity.Entities;
using Unity.Mathematics;

public struct AgentProperties : IComponentData
{
    public Entity agentPrefab;
    public int numberOfAgentsToSpawn;
    public float2 spawnRange;
}