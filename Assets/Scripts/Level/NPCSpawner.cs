using System.Collections.Generic;
using UnityEngine;


public class NPCSpawner : MonoBehaviour
{
    [SerializeField]
    GameObject[] allCars;


    Dictionary<int, float> Lines = new Dictionary<int, float>(4);
    float lastInterval = 0, lasttime = 0;
    float pos, range = 0.3f, interval = 2;
    int line, lastline;
    bool doubled = false;


    private void Start()
    {
        //initialization
        Lines.Add(1, -2f);
        Lines.Add(2, -0.7f);
        Lines.Add(3, 0.6f);
        Lines.Add(4, 1.9f);
    }

    void Update()
    {
        if (RoadMovement.gameover)
            return;
        
        //randomize line
        System.Random rand = new System.Random();
        line = rand.Next(1, 4);
        lastline = line;

        if (Time.timeSinceLevelLoad - lastInterval > interval)
        {
            //randomize position
            Lines.TryGetValue(line, out pos);
            pos += Random.Range(-range, range);
            //instantiate prefab
            GameObject temp = Instantiate(allCars[Random.Range(0, 3)], transform.parent);
            temp.transform.position = new Vector3(pos, 8f, -1f);
            if (doubled)
            {
                while(line == lastline)
                    line = rand.Next(1, 4);
                Lines.TryGetValue(line, out pos);
                pos += Random.Range(-range, range);
                temp = Instantiate(allCars[Random.Range(0, 2)], transform.parent);
                temp.transform.position = new Vector3(pos, 8f, -1f);
            }

            lastInterval = Time.timeSinceLevelLoad;
        }

        //change interval
        if (Time.timeSinceLevelLoad - lasttime >= 10)
        {
            lasttime = Time.timeSinceLevelLoad;
            interval -= 0.1f;
            if (interval <= 0.8f && !doubled)
            {
                interval = 2f;
                doubled = true;
            }
            if (interval <= 0.8f && doubled)
            {
                interval = 0.8f;
            }
        }
    }
}
