using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOverScreen : MonoBehaviour
{
    public Image blackImage; 
    public float fadeDuration = 2f; 
    public TextMeshProUGUI message;

    private void Start()
    {
        blackImage.gameObject.SetActive(false); 
    }

    
    public void TriggerGameOverScreen(string reason)
    {
        blackImage.gameObject.SetActive(true); 
        message.text = reason;
        StartCoroutine(FadeIn());

    }

    // Coroutine to gradually fade in the black image
    private IEnumerator FadeIn()
    {
        float elapsedTime = 0f;
        Color imgColor = blackImage.color;

        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            imgColor.a = Mathf.Clamp01(elapsedTime / fadeDuration); // Gradually increase alpha
            blackImage.color = imgColor;
            yield return null;
        }

        // Ensure the alpha is fully set to 1 at the end
        imgColor.a = 1f;
        blackImage.color = imgColor;
    }

    public void RestartGame()
    {
        // Reload the current scene to restart the game
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
