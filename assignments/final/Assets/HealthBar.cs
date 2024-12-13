using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Image healthBarImage;
    private float health;
    private float maxHealth;
    private float lerpSpeed;

    private void Update()
    {
        lerpSpeed = 10 * Time.deltaTime;

        UpdateHealthBar();
    }

    public void SetMaxHealth(int maxHealthValue)
    {
        maxHealth = maxHealthValue;
        health = maxHealth;
        //UpdateHealthBar();
    }

    public void Damage(float damagePoints)
    {
        if (health > 0)
        {
            health -= damagePoints;
            if (health < 0)
                health = 0;

            //UpdateHealthBar();
        }
    }

    public void Repair()
    {
        if (health < maxHealth)
        {
            health = maxHealth;
            //UpdateHealthBar();
        }
    }

    private void UpdateHealthBar()
    {
        if (healthBarImage != null && maxHealth > 0)
        {
            healthBarImage.fillAmount = Mathf.Lerp(healthBarImage.fillAmount, (health/maxHealth), lerpSpeed);
            ColorChanger();
        }
    }


    //Skibidi rizz
    void ColorChanger()
    {
        if (maxHealth > 0) 
        {
            Color healthColor = Color.Lerp(Color.red, Color.green, health / maxHealth);
            healthBarImage.color = healthColor;
        }
    }
}
