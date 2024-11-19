using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CarManager : MonoBehaviour
{

    public Camera mainCamera;

    public List<CarScript> cars = new List<CarScript>();
    public CarScript activeCar;

    public GameObject carInfoWindow;
    public TMP_Text carNameText;
    public TMP_Text carPriceText;

    public GameManager gameManager;

    public TMP_Text moneyUIText;

    public Button buyButton;



    void Start()
    {
        // Ensure only the active car has the TaxiScript enabled at the start
        foreach (CarScript car in cars)
        {
            var taxiScript = car.GetComponent<TaxiScript>();
            if (taxiScript != null)
            {
                taxiScript.enabled = (car == activeCar);
            }
        }

        Debug.Log($"Scene initialized. Active car: {activeCar?.carName}");
    }
    public void ShowCarInfo(CarScript car)
    {
        carNameText.text = car.carName;
        carPriceText.text = $"{car.price}";
        buyButton.interactable = !car.isPurchased && gameManager.money >= car.price;
        carInfoWindow.SetActive(true);
        buyButton.onClick.RemoveAllListeners();
        buyButton.onClick.AddListener(() => PurchaseCar(car));
        
    }

    public void PurchaseCar(CarScript car)
    {
        if (gameManager.money >= car.price)
        {
            gameManager.money -= car.price;
            UpdateMoneyUI();
            car.isPurchased = true;
            carPriceText.text = "Purchased";
            buyButton.interactable = false;
            carInfoWindow.SetActive(false);

            SwitchActiveCar(car);
        }
    }

    public void SwitchActiveCar(CarScript car)
    {
        if (car.isPurchased)
        {
            // Deactivate TaxiScript for all cars
            foreach (CarScript c in cars)
            {
                var taxiScript = c.GetComponent<TaxiScript>();
                if (taxiScript != null)
                {
                    taxiScript.enabled = false;
                }
            }

            // Set the new active car
            activeCar = car;

            // Activate TaxiScript for the selected car
            var activeTaxiScript = activeCar.GetComponent<TaxiScript>();
            if (activeTaxiScript != null)
            {
                activeTaxiScript.enabled = true;
            }

            Debug.Log($"Active car switched to: {activeCar.carName}");
        }
        else
        {
            Debug.Log("Car not purchased. Cannot switch.");
        }
    }

    /*public void SwitchActiveCar(CarScript car)
    {
        if (car.isPurchased)
        {
            if (activeCar != null)
            {
                activeCar.carModel.SetActive(false); // Deactivate current car
            }
            activeCar = car;
            activeCar.carModel.SetActive(true); // Activate new car
        }
    }*/

    private void UpdateMoneyUI()
    {
        moneyUIText.text = $"$ {gameManager.money}";
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (EventSystem.current.IsPointerOverGameObject()) return;

            Ray mousePositionRay = mainCamera.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(mousePositionRay, out RaycastHit hitInfo))
            {
                Debug.Log($"Raycast hit: {hitInfo.collider.gameObject.name}");
                CarScript car = hitInfo.collider.GetComponent<CarScript>();
                if (car != null)
                {
                    Debug.Log($"Car detected: {car.carName}");
                    if (!car.isPurchased)
                    {
                        ShowCarInfo(car); // Show car details when clicked
                    }
                }
                else
                {
                    Debug.Log("No CarScript detected on the hit object.");
                }
            }
        }
    }


    public void CloseCarInfoWindow()
    {
        carInfoWindow.SetActive(false); // Hides the car info window
    }

}
