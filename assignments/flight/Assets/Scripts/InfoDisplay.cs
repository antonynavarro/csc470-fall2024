using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class InfoDisplay : MonoBehaviour
{
    //Tbh I asked ChatGPT for this code, I had no idea how to do it but it looked cool
    public TextMeshProUGUI speedText; 
    public TextMeshProUGUI altitudeText; 

    public float speedScale = 1f; 
    public float altitudeScale = 1f; 

    private Vector3 previousPosition; 
    private float updateInterval = 0.5f; 
    private float timeSinceLastUpdate = 0f; 

    // Start is called before the first frame update
    void Start()
    {
        // Initialize the previous position with the current position of the plane
        previousPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        // Update the timer with the time passed since the last frame
        timeSinceLastUpdate += Time.deltaTime;

        // Only update speed and altitude if the update interval has passed
        if (timeSinceLastUpdate >= updateInterval)
        {
            // Calculate speed and altitude
            float speed = CalculateSpeed();
            float altitude = CalculateAltitude();

            // Update the UI for speed and altitude
            UpdateSpeedUI(speed);
            UpdateAltitudeUI(altitude);

            // Reset the timer after the update
            timeSinceLastUpdate = 0f;
        }
    }

    // Function to calculate the speed of the plane
    private float CalculateSpeed()
    {
        // Calculate the distance traveled between frames
        float distance = Vector3.Distance(transform.position, previousPosition);
        // Divide by deltaTime to get speed (distance per second)
        float speed = distance / updateInterval;
        // Update the previous position to the current position for the next frame
        previousPosition = transform.position;

        // Return the speed adjusted by the speed scale
        return speed * speedScale;
    }

    // Function to calculate the altitude of the plane (Y-axis position)
    private float CalculateAltitude()
    {
        // Return the current Y position of the plane adjusted by the altitude scale
        return transform.position.y * altitudeScale;
    }

    // Function to update the speed display in the UI
    private void UpdateSpeedUI(float speed)
    {
        speedText.text = "Speed: " + Mathf.Round(speed).ToString() + " km/h"; // Example units
    }

    // Function to update the altitude display in the UI
    private void UpdateAltitudeUI(float altitude)
    {
        altitudeText.text = "Altitude: " + Mathf.Round(altitude).ToString() + " m"; // Example units
    }
}
