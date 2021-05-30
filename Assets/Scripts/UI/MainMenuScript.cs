using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuScript : MonoBehaviour
{
    private void Start()
    {
        GameObject.Find("Canvas/Settings").GetComponent<Settings>().Read();
    }

    public void StartGame()
    {
        Settings.ResetAll();
        Settings.level = "Level";
        SceneManager.LoadScene("LoadingScreen");
    }

    public void OpenSettings()
    {
        GameObject.Find("Canvas/Settings").transform.localScale = new Vector3(1, 1, 1);
        //settings.SetActive(true);
        GameObject.Find("Canvas/MainMenu").SetActive(false);
    }

    public void Exit() 
    {
        Application.Quit();
    }
}
