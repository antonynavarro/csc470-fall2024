using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformController : MonoBehaviour
{
    public float coyoteTime = 0.2f;
    public float jumpBufferTime = 0.2f;

    private float coyoteTimeCounter;
    private float jumpBufferCounter;

    public CharacterController cc;
    public Transform playerCamera;

    public float groundMoveSpeed = 10f;
    public float airControlFactor = 0.5f;
    public float jumpVelocity = 6f;

    private float yVelocity = 0f;
    public float gravity = -12f;
    public float groundCheckOffset = -2f;

    public float xRotation = 0f;
    public float lookSpeed = 2f;

    private Vector3 velocity;
    private GameObject movingPlatform;
    private Vector3 previousMovingPlatformPosition;

    private bool isBouncing = false;  // New flag to track if the player is bouncing

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        LookAround();
        MovePlayer();
        Debug.Log("Is BOUNCING ???" + isBouncing);
    }

    void LookAround()
    {
        float mouseX = Input.GetAxis("Mouse X") * lookSpeed;
        float mouseY = Input.GetAxis("Mouse Y") * lookSpeed;

        transform.Rotate(Vector3.up * mouseX);

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);
        playerCamera.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
    }

    void MovePlayer()
    {
        float hAxis = Input.GetAxis("Horizontal");
        float vAxis = Input.GetAxis("Vertical");

        Vector3 direction = transform.forward * vAxis + transform.right * hAxis;

        if (cc.isGrounded)
        {
            // Reset coyote time and velocity for grounded state
            coyoteTimeCounter = coyoteTime;
            yVelocity = groundCheckOffset;
            velocity = direction * groundMoveSpeed;

            // Stop bouncing when grounded
            if (isBouncing)
            {
                isBouncing = false;
                Debug.Log("Is BOUNCING ??? " + isBouncing);  // Confirm bounce ended
            }

            // Allow jump only if not bouncing
            if (!isBouncing && jumpBufferCounter > 0)
            {
                yVelocity = jumpVelocity;
                jumpBufferCounter = 0;
            }
            else if (!isBouncing && Input.GetKeyDown(KeyCode.Space))
            {
                yVelocity = jumpVelocity;
            }
        }
        else
        {
            // Apply gravity when not grounded
            yVelocity += gravity * Time.deltaTime;

            // Update coyote time countdown
            coyoteTimeCounter -= Time.deltaTime;

            // Jump buffer if not bouncing
            if (!isBouncing && coyoteTimeCounter > 0 && Input.GetKeyDown(KeyCode.Space))
            {
                yVelocity = jumpVelocity;
                coyoteTimeCounter = 0;
            }

            if (!isBouncing && Input.GetKeyDown(KeyCode.Space))
            {
                jumpBufferCounter = jumpBufferTime;
            }

            // Cancel jump if space is released
            if (yVelocity > 0 && Input.GetKeyUp(KeyCode.Space) && !isBouncing)
            {
                yVelocity = 0;
            }

            // Adjust velocity when moving in the air
            if (hAxis != 0 || vAxis != 0)
            {
                velocity = direction * groundMoveSpeed * airControlFactor;
            }
        }

        // Decrease jump buffer countdown
        if (jumpBufferCounter > 0)
        {
            jumpBufferCounter -= Time.deltaTime;
        }

        Vector3 move = velocity + Vector3.up * yVelocity;

        // Handle moving platform adjustments
        if (movingPlatform != null)
        {
            Vector3 platformMovement = movingPlatform.transform.position - previousMovingPlatformPosition;
            move += platformMovement / Time.deltaTime;
            previousMovingPlatformPosition = movingPlatform.transform.position;
        }

        cc.Move(move * Time.deltaTime);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("MovingPlatform"))
        {
            movingPlatform = other.gameObject;
            previousMovingPlatformPosition = movingPlatform.transform.position;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("MovingPlatform"))
        {
            movingPlatform = null;
        }
    }

    public void ApplyBounce(float bounceForce)
    {
        yVelocity = bounceForce;
        isBouncing = true;  // Set bounce flag to true when applying bounce
    }
}
