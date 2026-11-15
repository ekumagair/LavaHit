using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SaveData
{
    public int saveVersion;
    public string applicationVersion;

    public int recordMinutes;
    public int recordSeconds;

    public float masterVolume;
    public float sfxVolume;
    public float musicVolume;

    public int languageIndex;
    public string languageCode;

    public SaveData()
    {
        saveVersion = SaveManager.SAVE_VERSION;
        applicationVersion = Application.version;

        recordMinutes = PlayerScript.timeMinutesRecord;
        recordSeconds = PlayerScript.timeSecondsRecord;

        masterVolume = OptionsManager.Instance.GetMasterVolume();
        sfxVolume = OptionsManager.Instance.GetSFXVolume();
        musicVolume = OptionsManager.Instance.GetMusicVolume();

        languageIndex = OptionsManager.Instance.GetLanguageIndex(OptionsManager.Instance.CurrentLanguage);
        languageCode = OptionsManager.Instance.CurrentLanguage;
    }
}
