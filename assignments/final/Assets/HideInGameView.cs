using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HideInGameView : MonoBehaviour
{
    void Start()
    {
        // Disable the renderer so the object is not visible in the Game view
        Renderer renderer = GetComponent<Renderer>();
        if (renderer != null)
        {
            renderer.enabled = false;
        }

        // Optionally, disable Collider or other gameplay-related components
        Collider collider = GetComponent<Collider>();
        if (collider != null)
        {
            collider.enabled = false;
        }
    }

    void OnDrawGizmos()
    {
        // Draw something in the Scene view to represent the object
        Gizmos.color = Color.yellow;
        Gizmos.DrawSphere(transform.position, 0.5f);
    }
}
