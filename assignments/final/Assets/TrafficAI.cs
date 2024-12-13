using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class TrafficAI : MonoBehaviour
{
    public GameObject waypointsParent; // Parent GameObject holding all waypoints as children
    private List<Transform> waypoints = new List<Transform>(); // List to store the waypoints
    private NavMeshAgent agent; // NavMeshAgent to control the movement
    private int currentWaypointIndex = 0; // Index to track the current waypoint

    public GameObject explosionPrefab; // Prefab for the explosion effect
    public float screenShakeDuration = 0.5f; // Duration of the screen shake
    public float screenShakeMagnitude = 0.3f; // Intensity of the screen shake

    void Start()
    {
        // Get the NavMeshAgent component
        agent = GetComponent<NavMeshAgent>();

        // Populate the waypoints list from the children of the parent GameObject
        if (waypointsParent != null)
        {
            foreach (Transform child in waypointsParent.transform)
            {
                waypoints.Add(child);
            }
        }
        else
        {
            Debug.LogError("Waypoints parent GameObject is not assigned!");
        }

        // Choose an initial destination (closest waypoint relative to the current position)
        if (waypoints.Count > 0)
        {
            SetInitialDestination();
        }
        else
        {
            Debug.LogError("No waypoints found under the assigned parent GameObject!");
        }
    }

    void Update()
    {
        // Check if the NPC has reached its destination
        if (!agent.pathPending && agent.remainingDistance <= agent.stoppingDistance)
        {
            if (!agent.hasPath || agent.velocity.sqrMagnitude == 0f)
            {
                SetNextDestination();
            }
        }
    }

    void SetInitialDestination()
    {
        if (waypoints.Count == 0) return;

        // Find the closest waypoint to the agent's current position
        float minDistance = Mathf.Infinity;
        Transform closestWaypoint = null;

        foreach (Transform waypoint in waypoints)
        {
            float distance = Vector3.Distance(transform.position, waypoint.position);
            if (distance < minDistance)
            {
                minDistance = distance;
                closestWaypoint = waypoint;
            }
        }

        // Set the initial destination as the closest waypoint
        currentWaypointIndex = waypoints.IndexOf(closestWaypoint);
        agent.SetDestination(waypoints[currentWaypointIndex].position);
    }

    void SetNextDestination()
    {
        // Move to the next waypoint in the list
        currentWaypointIndex++;

        // If we've reached the last waypoint, start over
        if (currentWaypointIndex >= waypoints.Count)
        {
            currentWaypointIndex = 0;
        }

        // Set the new destination
        agent.SetDestination(waypoints[currentWaypointIndex].position);
    }

    private void OnCollisionEnter(Collision collision)
    {if (collision.gameObject.CompareTag("taxi"))
{
    Instantiate(explosionPrefab, transform.position, Quaternion.identity);

    // Trigger screen shake
    Camera.main.GetComponent<CameraFollow>()?.TriggerShake(screenShakeDuration, screenShakeMagnitude);

    Destroy(gameObject);
}

    }
}

