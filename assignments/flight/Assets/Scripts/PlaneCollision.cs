using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaneCollision : MonoBehaviour
{
    public GameObject explosionPrefab; 
    public GameOverScreen gameOverScreen;
    public VictoryScreen VictoryScreen;
    public TimerScript timerScript;
    public PlaneScript planeScript;

    
    private void OnCollisionEnter(Collision collision)
    {
       
        if (collision.gameObject.CompareTag("terrain"))
        {
            // Instantiate the explosion effect at the plane's position and rotation
            Instantiate(explosionPrefab, transform.position, transform.rotation);

            // Destroy the plane
            Destroy(gameObject);

            gameOverScreen.TriggerGameOverScreen("L Pilot \nYou crashed");
        }

        // Victory: Plane collides with trophy
        if (collision.gameObject.CompareTag("trophy"))
        {
            //string finalTime = timerScript.timeElapsed;
            VictoryScreen.TriggerVictoryScreen(planeScript.checkpointCount, "1:12");
            Debug.Log(planeScript.checkpointCount);
        }
    }
}
