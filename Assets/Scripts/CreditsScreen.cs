using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreditsScreen : MonoBehaviour
{
    void Update()
    {
        /*
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Back();
        }*/
    }

    public void Back()
    {
        CanvasManager.Instance.GoToMainMenuScreen();
    }
}
