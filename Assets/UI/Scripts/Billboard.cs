using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Billboard : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        Vector3 direction = -(Camera.main.transform.position - transform.position).normalized;
        transform.rotation = Quaternion.LookRotation(direction);
    }
}
