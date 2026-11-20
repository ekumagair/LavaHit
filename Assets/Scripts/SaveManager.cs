using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public static class SaveManager
{
    public static string SavePath => Path.Combine(Application.persistentDataPath, SAVE_FILE_NAME);

    public const int SAVE_VERSION = 1;
    public const string SAVE_FILE_NAME = "SaveData.json";

    public static void Save()
    {
        SaveData saveData = new SaveData();
        string json = JsonUtility.ToJson(saveData, true);
        File.WriteAllText(SavePath, json);
    }

    public static void Load()
    {
        if (!File.Exists(SavePath))
            return;

        string json = File.ReadAllText(SavePath);
        SaveData loadedData = JsonUtility.FromJson<SaveData>(json);

        PlayerScript.timeMinutesRecord = loadedData.recordMinutes;
        PlayerScript.timeSecondsRecord = loadedData.recordSeconds;

        OptionsManager.Instance.SetMasterVolume(loadedData.masterVolume);
        OptionsManager.Instance.SetSFXVolume(loadedData.sfxVolume);
        OptionsManager.Instance.SetMusicVolume(loadedData.musicVolume);

        OptionsManager.Instance.SetLanguage(loadedData.languageCode);

        OptionsManager.Instance.PostProcessWeight = loadedData.postProcessWeight;
        OptionsManager.Instance.PostProcessBloom = loadedData.postProcessBloom;
        OptionsManager.Instance.PostProcessAmbientOcclusion = loadedData.postProcessAmbientOcclusion;
        OptionsManager.Instance.PostProcessMotionBlur = loadedData.postProcessMotionBlur;
        OptionsManager.Instance.PostProcessColorGrading = loadedData.postProcessColorGrading;
        OptionsManager.Instance.PostProcessChromaticAberration = loadedData.postProcessChromaticAberration;
    }

    public static void Delete()
    {
        if (!File.Exists(SavePath))
            return;

        File.Delete(SavePath);
    }
}
