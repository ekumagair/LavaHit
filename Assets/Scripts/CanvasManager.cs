using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasManager : MonoBehaviour
{
    [Header("Prefabs")]
    public GameObject fadeOut;

    [Header("Screens")]
    public MainMenuScreen mainMenuScreen;
    public OptionsScreen optionsScreen;
    public GameplayCanvas gameplayCanvas;

    public static CanvasManager Instance { private set; get; }

    void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }

    void Start()
    {
        GoToMainMenuScreen();

        if (PlayerScript.gamesAmount > 0)
        {
            FadeOut();
        }
    }

    public void FadeOut()
    {
        Destroy(Instantiate(fadeOut, transform), 1f);
    }

    public void GoToMainMenuScreen()
    {
        mainMenuScreen.gameObject.SetActive(true);
        gameplayCanvas.gameObject.SetActive(false);
        optionsScreen.gameObject.SetActive(false);
    }

    public void GoToOptionsScreen()
    {
        mainMenuScreen.gameObject.SetActive(false);
        gameplayCanvas.gameObject.SetActive(false);
        optionsScreen.gameObject.SetActive(true);
    }

    public void GoToGameplayCanvas()
    {
        mainMenuScreen.gameObject.SetActive(false);
        gameplayCanvas.gameObject.SetActive(true);
        optionsScreen.gameObject.SetActive(false);
    }
}
