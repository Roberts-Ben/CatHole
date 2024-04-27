using UnityEngine;
using UnityEngine.AI;

public class AgentSpawner : MonoBehaviour
{
    public GameObject agentPrefab;
    public int numberOfAgentsToSpawn;
    public float range;

    void Awake()
    {
        for (int i = 0; i < numberOfAgents; i++)
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
    }
}