using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class CarController : MonoBehaviour
{
    public float maxForwardSpeed = 10f; // Maximum forward speed
    public float maxReverseSpeed = 5f; // Maximum reverse speed
    public float acceleration = 5f; // How quickly the car accelerates
    public float deceleration = 10f; // How quickly the car decelerates
    public float brakingForce = 15f; // How quickly the car brakes
    public float turnSpeed = 100f; // Speed for turning the car

    private CarScript carScript;
    private NavMeshAgent agent;
    public HealthBar healthBar;
    private float currentSpeed = 0f; // The current speed of the car
    private float targetSpeed = 0f; // The desired speed based on input
    private float inputHorizontal;
    private float inputVertical;
    private bool isBraking = false;

    


    void Start()
    {
        // Get the NavMeshAgent component
        agent = GetComponent<NavMeshAgent>();

        // Disable NavMeshAgent automatic movement
        agent.updatePosition = false;
        agent.updateRotation = false;

        // Get the CarScript component
        carScript = GetComponent<CarScript>();

        healthBar = FindObjectOfType<HealthBar>();
        if (carScript != null)
        {
            healthBar.SetMaxHealth(carScript.MaxHealth);
        }

    }

    void Update()
    {
        // Only process input and movement if this car is selected
        if (carScript != null && carScript.selected)
        {
            if (CarManager.instance.IsinMenu == false)
            {
                //Debug.Log("CANNOT MOVEEE");
                HandleInput();
                UpdateSpeed();
                MoveCar();
            }
            
        }
    }
    void HandleInput()
    {
        
       
        inputHorizontal = Input.GetAxis("Horizontal");
        inputVertical = Input.GetAxis("Vertical"); 

        // Check if the car is braking
        if ((currentSpeed > 0 && inputVertical < 0) || (currentSpeed < 0 && inputVertical > 0))
        {
            isBraking = true;
        }
        else
        {
            isBraking = false;
        }

        // Set target speed based on input if not braking
        if (!isBraking)
        {
            if (inputVertical > 0)
            {
                targetSpeed = maxForwardSpeed * inputVertical;
            }
            else if (inputVertical < 0)
            {
                targetSpeed = maxReverseSpeed * inputVertical;
            }
            else
            {
                targetSpeed = 0f; // Stop the car when no vertical input
            }
        }
    }

    void UpdateSpeed()
    {
        if (isBraking)
        {
            // Apply braking force
            currentSpeed = Mathf.MoveTowards(currentSpeed, 0, brakingForce * Time.deltaTime);
        }
        else
        {
            // Accelerate or decelerate toward the target speed
            if (Mathf.Abs(targetSpeed) > Mathf.Abs(currentSpeed))
            {
                currentSpeed = Mathf.MoveTowards(currentSpeed, targetSpeed, acceleration * Time.deltaTime);
            }
            else
            {
                currentSpeed = Mathf.MoveTowards(currentSpeed, targetSpeed, deceleration * Time.deltaTime);
            }
        }
    }

    void MoveCar()
    {
        // Apply rotation only when the car is moving
        if (currentSpeed != 0)
        {
            float rotation = inputHorizontal * turnSpeed * Time.deltaTime;
            transform.Rotate(0, rotation, 0);
        }

        // Calculate forward movement
        Vector3 movement = transform.forward * currentSpeed * Time.deltaTime;

        // Move the agent on the NavMesh
        if (NavMesh.SamplePosition(transform.position + movement, out NavMeshHit hit, 1f, NavMesh.AllAreas))
        {
            agent.nextPosition = hit.position; // Align to NavMesh
            transform.position = hit.position; // Move the car
        }
        else
        {
            // If NavMesh is invalid, stop movement
            currentSpeed = 0f;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("traffic"))
        {
            Debug.Log($"{carScript.carName} collided with traffic!");
            carScript.TakeDamage(10);
            healthBar.Damage(10);
            
        }
    }
}
