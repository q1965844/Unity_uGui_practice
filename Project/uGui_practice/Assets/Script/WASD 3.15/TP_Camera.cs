using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TP_Camera : MonoBehaviour
{
    public bool lockCursor;
    public float mouseSensitivity = 5;
    public Transform target;
    public float dstFromTarget = 2;
    public Vector3 TargetOffset = new Vector3();
    public Vector2 pitchMinMax = new Vector2(-40, 85);

    public float rotationSmootTime = .12f;
    Vector3 rotationSmoothVelocity;
    Vector3 CurrentRotation;

    float yaw;
    float pitch;

    void Start()
    {
        if (lockCursor)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
    }

    
    void LateUpdate()
    {
        yaw += Input.GetAxis("Mouse X") * mouseSensitivity;
        pitch -= Input.GetAxis("Mouse Y") * mouseSensitivity;
        pitch = Mathf.Clamp(pitch, pitchMinMax.x, pitchMinMax.y);

        CurrentRotation = Vector3.SmoothDamp(CurrentRotation, new Vector3(pitch, yaw), ref rotationSmoothVelocity, rotationSmootTime);
        transform.eulerAngles = CurrentRotation;

        transform.position = (target.position + TargetOffset) - transform.forward * dstFromTarget;
    }
}
