using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasManager : MonoBehaviour
{
    [Header("Prefabs")]
    public GameObject fadeIn;
    public GameObject fadeOut;

    [Header("Screens")]
    public MainMenuScreen mainMenuScreen;
    public OptionsScreen optionsScreen;
    public CreditsScreen creditsScreen;
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
        FadeOut();
    }

    public void FadeIn()
    {
        Instantiate(fadeIn, transform);
    }

    public void FadeOut()
    {
        Destroy(Instantiate(fadeOut, transform), 1f);
    }

    public void HideAllScreens()
    {
        mainMenuScreen.gameObject.SetActive(false);
        gameplayCanvas.gameObject.SetActive(false);
        optionsScreen.gameObject.SetActive(false);
        creditsScreen.gameObject.SetActive(false);
    }

    public void GoToMainMenuScreen()
    {
        HideAllScreens();
        mainMenuScreen.gameObject.SetActive(true);
    }

    public void GoToOptionsScreen()
    {
        HideAllScreens();
        optionsScreen.gameObject.SetActive(true);
    }

    public void GoToGameplayCanvas()
    {
        HideAllScreens();
        gameplayCanvas.gameObject.SetActive(true);
    }

    public void GoToCreditsScreen()
    {
        HideAllScreens();
        creditsScreen.gameObject.SetActive(true);
    }
}
