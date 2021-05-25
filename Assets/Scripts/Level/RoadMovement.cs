using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public delegate void Notify();

public class RoadMovement : MonoBehaviour
{
    public AudioSource EngineSound;
    public AudioSource Music;

    static GameObject menu;
    
    public static event Notify SpeedChanged;

    public static float Speed = 3f;
    public static bool gameover = false, crashed = false, fuelEmpty = false;
    public static float lastSpeed;

    float endpos = -8.3f, lasttime;

    Transform[] parts = new Transform[3];

    private void Start()
    {
        menu = GameObject.Find("Canvas").gameObject;
        lasttime = 0;
        parts[0] = gameObject.transform.GetChild(0);
        parts[1] = gameObject.transform.GetChild(1);
        parts[2] = gameObject.transform.GetChild(2);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
            FuelSystem.Fuel = 0.3f;

        parts = parts.OrderByDescending(x => x.position.y).ToArray();
        foreach (Transform part in parts)
        {
            part.position -= new Vector3(0, Speed * Time.deltaTime, 0);
            if (part.position.y <= endpos)
            {
                part.position = new Vector3(0, parts[0].position.y + parts[0].gameObject.GetComponent<RectTransform>().rect.height, 0);
            }
        }

        if (gameover)
        {
            EngineSound.Stop();
            Music.Stop();
            if (Speed > 0.2f)
            {
                Speed = Mathf.Lerp(Speed, 0, Time.deltaTime);
            }
            else if (Speed != 0)
            {
                Speed = 0;
                menu.GetComponent<Animation>().Play();
                TextMeshProUGUI text = menu.transform.Find("GameOverMenu/Text_Score").GetComponent<TextMeshProUGUI>();
                text.text = "Your Score: " + ScoreCalculator.score.GetValue().ToString("0");
            }
            return;
        }
        
        if (Time.timeSinceLevelLoad - lasttime >= 5)
        {
            lasttime = Time.timeSinceLevelLoad;
            Speed += 0.3f;
            SpeedChanged?.Invoke();
        }
    }

    public static void GameOver(GameOverFlag flag)
    {
        //for future purposes
        switch ((int)flag)
        {
            case 0: crashed = true; break;
            case 1: fuelEmpty = true; break;
            default: break;
        }
        gameover = true;
        lastSpeed = Speed;
    }
}

public enum GameOverFlag
{ 
    crashed,
    fuel
}