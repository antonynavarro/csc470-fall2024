using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using Unity.VisualScripting;
using static UnityEngine.UI.CanvasScaler;

public class GameManager : MonoBehaviour
{

    public static GameManager instance;

    public Camera mainCamera;

    public UnitScript selectedUnit;
    public List<UnitScript> units = new List<UnitScript>();

    //Mission Pop Up
    public GameObject popUpWindow;
    public GameObject InfoWindow;
    public GameObject InfoUI;
    public GameObject moneyUI;

    public TMP_Text moneyUIText;

    public TMP_Text nameText;
    public TMP_Text taskText;
    public TMP_Text rewardText;
    public Image portraitImage;

    //InfoPopUp
    public TMP_Text InfoPopUpnameText;
    public TMP_Text InfoPopUptaskText;
    public TMP_Text InfoPopUprewardText;



    private LayerMask layerMask;

    private bool missionActive = false;
    private GameObject missionGoal;

    public delegate void UnitEvent(UnitScript unit);
    public event UnitEvent OnUnitSelected;
    public event UnitEvent OnMissionStarted;

    public int money;

    private void OnEnable()
    {
        if (GameManager.instance == null)
        {
            GameManager.instance = this;
        }
        else
        {
            Destroy(this);
        }
    }

    void Start()
    {
        layerMask = LayerMask.GetMask("ground");
        money = 0;
        UpdateMoneyUI();
    }

    public void SelectUnit(UnitScript unit)
    {
        if (missionActive)
        {
            Debug.Log("Cannot select a new unit while a mission is active.");
            return;
        }

        foreach (UnitScript u in units)
        {
            u.selected = false;
            u.bodyRenderer.material.color = u.normalColor;
        }

        selectedUnit = unit;
        unit.selected = true;
        unit.bodyRenderer.material.color = unit.selectedColor;

        OnUnitSelected?.Invoke(unit);
    }

    public void DeselectUnit()
    {
        
        
        // Reset the selected unit's state
        selectedUnit.selected = false;
        selectedUnit.bodyRenderer.material.color = selectedUnit.normalColor;

        // Clear the reference to the selected unit
        selectedUnit = null;

        Debug.Log("Unit deselected.");
        

        
    }

    public void StartMission()
    {
        if (missionActive)
        {
            Debug.Log("Mission already active!");
            return;
        }

        if (selectedUnit == null)
        {
            Debug.Log("No unit selected to start the mission.");
            return;
        }

        missionActive = true;
        popUpWindow.SetActive(false);
        InfoUI.SetActive(true);
        moneyUI.SetActive(true);

        // Deactivate greenTarget for all units except the selected one
        foreach (UnitScript unit in units)
        {
            if (unit != selectedUnit && unit.greenTarget != null)
            {
                unit.greenTarget.SetActive(false);
            }
        }

        selectedUnit.greenTarget.SetActive(false);
        selectedUnit.ObjectiveTarget.SetActive(true);


        OnMissionStarted?.Invoke(selectedUnit);
    }

    public void CompleteMission(UnitScript unit)
    {
        if (unit == null)
        {
            Debug.LogError("No unit provided to CompleteMission.");
            return;
        }

        Debug.Log($"Mission completed for unit: {unit.unitName}");
        missionActive = false;
        missionGoal = null;

        // Destroy the unit's targets
        if (unit.greenTarget != null)
        {
            Destroy(unit.greenTarget);
        }
        if (unit.ObjectiveTarget != null)
        {
            Destroy(unit.ObjectiveTarget);
        }

        // Deactivate greenTarget for all units except the selected one
        foreach (UnitScript otherUnit in units)
        {
            
                otherUnit.greenTarget.SetActive(true);  
            
        }

        // Add the unit's reward to money
        money += unit.reward;
        UpdateMoneyUI();

        // Close the popup window and reset the selected unit
        ClosePopUpWindow();

        // Destroy the unit itself
        units.Remove(unit); // Remove it from the units list
        Destroy(unit.gameObject); // Destroy the GameObject

        InfoUI.SetActive(false);
    }


    public void OpenPopUpWindow(UnitScript unit)
    {
        if (missionActive)
        {
            Debug.Log("Cannot start a new mission while one is active.");
            return;
        }

        nameText.text = unit.unitName;
        taskText.text = unit.mission;
        rewardText.text = $"Reward: ${unit.reward}";

        popUpWindow.SetActive(true);
        moneyUI.SetActive(false);
        InfoUI.SetActive(false);
    }

    public void OpenInfoWindow(UnitScript unit)
    {
        if (missionActive)
        {
            Debug.Log("Cannot start a new mission while one is active.");
            return;
        }
        
        InfoPopUpnameText.text = unit.unitName;
        InfoPopUptaskText.text = unit.mission;
        InfoPopUprewardText.text = $"Reward: ${unit.reward}";

        InfoWindow.SetActive(true);
        moneyUI.SetActive(false);
        InfoUI.SetActive(false);
    }
    public void InfoButton()
    {

        InfoPopUpnameText.text = selectedUnit.unitName;
        InfoPopUptaskText.text = selectedUnit.mission;
        InfoPopUprewardText.text = $"Reward: ${selectedUnit.reward}";

        InfoWindow.SetActive(true);
        InfoUI.SetActive(false);
    }

    public void UpdateMoneyUI()
    {

        moneyUIText.text = $"$ {money}";
    }

    public void ClosePopUpWindow()
    {
        DeselectUnit();

        popUpWindow.SetActive(false);
        InfoUI.SetActive(true);
        moneyUI.SetActive(true);
    }

    public void Close()
    {
       
        InfoWindow.SetActive(false);
        InfoUI.SetActive(true);
        moneyUI.SetActive(true);
    }

}
