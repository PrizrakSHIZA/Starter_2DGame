using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ScoreCalculator : MonoBehaviour
{
    public TextMeshProUGUI scoretext;

    public static SecuredFloat score;
    float lastcheck = 0;


    private void Start()
    {
        score = new SecuredFloat(0);
    }

    void Update()
    {
        if (RoadMovement.gameover)
            return;

        if (Time.timeSinceLevelLoad - lastcheck > 0.1)
        {
            score += new SecuredFloat(1 * (RoadMovement.Speed - 2f) * (Time.timeSinceLevelLoad - lastcheck));
            lastcheck = Time.timeSinceLevelLoad;
            scoretext.text = score.GetValue().ToString("0000");
        }
    }

    public struct SecuredFloat
    {
        float seed;
        float score;

        public SecuredFloat(float value)
        {
            seed = Random.Range(-10000, 10000);
            score = value + seed;
        }

        public float GetValue()
        {
            return score - seed;
        }

        public static SecuredFloat operator +(SecuredFloat a, SecuredFloat b)
        {
            return new SecuredFloat(a.GetValue() + b.GetValue());
        }

        public static SecuredFloat operator -(SecuredFloat a, SecuredFloat b)
        {
            return new SecuredFloat(a.GetValue() - b.GetValue());
        }
    }
}
