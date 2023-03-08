using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class NavMover : MonoBehaviour
{
    NavMeshAgent agent;
    private Vector3 destination;

    public Vector3 Destination
    {
        get => destination;
        set{
            StartMove();
            destination = value;
            agent.destination = value;
        }
    }

    public float Speed
    {
        get => agent.velocity.magnitude / agent.speed;
    }
    
    public bool DestinationReached(Vector3 position, float stopRange = 1f)
    {
        if (position == destination)
        {
            if (agent.remainingDistance <= stopRange) return true;
        }
        return false;
    }

    public void StopMove()
    {
        agent.isStopped = true;
    }

    public void StartMove()
    {
        agent.isStopped = false;
    }
    
    void Awake() => agent = GetComponent<NavMeshAgent>();
}
