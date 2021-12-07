using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealthbar : MonoBehaviour
{
    public float health;
    public float maxhealth;

    
    private HealthScript healthscript;

    public GameObject healthbarUI;
    public Slider slider;

    private void Start()
    {
        healthscript = GetComponent<HealthScript>();
        maxhealth = healthscript.health;
        health = maxhealth;
        slider.value = CalculateHealth();
    }
    private void Update()
    {

        health = healthscript.health;
        //print(health);

        if(health < maxhealth)
        {
            healthbarUI.SetActive(true);

        }
            slider.value = CalculateHealth();
        if (health > maxhealth)
        {
            health = maxhealth;
        }
    }

    float CalculateHealth()
    {
        return health / maxhealth;
    }
}
