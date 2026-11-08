using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

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
    }

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
}
