using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GreenTarget : MonoBehaviour
{
    public UnitScript unit;
    public GameObject goalZone;

    private void OnTriggerEnter(Collider other)
    {
       
        if (other.CompareTag("taxi"))
        {
            //Debug.Log("IN DA ZOOOONE !!!");
            if (GameManager.instance.selectedUnit == null)
            {
                Debug.Log("MISSSION !!!!");
                unit.UnitMission();
            }
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
