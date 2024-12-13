using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class TaxiScript : MonoBehaviour
{

    public Camera mainCamera;
    LayerMask layerMask;

    public UnityEngine.AI.NavMeshAgent agent;

    void Start()
    {
        layerMask = LayerMask.GetMask("ground");
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            // Check if the mouse is over a UI element
            if (EventSystem.current.IsPointerOverGameObject())
            {
                return; // Exit if the click was on a UI element
            }

            Ray mousePositionRay = mainCamera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hitInfo;
            if (Physics.Raycast(mousePositionRay, out hitInfo, Mathf.Infinity, layerMask))
            {
                agent.SetDestination(hitInfo.point);
            }
        }
    }
   
}
