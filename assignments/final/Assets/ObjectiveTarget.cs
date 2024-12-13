using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectiveTarget : MonoBehaviour
{
    public UnitScript unit;

    private void OnTriggerEnter(Collider other)
    {
        
        if (other.CompareTag("taxi"))
        {
            //Debug.Log("IN DA ZOOOONE !!!");
            //if (GameManager.instance.selectedUnit != null)
            //{
                Debug.Log("COMPLETEEDDDDDDD MISSSION !!!!");
                unit.HandleCompletMission();
            //}
            /*else if (!GameManager.instance.missionActive)
            {
                GameManager.instance.StartMission(goalZone);
            }
            else if (other.gameObject == GameManager.instance.missionGoal)
            {
                GameManager.instance.CompleteMission();
            }*/
        }
    }
}
