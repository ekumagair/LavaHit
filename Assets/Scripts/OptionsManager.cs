using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.Localization;
using UnityEngine.Localization.Settings;

public class OptionsManager : MonoBehaviour
{
    public AudioMixer audioMixer;

    public static OptionsManager Instance { private set; get; }

    void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public int GetLanguageIndex(string locale)
    {
        switch (locale)
        {
            default:
            case "en":
                return 0;
            case "pt-BR":
                return 1;
        }
    }

    public string GetLanguageString(int index)
    {
        switch (index)
        {
            default:
            case 0:
                return "en";
            case 1:
                return "pt-BR";
        }
    }

    public void SetLanguage(int index)
    {
        switch (index)
        {
            default:
            case 0:
                SetLanguage("en");
                break;
            case 1:
                SetLanguage("pt-BR");
                break;
        }
    }

    public void SetLanguage(string language)
    {
        if (string.IsNullOrEmpty(language))
            return;

        Locale locale = LocalizationSettings.AvailableLocales.GetLocale(language);
        LocalizationSettings.SelectedLocale = locale;
    }

    public string CurrentLanguage => LocalizationSettings.SelectedLocale.Identifier.Code;

    public float GetMasterVolume() => GetVolume("MasterVolume");
    public void SetMasterVolume(float value) => SetVolume("MasterVolume", value);

    public float GetSFXVolume() => GetVolume("SFXVolume");
    public void SetSFXVolume(float value) => SetVolume("SFXVolume", value);

    public float GetMusicVolume() => GetVolume("MusicVolume");
    public void SetMusicVolume(float value) => SetVolume("MusicVolume", value);

    private float GetVolume(string parameter)
    {
        if (audioMixer.GetFloat(parameter, out float db))
        {
            return Mathf.Pow(10f, db / 20f);
        }

        return 1f;
    }

    private void SetVolume(string parameter, float value)
    {
        float volume = Mathf.Log10(Mathf.Max(value, 0.0001f)) * 20f;
        audioMixer.SetFloat(parameter, volume);
    }

    public float PostProcessWeight { get; set; } = 1.0f;
    public bool PostProcessBloom { get; set; } = true;
    public bool PostProcessAmbientOcclusion { get; set; } = true;
    public bool PostProcessMotionBlur { get; set; } = true;
    public bool PostProcessColorGrading { get; set; } = true;
    public bool PostProcessChromaticAberration { get; set; } = true;
}
