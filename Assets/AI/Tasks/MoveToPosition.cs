using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(NavMover))]
public class MoveToPosition : MonoBehaviour, ITask
{
    NavMover navMover;
    public bool destinationSet = false;
    Vector3 destination;
    public bool destinationReached = false;

    public Vector3 Destination
    {
        set{
            destination = value;
            destinationSet = true;
        }
    }

    void Awake() => navMover = GetComponent<NavMover>();

    public bool Evaluate()
    {
        if (!destinationSet) return false;

        navMover.Destination = destination;
        destinationReached = navMover.DestinationReached(destination);

        if (destinationReached) destinationSet = false; 

        return destinationSet;
    }
}
