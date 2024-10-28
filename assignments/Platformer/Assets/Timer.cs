using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Timer : MonoBehaviour
{
    public TextMeshProUGUI timerText; 
    private float timer = 0f; 
    private bool isTimerRunning = true; 

    void Update()
    {
        if (isTimerRunning)
        {
            timer += Time.deltaTime; 
            DisplayTime(timer);
        }
    }

    private void DisplayTime(float timeToDisplay)
    {

        // Format the time to show minutes, seconds, and milliseconds
        int minutes = Mathf.FloorToInt(timeToDisplay / 60);
        int seconds = Mathf.FloorToInt(timeToDisplay % 60);
        int milliseconds = Mathf.FloorToInt((timeToDisplay * 1000) % 1000);

        timerText.text = string.Format("{0:00}:{1:00}:{2:000}", minutes, seconds, milliseconds);
    }

    // Method to start the timer
    public void StartTimer()
    {
        isTimerRunning = true;
    }

    // Method to stop the timer
    public void StopTimer()
    {
        isTimerRunning = false;
        Debug.Log("Timer stopped at: " + timer.ToString("F2") + " seconds.");
    }

    // Optional: Method to reset the timer if needed
    public void ResetTimer()
    {
        timer = 0f;
        DisplayTime(timer);
    }

    public float GetElapsedTime()
    {
        return timer;
    }

}
