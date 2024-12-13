using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CarScript : MonoBehaviour
{
    public string carName;
    public int carPrice;

    public int missionBonusReward;

    public bool isBought = false;
    

    public NavMeshAgent agent;

    //public Renderer bodyRenderer;
    //public Color normalColor;
    //public Color selectedColor;

    public bool selected = false;

    private LayerMask layerMask;

    void OnEnable()
    {
        CarManager.CarClicked += CarManagerSaysCarWasClicked;
    }

    void OnDisable()
    {
        CarManager.CarClicked -= CarManagerSaysCarWasClicked;
    }

    void CarManagerSaysCarWasClicked(CarScript car)
    {
        if (car == this)
        {
            selected = true;
            //bodyRenderer.material.color = selectedColor;
        }
        else
        {
            selected = false;
            //bodyRenderer.material.color = normalColor;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        layerMask = LayerMask.GetMask("taxi", "ground");

        // Optionally, you can initialize some randomization or settings here
    }

    void OnDestroy()
    {
        CarManager.instance.cars.Remove(this);
    }

    // Update is called once per frame
    void Update()
    {
        // Handle car rotation or other movement logic if needed
    }
}
