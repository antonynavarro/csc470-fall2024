using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatformScript : MonoBehaviour
{
    public enum MoveDirection { X, Z }       // Enum to choose between X or Z direction
    public MoveDirection direction = MoveDirection.X;  // Default to X-axis movement
    public float moveSpeed = 1f;             
    public float freq = 1f;                  
    public float amp = 2f;                  

    private Vector3 startPosition;
    private float offset;

    void Start()
    {
        startPosition = transform.position;
        offset = Random.Range(0, Mathf.PI * 2);  
    }

    void Update()
    {
        Vector3 pos = startPosition;

        if (direction == MoveDirection.X)
        {
            // Move along X-axis
            pos += Vector3.right * Mathf.Sin((offset + Time.time * freq) * moveSpeed) * amp;
        }
        else if (direction == MoveDirection.Z)
        {
            // Move along Z-axis
            pos += Vector3.forward * Mathf.Sin((offset + Time.time * freq) * moveSpeed) * amp;
        }

        transform.position = pos;
    }
}
