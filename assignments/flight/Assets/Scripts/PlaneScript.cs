using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlaneScript : MonoBehaviour
{
    public GameObject cameraObject;
    public GameObject jetPlane;
    public GameObject normalPlane;


    public float forwardspeed = 1f;
    public float xrotationSpeed = 0.6f;
    public float yrotationSpeed = 0.4f;
    public float zrotationSpeed = 1f;


    private float boostDuration = 3f; 
    private bool isBoosting = false;

    public int checkpointCount = 0;

    

    // Start is called before the first frame update
    void Start()
    {
        // Retrieve the selected plane from PlayerPrefs
        int selectedPlane = PlayerPrefs.GetInt("SelectedPlane", 1);

        if (selectedPlane == 1)
        {
            jetPlane.SetActive(true);
            normalPlane.SetActive(false);
        }
        else
        {
            jetPlane.SetActive(false);
            normalPlane.SetActive(true);
        }


    }

    void Update()
    {
        float vAxis = Input.GetAxis("Vertical"); 
        float hAxis = Input.GetAxis("Horizontal");

        
        float pitch = -vAxis * xrotationSpeed * Time.deltaTime;
        float yaw = hAxis * yrotationSpeed * Time.deltaTime;

        transform.Rotate(pitch, yaw, 0, Space.Self);

        // Simulate roll when turning (banking the plane based on yaw)
        float targetZRotation = hAxis * -15f; // Target roll angle based on horizontal input
        float currentZRotation = transform.localEulerAngles.z;

        // Convert from 0-360 to -180 to 180 range for better clamping
        if (currentZRotation > 180f) currentZRotation -= 360f;

        //Thanks ChatGPT
        float newZRotation = Mathf.Lerp(currentZRotation, targetZRotation, zrotationSpeed * 0.1f * Time.deltaTime);

        // Apply the new Z rotation (banking) back to the plane
        Vector3 currentRotation = transform.localEulerAngles;
        currentRotation.z = newZRotation;
        transform.localEulerAngles = currentRotation;

        
        transform.position += transform.forward * forwardspeed * Time.deltaTime;

        // Update camera to follow the plane
        cameraObject.transform.position = transform.position;
        Vector3 cameraPosition = cameraObject.transform.position;
        cameraPosition += -transform.forward * 20f;
        cameraPosition += Vector3.up * 2f;
        cameraObject.transform.position = cameraPosition;
        cameraObject.transform.LookAt(transform.position);
    }



    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("boost") && !isBoosting)
        {
            Debug.Log("Collision! Boost activated!");
            StartCoroutine(ApplyBoost());
            Destroy(other.gameObject);
        }
    }

    
    private IEnumerator ApplyBoost()
    {
        isBoosting = true;
        forwardspeed += 50f; 

        yield return new WaitForSeconds(boostDuration); 

        forwardspeed -= 50f; 
        isBoosting = false;

        Debug.Log("Boost deactivated.");
    }

}

