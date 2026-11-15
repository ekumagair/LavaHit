using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;

public class MainMenuScreen : MonoBehaviour
{
    public TMP_Text textVersion;
    public TMP_Text textRecordTime;

    void Start()
    {
        PlayerScript.playing = false;

        // Carregar recorde salvo.
        if (PlayerPrefs.HasKey("recorde_minutos"))
        {
            PlayerScript.timeMinutesRecord = PlayerPrefs.GetInt("recorde_minutos");
            PlayerScript.timeSecondsRecord = PlayerPrefs.GetInt("recorde_segundos");
            PlayerPrefs.Save();
        }

        ShowText();
    }

    void Update()
    {
        // Deletar dados salvos.
        if (Input.GetKeyDown(KeyCode.Delete))
        {
            PlayerScript.timeMinutesRecord = 0;
            PlayerScript.timeSecondsRecord = 0;
            PlayerPrefs.SetInt("recorde_minutos", 0);
            PlayerPrefs.SetInt("recorde_segundos", 0);
            PlayerPrefs.Save();
            ShowText();
        }

        // Sair do jogo com esc.
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            QuitGame();
        }
    }

    private void ShowText()
    {
        textRecordTime.text = PlayerScript.timeMinutesRecord.ToString() + ":";

        if (PlayerScript.timeSecondsRecord < 10)
        {
            textRecordTime.text += "0" + PlayerScript.timeSecondsRecord.ToString();
        }
        else
        {
            textRecordTime.text += PlayerScript.timeSecondsRecord.ToString();
        }

        textVersion.text = "v" + Application.version;
    }

    public void PlayGame()
    {
        CanvasManager.Instance.GoToGameplayCanvas();
        PlayerScript.playing = true;
        PlayerScript.gamesAmount++;
    }

    public void GoToOptions()
    {
        CanvasManager.Instance.GoToOptionsScreen();
    }

    public void GoToCredits()
    {
        CanvasManager.Instance.GoToCreditsScreen();
    }

    public void QuitGame()
    {
#if !UNITY_EDITOR && (UNITY_STANDALONE || UNITY_WEBGL)
        Application.Quit();
#else
        EditorApplication.isPlaying = false;
#endif
    }
}
