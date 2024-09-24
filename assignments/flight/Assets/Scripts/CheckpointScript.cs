using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CheckpointScript : MonoBehaviour
{
    public TextMeshProUGUI checkpointText;
    //public int checkpointCount = 0; 
    private PlaneScript planeScript;

    // When the plane enters the checkpoint
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("plane")) 
        {

            // Find the PlaneScript component on the object that collided (the plane)
            planeScript = other.GetComponent<PlaneScript>();

            if (planeScript != null)
            {
                
                planeScript.checkpointCount++;
                checkpointText.text = "Checkpoint: " + planeScript.checkpointCount.ToString() + "/25";

                // Destroy this checkpoint after it's passed
                Destroy(gameObject);
            }

        }
    }
}
