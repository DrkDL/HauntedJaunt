using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI; // namespace for NavMesh agent scripting

public class WaypointPatrol : MonoBehaviour
{
    public NavMeshAgent navMeshAgent;
    public Transform[] waypoints;

    int currentWaypointIndex;
    // Start is called before the first frame update
    void Start()
    {
        // initialize destination of the agent
        navMeshAgent.SetDestination(waypoints[0].position);
    }

    // Update is called once per frame
    void Update()
    {
        if (navMeshAgent.remainingDistance < navMeshAgent.stoppingDistance)
        {
            // make sure current index keeps increasing and loops back to 0
            currentWaypointIndex = (currentWaypointIndex + 1) % waypoints.Length;
            // move the agent to the next destination
            navMeshAgent.SetDestination(waypoints[currentWaypointIndex].position);
        }
    }
}
