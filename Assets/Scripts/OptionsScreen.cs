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
    public OptionSlider optionPostWeight;
    public OptionCheckbox optionPostBloom;
    public OptionCheckbox optionPostAmbientOcc;
    public OptionCheckbox optionMotionBlur;
    public OptionCheckbox optionColorGrading;
    public OptionCheckbox optionChromatic;

    void OnEnable()
    {
        GetSettings();
    }

    void Update()
    {
        /*
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Back();
        }*/
    }

    public void GetSettings()
    {
        if (OptionsManager.Instance == null)
            return;

        optionLanguage.SetValue(OptionsManager.Instance.GetLanguageIndex(OptionsManager.Instance.CurrentLanguage));
        optionMasterVolume.SetValue(OptionsManager.Instance.GetMasterVolume());
        optionSfxVolume.SetValue(OptionsManager.Instance.GetSFXVolume());
        optionMusicVolume.SetValue(OptionsManager.Instance.GetMusicVolume());
        optionPostWeight.SetValue(OptionsManager.Instance.PostProcessWeight);
        optionPostBloom.SetValue(OptionsManager.Instance.PostProcessBloom);
        optionPostAmbientOcc.SetValue(OptionsManager.Instance.PostProcessAmbientOcclusion);
        optionMotionBlur.SetValue(OptionsManager.Instance.PostProcessMotionBlur);
        optionColorGrading.SetValue(OptionsManager.Instance.PostProcessColorGrading);
        optionChromatic.SetValue(OptionsManager.Instance.PostProcessChromaticAberration);
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

    public void OnPostWeightChanged(float value)
    {
        OptionsManager.Instance.PostProcessWeight = value;
        PostProcessingManager.Instance.GetSettings();
    }

    public void OnPostBloomChanged(bool value)
    {
        OptionsManager.Instance.PostProcessBloom = value;
        PostProcessingManager.Instance.GetSettings();
    }

    public void OnPostAmbientOccChanged(bool value)
    {
        OptionsManager.Instance.PostProcessAmbientOcclusion = value;
        PostProcessingManager.Instance.GetSettings();
    }

    public void OnPostMotionBlurChanged(bool value)
    {
        OptionsManager.Instance.PostProcessMotionBlur = value;
        PostProcessingManager.Instance.GetSettings();
    }

    public void OnPostColorGradingChanged(bool value)
    {
        OptionsManager.Instance.PostProcessColorGrading = value;
        PostProcessingManager.Instance.GetSettings();
    }

    public void OnPostChromaticChanged(bool value)
    {
        OptionsManager.Instance.PostProcessChromaticAberration = value;
        PostProcessingManager.Instance.GetSettings();
    }

    public void Back()
    {
        SaveManager.Save();
        PostProcessingManager.Instance.GetSettings();
        CanvasManager.Instance.GoToMainMenuScreen();
    }
}
