using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class AttackTarget : MonoBehaviour, ITask
{
    NavMover navMover;
    public GameObject target;
    public float attackRange = 2f;
    public float searchRadius = 10f;
    public int damage = 1;
    public bool autoFindTarget = true;

    void Awake() => navMover = GetComponent<NavMover>();

    public bool Evaluate()
    {
        target = !target ? (autoFindTarget ? FindTarget() : null) : target;
        if (!target) return false;

        
        if (TargetInRange())
        {
            if (navMover) navMover.StopMove();
            DamageTarget();
        }
        else{
            if (navMover) navMover.Destination = target.transform.position;
        }

        return true;
    }

    public void DamageTarget()
    {
        target.GetComponent<IDamageable>().Damage(damage);
    }

    bool TargetInRange()
    {
        if (!target) return false;

        Vector3 start = transform.position;
        Vector3 end = target.transform.position;
        RaycastHit[] hits = Physics.RaycastAll(start, (end - start).normalized, attackRange);

        for(int i = 0; i < hits.Length; i++)
        {
            if (hits[i].collider.gameObject == target) return true;
        }
        
        return false;
    }

    GameObject FindTarget()
    {
        int layerMask;
        if (LayerMask.LayerToName(gameObject.layer) == "Team 1") layerMask = LayerMask.GetMask("Team 2");
        else layerMask = LayerMask.GetMask("Team 1");

        RaycastHit[] hits = Physics.SphereCastAll(transform.position, searchRadius, Vector3.up, 1f, layerMask);
        float minDistance = 100;
        GameObject newTarget = null;
        foreach(RaycastHit hit in hits)
        {
            if ((hit.collider.gameObject.GetComponent<IDamageable>()) == null) continue;

            float distance = (hit.point - transform.position).magnitude;
            if (distance <= minDistance)
            {
                minDistance = distance;
                newTarget = hit.collider.gameObject;
            }
        }

        return newTarget; 
    }
}
