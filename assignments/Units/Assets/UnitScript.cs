using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitScript : MonoBehaviour
{

    public GameObject character;

    public string unitName;
    public string mission;
    public string mood;
    public int reward;

    public GameObject greenTarget;
    public GameObject ObjectiveTarget;

    public UnityEngine.AI.NavMeshAgent agent;

    public Renderer bodyRenderer;
    public Color normalColor;
    public Color selectedColor;
    public bool selected = false;

    private void Start()
    {
        GameManager.instance.units.Add(this);
        GameManager.instance.OnUnitSelected += HandleUnitSelected;
        GameManager.instance.OnMissionStarted += HandleMissionStarted;
    }

    private void OnDestroy()
    {
        GameManager.instance.units.Remove(this);
    }

    public void UnitMission()
    {
        GameManager.instance.SelectUnit(this);
        GameManager.instance.OpenPopUpWindow(this);
    }

    public void HandleCompletMission()
    {
        GameManager.instance.CompleteMission(this);
        
    }

    private void HandleUnitSelected(UnitScript unit)
    {
        if (unit == this)
        {
            Debug.Log($"Unit {unitName} selected.");
        }
    }

    private void HandleMissionStarted(UnitScript unit)
    {
        if (unit == this)
        {
            Debug.Log($"Mission started for unit: {unitName}");
            character.SetActive(false);
        }
    }
}
