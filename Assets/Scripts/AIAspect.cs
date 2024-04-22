using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;
using UnityEngine.AI;

public readonly partial struct AIAspect : IAspect
{
    public readonly Entity entity;

    private readonly RefRW<LocalTransform> _transform;
    private LocalTransform Transform => _transform.ValueRO;

    private readonly RefRO<AgentProperties> _AIProperties;
    private readonly RefRW<AIRandom> _AIRandom;

    public int NumberOfAgentsToSpawn => _AIProperties.ValueRO.numberOfAgentsToSpawn;
    public Entity AgentPrefab => _AIProperties.ValueRO.agentPrefab;

    public LocalTransform getRandomAgentTransform()
    {
        return new LocalTransform
        {
            Position = GetRandomPosition(),
            Rotation = quaternion.identity,
            Scale = 1f
        };
    }

    private float3 GetRandomPosition()
    {
        float3 randomPosition;
        int attempts = 0;
        do
        {
            randomPosition = _AIRandom.ValueRW.value.NextFloat3(MinCorner, MaxCorner);
            attempts++;
        } while (!CheckValidSpawn(randomPosition, attempts));

        randomPosition.y = 0.5f;
    
        return randomPosition;
    }

    private float3 MinCorner => Transform.Position - HalfDimensions;
    private float3 MaxCorner => Transform.Position + HalfDimensions;
    private float3 HalfDimensions => new()
    {
        x = _AIProperties.ValueRO.spawnRange.x * 0.5f,
        y = 0f,
        z = _AIProperties.ValueRO.spawnRange.y * 0.5f
    };

    private bool CheckValidSpawn(float3 _randomPosition, int _attempts)
    {
        LayerMask agentMask = LayerMask.GetMask("Agent");
        bool validSpawn = false;

        if(_attempts >= 10)
        {
            Debug.Log("Too many attempts");
            return true;
        }

        if (NavMesh.SamplePosition(_randomPosition, out _, 1.0f, NavMesh.AllAreas))
        {
            if(Physics.OverlapSphere(_randomPosition, 1, agentMask).Length == 0)
            {
                validSpawn = true;
            }
        }
        return validSpawn;
    }
}