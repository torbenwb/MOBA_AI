using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class Fireball : MonoBehaviour, ITask, IAbility
{
    LineRenderer lineRenderer;
    NavMover navMover;
    public bool request = false;
    bool lastRequest = false;
    bool cooldownActive;
    public GameObject projectilePrefab;

    public bool Request{
        set => request = value;
    }

    void Awake()
    {
        navMover = GetComponent<NavMover>();
        lineRenderer = GetComponent<LineRenderer>();
    }

    public bool Evaluate(){
        if (request && !cooldownActive)
        {
            navMover.StopMove();
            Vector3 targetDirection = (PlayerController.MouseTargetPoint() - transform.position).normalized;
            targetDirection.y = 0f;
            lineRenderer.positionCount = 2;
            lineRenderer.SetPosition(0,transform.position);
            lineRenderer.SetPosition(1, transform.position + targetDirection * 2f);
            lastRequest = true;
            return true;
        }
        else if (lastRequest)
        {
            lastRequest = false;
            Execute();
            return true;
        }
        else
        {
            lineRenderer.positionCount = 0;
            lastRequest = false;
            return false;
        }
    }

    public void Execute()
    {
        GameObject newProjectile = Instantiate(projectilePrefab, transform.position, Quaternion.identity);
        newProjectile.layer = gameObject.layer;
        Vector3 targetDirection = (PlayerController.MouseTargetPoint() - transform.position);
        targetDirection.y = 0f;
        targetDirection.Normalize();
        newProjectile.GetComponent<Projectile>().Forward = targetDirection;
        StartCoroutine(Cooldown(5f));
    }

    public IEnumerator Cooldown(float cooldownTime){
        cooldownActive = true;
        yield return new WaitForSeconds(cooldownTime);
        cooldownActive = false;
    }
}
