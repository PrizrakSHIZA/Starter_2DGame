using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadingScreen : MonoBehaviour
{
    Slider progressbar;

    void Start()
    {
        progressbar = GameObject.Find("Slider").GetComponent<Slider>();
        //Start loading level async
        StartCoroutine(LoadLevelAsync());
    }

    IEnumerator LoadLevelAsync()
    {
        AsyncOperation gamelevel = SceneManager.LoadSceneAsync(Settings.level);

        while (gamelevel.progress < 1)
        {
            //Fill progressbar while level loading
            progressbar.value = gamelevel.progress;
            //progressbar.fillAmount = gamelevel.progress;
            yield return new WaitForEndOfFrame();
        }
    }
}
