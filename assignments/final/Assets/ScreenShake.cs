using System.Collections;
using UnityEngine;

public class ScreenShake : MonoBehaviour
{
    private Vector3 originalPosition;

    private void Start()
    {
        // Save the initial position of the camera
        originalPosition = transform.localPosition;
    }

    /// <summary>
    /// Triggers the screen shake effect.
    /// </summary>
    public void TriggerShake(float duration, float magnitude)
    {
        StartCoroutine(Shake(duration, magnitude));
    }

    private IEnumerator Shake(float duration, float magnitude)
    {
        Debug.Log("SCREEEENN SHAKEKEKEKE");
        float elapsed = 0.0f;

        while (elapsed < duration)
        {
            // Generate random offset for the shake
            float offsetX = Random.Range(-1f, 1f) * magnitude;
            float offsetY = Random.Range(-1f, 1f) * magnitude;

            // Apply the offset
            transform.localPosition = originalPosition + new Vector3(offsetX, offsetY, 0);

            // Wait for the next frame
            elapsed += Time.deltaTime;
            yield return null;
        }

        // Restore the camera's original position
        transform.localPosition = originalPosition;
    }
}

