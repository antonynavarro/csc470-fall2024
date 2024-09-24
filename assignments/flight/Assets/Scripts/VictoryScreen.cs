using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine.UI;
using UnityEngine;

public class VictoryScreen : MonoBehaviour
{
    public Image victoryImage; 
    public TextMeshProUGUI messageText; 
    public TextMeshProUGUI timerText; 
    public float fadeDuration = 2f; 
    public PlaneScript planeScript;
    private int checkpointsPassed; 
    public TimerScript timerScript;

    private string timerValue;
    private void Start()
    {
        
        Color imgColor = victoryImage.color;
        imgColor.a = 0f;
        victoryImage.color = imgColor;
        victoryImage.gameObject.SetActive(false); 
    }

    
    public void TriggerVictoryScreen(int checkpoints, string finalTime)
    {
        checkpointsPassed = checkpoints; 
        timerValue = timerScript.GetFormattedTime();

        victoryImage.gameObject.SetActive(true);
        StartCoroutine(FadeIn());
    }

    // Coroutine to gradually fade in the victory screen
    private IEnumerator FadeIn()
    {
        float elapsedTime = 0f;
        Color imgColor = victoryImage.color;

        // Stop the plane
        planeScript.forwardspeed = 0f;

        // Display the message based on checkpoints
        if (checkpointsPassed < 3)
        {
            messageText.text = "So, you just  don't  give a shit\nabout  the checkpoints don't  you";
        }
        else if (checkpointsPassed >= 3 && checkpointsPassed < 25)
        {
            messageText.text = "Not bad\nYou missed some checkpoints tho.";
        }
        else
        {
            messageText.text = "Amazing! You passed all checkpoints!";
        }

        
        timerText.text = "Time: " + timerValue;

        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            imgColor.a = Mathf.Clamp01(elapsedTime / fadeDuration); // Gradually increase alpha
            victoryImage.color = imgColor;
            yield return null;
        }
        imgColor.a = 1f;
        victoryImage.color = imgColor;
    }
}
