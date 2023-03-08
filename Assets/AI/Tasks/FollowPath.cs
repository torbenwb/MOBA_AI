using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(NavMover))]
public class FollowPath : MonoBehaviour, ITask
{
    NavMover navMover;
    public NavPath path;
    public int pathIndex;
    public bool followReverse = false;

    void Awake() => navMover = GetComponent<NavMover>();

    public bool Evaluate()
    {
        if (!path) return false;
        
        float distance = (path[pathIndex] - transform.position).magnitude;
        if (distance <= 2f) pathIndex = path.NextIndex(followReverse ? --pathIndex : ++pathIndex);

        navMover.Destination = path[pathIndex];
        return true;
    }
}
