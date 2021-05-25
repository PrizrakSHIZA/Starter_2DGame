using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuScript : MonoBehaviour
{
    public void StartGame()
    {
        Settings.ResetAll();
        SceneManager.LoadScene("Level");
    }

    public void Exit() 
    {
        Application.Quit();
    }
}
