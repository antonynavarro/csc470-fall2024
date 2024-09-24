using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class FuelScript : MonoBehaviour
{
    public TextMeshProUGUI fuelText; 
    public float maxFuel = 100f; 
    private float currentFuel; 
    public float fuelConsumptionRate = 5f; 
    private bool isOutOfFuel = false;
    public GameObject explosionPrefab;
    public GameOverScreen gameOverScreen;

    // Start is called before the first frame update
    void Start()
    {
        currentFuel = maxFuel;
        UpdateFuelUI();
    }

    // Update is called once per frame
    void Update()
    {
        
        if (currentFuel > 0)
        {
            currentFuel -= fuelConsumptionRate * Time.deltaTime;
            currentFuel = Mathf.Clamp(currentFuel, 0, maxFuel); 
            UpdateFuelUI();
        }
        else if (!isOutOfFuel)
        {
            isOutOfFuel = true;
            HandleOutOfFuel(); 
        }
    }

    
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("fuel"))
        {
            Refuel();
            Destroy(other.gameObject); 
        }
    }

   
    private void Refuel()
    {
        currentFuel = maxFuel;
        UpdateFuelUI();
        Debug.Log("Fuel refilled!");
    }

    
    private void UpdateFuelUI()
    {
        fuelText.text = "Fuel: " + Mathf.Round(currentFuel) + "%";
    }

   
    private void HandleOutOfFuel()
    {
        Debug.Log("Out of fuel!");
        // Instantiate the explosion effect 
        Instantiate(explosionPrefab, transform.position, transform.rotation);
        Destroy(gameObject);

        gameOverScreen.TriggerGameOverScreen("Running out of fuel somehow  made you explode");

    }
}
