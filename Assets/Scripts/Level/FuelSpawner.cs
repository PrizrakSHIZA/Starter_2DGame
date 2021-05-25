using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FuelSpawner : MonoBehaviour
{
    public GameObject FuelPF;

    float height = 12f, minx = -2.2f, maxx = 2.2f;
    float lasttime = 0;

    void Update()
    {
        if (RoadMovement.gameover)
            return;

        if (Time.timeSinceLevelLoad - lasttime >= 5)
        {
            lasttime = Time.timeSinceLevelLoad;
            if (FuelSystem.Fuel > 50)
            {
                if (Random.value > 0.9)
                    SpawnFuel();
            }
            else if (FuelSystem.Fuel < 50 && FuelSystem.Fuel > 20)
            {
                if (Random.value > 0.7)
                    SpawnFuel();
            }
            else if (FuelSystem.Fuel < 20)
            { 
                if (Random.value > 0.2)
                    SpawnFuel();
            }
        }
    }

    void SpawnFuel()
    {
        GameObject temp = Instantiate(FuelPF, transform.parent);
        temp.transform.position = new Vector3(Random.Range(minx, maxx), height, 0);
    }
}
