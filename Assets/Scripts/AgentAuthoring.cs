using UnityEngine;
using Unity.Mathematics;
using Unity.Entities;

public class AgentAuthoring : MonoBehaviour
{
    public GameObject agentPrefab;
    public int numberOfAgentsToSpawn;
    public float2 spawnRange;
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
    }
}
    /*void Awake()
    {

        for(int i = 0; i < numberOfAgents; i++)
        {
            for (int o = 0; o < 30; o++)
            {
                Vector3 randomPoint = Vector3.zero + Random.insideUnitSphere * range;
                randomPoint.y = 0.25f;
                if (NavMesh.SamplePosition(randomPoint, out _, 1.0f, NavMesh.AllAreas))
                {
                    GameObject newAgent = Instantiate(agentPrefab, randomPoint, Quaternion.identity, transform);
                    newAgent.transform.GetComponentInChildren<Renderer>().material.color = Color.black;
                    newAgent.name = "Agent " + i;
                    break;
                }

                Debug.Log("Agent: " + i + " could not spawn at" + randomPoint);
            }
        }
    }*/