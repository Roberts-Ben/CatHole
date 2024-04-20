using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AgentSpawner : MonoBehaviour
{
    public GameObject agentPrefab;
    public int numberOfAgents;
    public float range;
    void Awake()
    {

        for(int i = 0; i < numberOfAgents; i++)
        {
            for (int o = 0; o < 30; o++)
            {
                Vector3 randomPoint = Vector3.zero + Random.insideUnitSphere * range;
                randomPoint.y = 0.015f;
                NavMeshHit hit;

                if (NavMesh.SamplePosition(randomPoint, out hit, 1.0f, NavMesh.AllAreas))
                {
                    GameObject newAgent = Instantiate(agentPrefab, randomPoint, Quaternion.identity, transform);
                    newAgent.name = "Agent " + i;
                    break;
                }

                Debug.Log("Agent: " + i + " could not spawn at" + randomPoint);
            }
        }
    }
}
