using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverMenu : MonoBehaviour
{
    public void MainMenu()
    {
        Settings.level = "MainMenu";
        SceneManager.LoadScene("LoadingScreen");
    }

    public void RestartGame()
    {
        Settings.ResetAll();
        Settings.level = "Level";
        SceneManager.LoadScene("LoadingScreen");
    }
}
