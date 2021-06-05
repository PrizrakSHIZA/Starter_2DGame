using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial : MonoBehaviour
{
    void Start()
    {
        if (Settings.tutorial)
        {
            gameObject.GetComponent<Animation>().Play("TutorialAnim");
            Settings.tutorial = false;
            Settings.Save();
        }
    }
}
