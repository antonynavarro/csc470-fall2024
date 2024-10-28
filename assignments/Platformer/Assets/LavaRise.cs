using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class LavaRise : MonoBehaviour
{
    public float riseSpeed = 1.0f;
    public bool shouldRise = false;

    void Update()
    {
        if (shouldRise)
        {
            transform.position += Vector3.up * riseSpeed * Time.deltaTime;
        }
    }

    public void StartRising() 
    {
        shouldRise = true;
        Debug.Log("Lava started rising.");
    }

    private void OnTriggerEnter(Collider other)
    {
        
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player touched the lava!");

            
            PlatformController playerController = other.GetComponent<PlatformController>();
            if (playerController != null)
            {
                playerController.enabled = false;
            }

            
            StartCoroutine(RestartSceneAfterDelay(2f));
        }
    }

    private IEnumerator RestartSceneAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}

