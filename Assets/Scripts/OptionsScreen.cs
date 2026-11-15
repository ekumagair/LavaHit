using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Localization.Settings;

public class OptionsScreen : MonoBehaviour
{
    [Header("Components")]
    public OptionDropdown optionLanguage;
    public OptionSlider optionMasterVolume;
    public OptionSlider optionSfxVolume;
    public OptionSlider optionMusicVolume;

    void Start()
    {
        GetSettings();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Back();
        }
    }

    public void GetSettings()
    {
        optionLanguage.SetValue(OptionsManager.Instance.GetLanguage(LocalizationSettings.SelectedLocale.Identifier.Code));
        optionMasterVolume.SetValue(OptionsManager.Instance.GetMasterVolume());
        optionSfxVolume.SetValue(OptionsManager.Instance.GetSFXVolume());
        optionMusicVolume.SetValue(OptionsManager.Instance.GetMusicVolume());
    }

    public void OnLanguageChanged(int index)
    {
        OptionsManager.Instance.SetLanguage(index);
    }

    public void OnMasterVolumeChanged(float value)
    {
        OptionsManager.Instance.SetMasterVolume(value);
    }

    public void OnSFXVolumeChanged(float value)
    {
        OptionsManager.Instance.SetSFXVolume(value);
    }

    public void OnMusicVolumeChanged(float value)
    {
        OptionsManager.Instance.SetMusicVolume(value);
    }

    public void Back()
    {
        CanvasManager.Instance.GoToMainMenuScreen();
    }
}
