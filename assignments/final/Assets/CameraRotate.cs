using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRotate : MonoBehaviour
{
    public float rotationSpeed = 10f;

    void Update()
    {
        // Get the current rotation
        Vector3 currentRotation = transform.eulerAngles;

        // Modify only the Y-axis
        currentRotation.y += rotationSpeed * Time.deltaTime;

        // Apply the new rotation back
        transform.eulerAngles = currentRotation;
    }
}
