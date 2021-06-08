using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Advertisements;

public class GameOverMenu : MonoBehaviour, IUnityAdsListener
{
    private void Start()
    {
        //Advertisement
        if (Advertisement.isSupported)
        {
            Advertisement.Initialize("4160157", false);
            Advertisement.AddListener(this);
        }
    }

    public void MainMenu()
    {
        Settings.ResetAll();
        Settings.level = "MainMenu";
        SceneManager.LoadScene("LoadingScreen");
    }

    public void RestartGame()
    {
        Settings.ResetAll();
        Settings.level = "Level";
        //advertisment
        if (Advertisement.IsReady("Interstitial_Android") && Time.time - Settings.lastAd > Random.Range(120f, 300f))
        {
            Settings.lastAd = Time.time;
            Advertisement.Show("Interstitial_Android");
        }
        else
        {
            SceneManager.LoadScene("LoadingScreen");
        }
    }

    public void OnUnityAdsDidFinish(string surfacingId, ShowResult showResult)
    {
        SceneManager.LoadScene("LoadingScreen");
    }

    public void OnUnityAdsReady(string placementId)
    {
    }

    public void OnUnityAdsDidError(string message)
    {
    }

    public void OnUnityAdsDidStart(string placementId)
    {
    }
}