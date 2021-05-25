using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverMenu : MonoBehaviour
{
    public void MainMenu()
    { 
        SceneManager.LoadScene("MainMenu");
    }

    public void RestartGame()
    {
        Settings.ResetAll();
        SceneManager.LoadScene("Level");
    }
}
