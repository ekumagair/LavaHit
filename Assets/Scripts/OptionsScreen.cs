using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OptionsScreen : MonoBehaviour
{
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
        optionMasterVolume.SetValue(OptionsManager.Instance.GetMasterVolume());
        optionSfxVolume.SetValue(OptionsManager.Instance.GetSFXVolume());
        optionMusicVolume.SetValue(OptionsManager.Instance.GetMusicVolume());
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
