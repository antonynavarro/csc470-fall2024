using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform ball;  // Assign the ball object in the Unity Inspector
    private Vector3 offset;

    void Start()
    {
        // Calculate the initial offset between the camera and the ball
        offset = transform.position - ball.position;
    }

    void LateUpdate()
    {
        // Follow the ball's y position while maintaining the initial x and z offsets
        transform.position = new Vector3(transform.position.x, ball.position.y + offset.y, transform.position.z);
    }
}
