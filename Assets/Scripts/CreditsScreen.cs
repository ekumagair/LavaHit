using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreditsScreen : MonoBehaviour
{
    public void Back()
    {
        CanvasManager.Instance.GoToMainMenuScreen();
    }
}
