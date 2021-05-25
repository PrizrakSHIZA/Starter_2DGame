using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class Settings : MonoBehaviour
{
    [Header("Mixer")]
    public AudioMixer mixer;
    [Header("UI Elements")]
    public GameObject settings;
    public GameObject mainMenu;

    public void MasterVolumeChanged(float value)
    {
        if (value == -20)
            value = -80;
        mixer.SetFloat("MasterVol", value);
    }

    public void MusicVolumeChanged(float value)
    {
        if (value == -20)
            value = -80;
        mixer.SetFloat("MusicVol", value);
    }

    public void SFXVolumeChanged(float value)
    {
        if (value == -20)
            value = -80;
        mixer.SetFloat("SFXVol", value);
    }

    public void OpenSettings()
    {
        settings.SetActive(true);
        mainMenu.SetActive(false);
    }
    public void CloseSettings()
    {
        settings.SetActive(false);
        mainMenu.SetActive(true);
    }

    //Method to reset level values
    public static void ResetAll()
    {
        RoadMovement.gameover = false;
        RoadMovement.crashed = false;
        RoadMovement.fuelEmpty = false;
        RoadMovement.Speed = 3f;
        FuelSystem.Fuel = 100f;
    }
}