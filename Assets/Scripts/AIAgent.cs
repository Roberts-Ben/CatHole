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

    public Animator animController;

    public List<Vector3> wayPoints;
    private int currentWaypoint = 0;

    void FixedUpdate()
    {
        if(!hasDestination)
        {
            if (GetNewDestination(range, out targetPos))
            {
                agent.SetDestination(targetPos);

                wayPoints.Clear();
                currentWaypoint = 0;
                for(int i = 1; i < agent.path.corners.Length; i++)
                {
                    wayPoints.Add(new Vector3(agent.path.corners[i].x, 0.35f, agent.path.corners[i].z));
                }
            }
        }
        else if(hasDestination)
        {
            if(gameObject.GetComponent<NavMeshAgent>().enabled)
            {
                transform.rotation = Quaternion.LookRotation(Vector3.RotateTowards(transform.forward, targetPos, 5f * Time.deltaTime, 0f));
                if (Vector3.Distance(transform.position, targetPos) < 1f)
                {
                    hasDestination = false;
                }
            }
            else
            {
                if (currentWaypoint < wayPoints.Count)
                {
                    transform.position = Vector3.MoveTowards(transform.position, wayPoints[currentWaypoint], speed * Time.deltaTime);
                    Vector3 targetDirection = wayPoints[currentWaypoint] - transform.position;
                    transform.rotation = Quaternion.LookRotation(Vector3.RotateTowards(transform.forward, targetDirection, 5f * Time.deltaTime, 0f));

                    if (Vector3.Distance(transform.position, wayPoints[currentWaypoint]) < 1f)
                    {
                        currentWaypoint += 1;
                    }
                }
                else
                {
                    hasDestination = false;
                    animController.SetBool("HasTarget", hasDestination);
                }
            }
        }
    }

    public bool GetNewDestination(float range, out Vector3 result)
    {
        for (int i = 0; i < 30; i++)
        {
            Vector3 randomPoint = transform.position + Random.insideUnitSphere * range;
            randomPoint.y = 0.35f;
            if (NavMesh.SamplePosition(randomPoint, out _, 1.0f, NavMesh.AllAreas))
            {
                result = randomPoint;
                hasDestination = true;
                animController.SetBool("HasTarget", hasDestination);
                return true; 
            }
        }
        result = Vector3.zero;
        Debug.Log("Could not find position");
        return false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Hole"))
        {
            Debug.Log("Caught");
            gameObject.GetComponent<NavMeshAgent>().enabled = false;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Hole"))
        {
            Debug.Log("Escaped");
            //gameObject.GetComponent<NavMeshAgent>().enabled = true;
            
        }
    }
}
