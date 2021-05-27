using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Xml;
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

    public static bool SideControl = false;
    static string filepath;
    float masterVol, musicVol, SFXVol;

    private void Awake()
    {
        filepath = Application.persistentDataPath + "/settings.xml";
    }

    public void ControlChanged(bool value)
    {
        SideControl = value;
    }

    public void MasterVolumeChanged(float value)
    {
        if (value == -20)
            value = -80;
        mixer.SetFloat("MasterVol", value);
        masterVol = value;
    }

    public void MusicVolumeChanged(float value)
    {
        if (value == -20)
            value = -80;
        mixer.SetFloat("MusicVol", value);
        musicVol = value;
    }

    public void SFXVolumeChanged(float value)
    {
        if (value == -20)
            value = -80;
        mixer.SetFloat("SFXVol", value);
        SFXVol = value;
    }

    public void CloseSettings()
    {
        settings.transform.localScale = new Vector3(0, 0, 0);
        //settings.SetActive(false);
        mainMenu.SetActive(true);
        Save();
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

    //Save/Read settings
    public void Save()
    {
        XmlWriterSettings settings = new XmlWriterSettings();
        settings.Indent = true;
        settings.IndentChars = ("\t");
        settings.OmitXmlDeclaration = true;

        XmlWriter xmlWriter = XmlWriter.Create(filepath, settings);

        xmlWriter.WriteStartDocument();
        xmlWriter.WriteStartElement("settings");

        xmlWriter.WriteStartElement("SideButtonsControl");
        xmlWriter.WriteString(SideControl.ToString());
        xmlWriter.WriteEndElement();

        xmlWriter.WriteStartElement("MasterVolume");
        xmlWriter.WriteString(masterVol.ToString());
        xmlWriter.WriteEndElement();

        xmlWriter.WriteStartElement("MusicVolume");
        xmlWriter.WriteString(musicVol.ToString());
        xmlWriter.WriteEndElement();

        xmlWriter.WriteStartElement("SFXVolume");
        xmlWriter.WriteString(SFXVol.ToString());

        xmlWriter.WriteEndDocument();
        xmlWriter.Close();
    }

    public void Read()
    {
        if (!File.Exists(filepath))
            return;

        XmlDocument doc = new XmlDocument();
        doc.Load(filepath);
        XmlNode node;
        //flaot values
        float temp = 0;

        //Side buttons control
        node = doc.DocumentElement.SelectSingleNode("/settings/SideButtonsControl");
        Boolean.TryParse(node.InnerText, out SideControl);
        GameObject.Find("ToggleControl").GetComponent<Toggle>().isOn = SideControl;

        //master volume
        node = doc.DocumentElement.SelectSingleNode("/settings/MasterVolume");
        float.TryParse(node.InnerText, out temp);
        MasterVolumeChanged(temp);
        GameObject.Find("MasterVolume").GetComponent<Slider>().value = temp;
        
        //Music volume
        node = doc.DocumentElement.SelectSingleNode("/settings/MusicVolume");
        float.TryParse(node.InnerText, out temp);
        MusicVolumeChanged(temp);
        GameObject.Find("MusicVolume").GetComponent<Slider>().value = temp;
        
        //SFX volume
        node = doc.DocumentElement.SelectSingleNode("/settings/SFXVolume");
        float.TryParse(node.InnerText, out temp);
        SFXVolumeChanged(temp);
        GameObject.Find("SFXVolume").GetComponent<Slider>().value = temp;
    }
}