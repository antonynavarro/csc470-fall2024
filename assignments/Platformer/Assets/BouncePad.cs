using UnityEngine;

public class BouncePad : MonoBehaviour
{
    public float bounceForce = 15f; 

    void OnTriggerEnter(Collider other)
    {
        
        PlatformController player = other.GetComponent<PlatformController>();
        if (player != null)
        {
            player.ApplyBounce(bounceForce);
        }
    }
}

