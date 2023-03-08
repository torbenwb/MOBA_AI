using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class FollowCamera : MonoBehaviour
{
    public bool followTargetPosition = false;
    public Vector3 targetPosition;
    public Vector3 moveDirection;

    public float yaw;
    public float pitch;
    public float cameraDistance;
    public float FOV;
    public Vector3 origin;
    public float moveSpeed = 15f;

    public Vector3 MoveDirection
    {
        get => moveDirection;
        set{
            moveDirection = value.normalized;

            if (moveDirection.magnitude > 0) followTargetPosition = false;
        }
    }

    public Vector3 TargetPosition
    {
        set{
            targetPosition = value;
            followTargetPosition = true;
        }
    }

    private void LateUpdate()
    {
        if (followTargetPosition)
        {
            origin = Vector3.MoveTowards(origin, targetPosition, (targetPosition - origin).magnitude * Time.deltaTime);
        }
        else{
            origin = Vector3.MoveTowards(origin, origin + MoveDirection, moveSpeed * Time.deltaTime);
        }
        
        Quaternion rotation = Quaternion.Euler(pitch, yaw, 0f);
        Vector3 p = origin - (rotation * Vector3.forward) * cameraDistance;
        Camera.main.transform.position = p;
        Camera.main.transform.rotation = rotation;
        Camera.main.fieldOfView = FOV;
    }
}
