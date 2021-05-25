using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EngineSound : MonoBehaviour
{
    AudioSource audiosrc;

    private void Start()
    {
        audiosrc = gameObject.GetComponent<AudioSource>();
        RoadMovement.SpeedChanged += ChangePitch;
    }
    private void Update()
    {
        if (RoadMovement.gameover)
            RoadMovement.SpeedChanged -= ChangePitch;
    }

    void ChangePitch()
    {
        if(audiosrc.pitch != 2f)
            audiosrc.pitch = (RoadMovement.Speed - 3f) / 30 + 1f;
    }
}
