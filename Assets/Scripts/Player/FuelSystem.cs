using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FuelSystem : MonoBehaviour
{
    public static float Fuel = 100f;
    public Slider slider;
    public AudioSource audioSource;
    float lastTime;

    private void Start()
    {
        lastTime = Time.timeSinceLevelLoad;
    }

    private void Update()
    {
        if (RoadMovement.gameover)
            return;

        if (Time.timeSinceLevelLoad - lastTime > 0.1 && Fuel > 0)
        {
            Fuel -= 0.2f;
            lastTime = Time.timeSinceLevelLoad;
            slider.value = Fuel;

            //Set Fuel to 0 to prevent negative values
            if (Fuel < 0) Fuel = 0;
        }

        if (Fuel == 0)
        {
            audioSource.Play();
            RoadMovement.GameOver(GameOverFlag.fuel);
        }
    }

    public void AddFuel(float amount)
    {
        Fuel += amount;
        //prevent getting more than 100% of fuel
        if (Fuel > 100) Fuel = 100;
        slider.value = Fuel;
    }
}
