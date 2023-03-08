using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public GameObject target;
    public float moveSpeed;
    public float rotationRate;
    

    public Vector3 TargetPosition
    {
        set
        {
            Quaternion targetRotation = Quaternion.LookRotation(value - transform.position);
            transform.rotation = targetRotation;
        }
    }

    public Vector3 Forward
    {
        set
        {
            TargetPosition = transform.position + value;
        }
    }

    private void Update()
    {
        if (target)
        {
            Quaternion targetRotation = Quaternion.LookRotation(target.transform.position - transform.position);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationRate * Time.deltaTime);
        }

        transform.position += transform.forward * moveSpeed * Time.deltaTime;
    }
}
