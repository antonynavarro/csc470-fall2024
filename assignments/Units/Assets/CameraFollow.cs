using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform car;          // Assign your car's transform here
    public Vector3 offset;         // Set an offset to position the camera correctly
    public float scrollSensitivity = 10f; // Sensitivity for adjusting the camera height
    public float minHeight = 30f;  // Minimum camera height
    public float maxHeight = 150f; // Maximum camera height
    public float minZoom = -50f;   // Minimum zoom distance (closer to the car)
    public float maxZoom = -200f;  // Maximum zoom distance (further from the car)

    private float smoothSpeed = 0.1f; // Smoothing factor for height and zoom
    private float targetHeight;
    private float targetZoom;

    private void Start()
    {
       

        // Set initial target height and zoom based on the starting offset
        targetHeight = offset.y;
        targetZoom = offset.z;
    }

    private void LateUpdate()
    {
        // Get the scroll input
        float scroll = Input.GetAxis("Mouse ScrollWheel");

        // Scroll for zoom (increasing or decreasing zoom)
        targetZoom = Mathf.Clamp(targetZoom + scroll * scrollSensitivity, minHeight, maxHeight);

        // Scroll for height (moving the camera up/down)
        targetHeight = Mathf.Clamp(targetHeight - scroll * scrollSensitivity, minHeight, maxHeight);

        // Smoothly adjust the camera's offset based on the scroll direction
        offset.y = Mathf.Lerp(offset.y, targetHeight, smoothSpeed);
        //offset.z = Mathf.Lerp(offset.y, targetZoom, smoothSpeed);

        // Set the camera's position based on the car's position and the offset
        transform.position = car.position + offset;

        // Set the camera to look at the car
        transform.rotation = Quaternion.Euler(80, 180, 0); // Adjust the rotation as needed
    }
}
