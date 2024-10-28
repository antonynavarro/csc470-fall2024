using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GateTrigger : MonoBehaviour
{
    public LavaRise lavaRise; 

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player passed through the gate!");

            if (lavaRise != null)
            {
                lavaRise.StartRising(); // start the lava rising
                Debug.Log("Lava rising activated!"); 
            }
            else
            {
                Debug.LogWarning("LavaRise reference not set on GateTrigger.");
            }
        }
    }
}
