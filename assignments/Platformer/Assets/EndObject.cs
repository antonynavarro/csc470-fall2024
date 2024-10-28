using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndObject : MonoBehaviour
{
    public Timer timer; 

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player reached the end!");

            // Stop the timer
            if (timer != null)
            {
                timer.StopTimer();
                Debug.Log("Timer stopped at: " + timer.GetElapsedTime() + " seconds.");
            }
            else
            {
                Debug.LogWarning("TimerManager reference not set on EndObject.");
            }
        }
    }
}
