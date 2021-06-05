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

    public static bool tutorial = true;
    public static bool SideControl = false;
    public static string level = "Level";
    static string filepath;
    static float masterVol, musicVol, SFXVol;

    private void Awake()
    {
        filepath = Application.persistentDataPath + "/settings.xml";
    }

    public void TutorialChanged(bool value)
    {
        tutorial = value;
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
        PlayerPrefs.SetFloat("MasterVol", masterVol);
    }

    public void MusicVolumeChanged(float value)
    {
        if (value == -20)
            value = -80;
        mixer.SetFloat("MusicVol", value);
        musicVol = value;
        PlayerPrefs.SetFloat("MusicVol", musicVol);
    }

    public void SFXVolumeChanged(float value)
    {
        if (value == -20)
            value = -80;
        mixer.SetFloat("SFXVol", value);
        SFXVol = value;
        PlayerPrefs.SetFloat("SFXVol", SFXVol);
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
    public static void Save()
    {
        XmlWriterSettings settings = new XmlWriterSettings();
        settings.Indent = true;
        settings.IndentChars = ("\t");
        settings.OmitXmlDeclaration = true;

        XmlWriter xmlWriter = XmlWriter.Create(filepath, settings);

        xmlWriter.WriteStartDocument();
        xmlWriter.WriteStartElement("settings");

        xmlWriter.WriteStartElement("Tutorial");
        xmlWriter.WriteString(tutorial.ToString());
        xmlWriter.WriteEndElement();

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

        //Play tutorial
        node = doc.DocumentElement.SelectSingleNode("/settings/Tutorial");
        if (node != null)
        {
            Boolean.TryParse(node.InnerText, out tutorial);
            GameObject.Find("PlayTutorial").GetComponent<Toggle>().isOn = tutorial;
        }

        //Side buttons control
        node = doc.DocumentElement.SelectSingleNode("/settings/SideButtonsControl");
        if (node != null)
        {
            Boolean.TryParse(node.InnerText, out SideControl);
            GameObject.Find("ToggleControl").GetComponent<Toggle>().isOn = SideControl;
        }

        //master volume
        node = doc.DocumentElement.SelectSingleNode("/settings/MasterVolume");
        if (node != null)
        {
            float.TryParse(node.InnerText, out temp);
            MasterVolumeChanged(temp);
            GameObject.Find("MasterVolume").GetComponent<Slider>().value = temp;
        }

        //Music volume
        node = doc.DocumentElement.SelectSingleNode("/settings/MusicVolume");
        if (node != null)
        {
            float.TryParse(node.InnerText, out temp);
            MusicVolumeChanged(temp);
            GameObject.Find("MusicVolume").GetComponent<Slider>().value = temp;
        }

        //SFX volume
        node = doc.DocumentElement.SelectSingleNode("/settings/SFXVolume");
        if (node != null)
        {
            float.TryParse(node.InnerText, out temp);
            SFXVolumeChanged(temp);
            GameObject.Find("SFXVolume").GetComponent<Slider>().value = temp;
        }
    }
}