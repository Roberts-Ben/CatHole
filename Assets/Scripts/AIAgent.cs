using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AIAgent : MonoBehaviour
{
    public NavMeshAgent agent;
    private bool hasDestination = false;
    public Vector3 targetPos;
    public float range;
    public float speed;

    public List<Vector3> wayPoints;
    private int currentWaypoint = 0;
    
    void FixedUpdate()
    {
        if(!hasDestination)
        {
            gameObject.GetComponent<NavMeshAgent>().enabled = true;
            if (GetNewDestination(transform.position, range, out targetPos))
            {
                agent.SetDestination(targetPos);

                wayPoints.Clear();
                currentWaypoint = 0;
                for(int i = 1; i < agent.path.corners.Length; i++)
                {
                    wayPoints.Add(agent.path.corners[i]);
                }
                gameObject.GetComponent<NavMeshAgent>().enabled = false;
            }
        }
        else
        {
            if (currentWaypoint < wayPoints.Count)
            {
                transform.position = Vector3.MoveTowards(transform.position, wayPoints[currentWaypoint], speed * Time.deltaTime);

                if (Vector3.Distance(transform.position, wayPoints[currentWaypoint]) < 1f)
                {
                    currentWaypoint += 1;
                }
            }
            else
            {
                hasDestination = false;
            }
        }
    }

    public bool GetNewDestination(Vector3 center, float range, out Vector3 result)
    {
        for (int i = 0; i < 30; i++)
        {
            Vector3 randomPoint = transform.position + Random.insideUnitSphere * range;
            randomPoint.y = 0.015f;
            NavMeshHit hit;

            if (NavMesh.SamplePosition(randomPoint, out hit, 1.0f, NavMesh.AllAreas))
            {
                result = randomPoint;
                hasDestination = true;
                return true; 
            }
        }
        result = Vector3.zero;
        Debug.Log("Could not find position");
        return false;
    }
}
