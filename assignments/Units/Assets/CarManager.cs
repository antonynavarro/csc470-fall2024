using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using UnityEngine.EventSystems;

public class CarManager : MonoBehaviour
{
    public static Action<CarScript> CarClicked;

    public static CarManager instance;

    public Camera mainCamera;

    public CarScript selectedCar;

    public List<CarScript> cars = new List<CarScript>();

    public GameObject CarpopUpWindow;
    public TMP_Text nameText;
    public TMP_Text priceText;
    public TMP_Text missionBonusReward;

    public CarScript previousSelectedCar;


    LayerMask layerMask;

    void OnEnable()
    {
        if (CarManager.instance == null)
        {
            CarManager.instance = this;
        }
        else
        {
            Destroy(this);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        layerMask = LayerMask.GetMask("ground", "taxi");
    }

    void Update()
    {
        // Check if the mouse is over a UI element
        if (EventSystem.current.IsPointerOverGameObject())
        {
            return; // Do nothing if the mouse is over a UI element
        }

        if (Input.GetMouseButtonDown(0)) // Left mouse button click
        {
            Ray mousePositionRay = mainCamera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hitInfo;

            if (Physics.Raycast(mousePositionRay, out hitInfo, Mathf.Infinity, layerMask))
            {
                Debug.Log("Raycast hit: " + hitInfo.collider.name);

                if (hitInfo.collider.CompareTag("ground"))
                {
                    // If we click on the ground, move the selected car (if any)
                    if (selectedCar != null)
                    {
                        Debug.Log("Moving car to: " + hitInfo.point);
                        selectedCar.agent.SetDestination(hitInfo.point);
                    }
                    else
                    {
                        Debug.Log("No car selected to move.");
                    }
                }
                else if (hitInfo.collider.CompareTag("taxi"))
                {
                    Debug.Log("Car clicked: " + hitInfo.collider.gameObject.name);
                    SelectCar(hitInfo.collider.gameObject.GetComponent<CarScript>());
                }
            }
            else
            {
                Debug.Log("Raycast did not hit anything.");
            }
        }
    }

    public void OpenCarInfoWindow()
    {
        if (selectedCar == null) return;

        nameText.text = selectedCar.carName;
        priceText.text = "$" + selectedCar.carPrice;
        missionBonusReward.text = "$" + selectedCar.missionBonusReward;

        CarpopUpWindow.SetActive(true);


    }

    public void SelectCar(CarScript car)
    {
        // Deselect all cars
        foreach (CarScript c in cars)
        {
            c.selected = false;
        }

        CarClicked?.Invoke(car);

        // Save the current car as previousSelectedCar
        previousSelectedCar = selectedCar;

        // Select the new car
        selectedCar = car;
        selectedCar.selected = true;
        if (!car.isBought)
        {
            OpenCarInfoWindow();
        }
    }

    public void ClosePopUpWindow()
    {
        CarpopUpWindow.SetActive(false);

        // Set the previously active car as the current active
        if (previousSelectedCar != null)
        {
            selectedCar = previousSelectedCar;
            previousSelectedCar.selected = true;
        }
    }

    public void BuyCar()
    {
        if(GameManager.instance.money >= CarManager.instance.selectedCar.carPrice)
        {
            GameManager.instance.money -= CarManager.instance.selectedCar.carPrice;
            GameManager.instance.UpdateMoneyUI();
            CarManager.instance.selectedCar.isBought = true;
            CarpopUpWindow.SetActive(false);

        }

    }

    
}
