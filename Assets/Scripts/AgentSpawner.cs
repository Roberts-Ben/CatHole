using UnityEngine;
using UnityEngine.AI;
using System.Collections.Generic;

public class AgentSpawner : MonoBehaviour
{
    public GameObject agentPrefab;
    public int numberOfAgentsToSpawn;
    public float range;

    public List<Color> catColors;

    void Awake()
    {
        for (int i = 0; i < numberOfAgentsToSpawn; i++)
        {
            for (int o = 0; o < 30; o++)
            {
                Vector3 randomPoint = Vector3.zero + Random.insideUnitSphere * range;
                randomPoint.y = 0.25f;
                if (NavMesh.SamplePosition(randomPoint, out _, 1.0f, NavMesh.AllAreas))
                {
                    GameObject newAgent = Instantiate(agentPrefab, randomPoint, Quaternion.identity, transform);
                    newAgent.GetComponentInChildren<Renderer>().material.color = catColors[Random.Range(0, catColors.Count)];
                    newAgent.name = "Agent " + i;
                    break;
                }

                Debug.Log("Agent: " + i + " could not spawn at" + randomPoint);
            }
        }
    }
}