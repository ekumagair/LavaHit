using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CanvasScript : MonoBehaviour
{
    [Header("References")]
    public GameObject fadeOut;

    [Header("Components")]
    public GameObject menuRoot;
    public GameObject gameplayRoot;

    [Header("Text")]
    public TextMeshProUGUI textVersion;
    public TextMeshProUGUI textTempoRecorde;
    public TextMeshProUGUI textTempo;
    public TextMeshProUGUI textVida;

    private PlayerScript _playerScript;

    void Start()
    {
        _playerScript = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerScript>();
        PlayerScript.playing = false;
        gameplayRoot.SetActive(false);

        ShowMainMenuText();

        if (PlayerScript.gamesAmount > 0)
        {
            Destroy(Instantiate(fadeOut, transform), 1f);
        }

        // Carregar recorde salvo.
        if (PlayerPrefs.HasKey("recorde_minutos"))
        {
            PlayerScript.timeMinutesRecord = PlayerPrefs.GetInt("recorde_minutos");
            PlayerScript.timeSecondsRecord = PlayerPrefs.GetInt("recorde_segundos");
            PlayerPrefs.Save();
        }
    }

    void Update()
    {
        if (PlayerScript.playing == true)
        {
            textVida.text = _playerScript.vida.ToString();
            textTempo.text = PlayerScript.timeMinutes.ToString() + " : ";

            if (PlayerScript.timeSeconds < 10)
            {
                textTempo.text += "0" + PlayerScript.timeSeconds.ToString();
            }
            else
            {
                textTempo.text += PlayerScript.timeSeconds.ToString();
            }

            // Voltar para o menu com esc.
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                SceneManager.LoadScene("Jogo");
            }
        }
        else
        {
            // Deletar dados salvos.
            if (Input.GetKeyDown(KeyCode.Delete))
            {
                PlayerScript.timeMinutesRecord = 0;
                PlayerScript.timeSecondsRecord = 0;
                PlayerPrefs.SetInt("recorde_minutos", 0);
                PlayerPrefs.SetInt("recorde_segundos", 0);
                PlayerPrefs.Save();
                ShowMainMenuText();
            }

            // Sair do jogo com esc.
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                Sair();
            }
        }
    }

    public void Iniciar()
    {
        StartCoroutine(IniciarCoroutine());
    }

    private IEnumerator IniciarCoroutine()
    {
        yield return new WaitForSeconds(0.2f);

        menuRoot.SetActive(false);
        gameplayRoot.SetActive(true);
        PlayerScript.playing = true;
        PlayerScript.gamesAmount++;
    }

    private void ShowMainMenuText()
    {
        textTempoRecorde.text = "Tempo recorde: " + PlayerScript.timeMinutesRecord + " : ";

        if (PlayerScript.timeSecondsRecord < 10)
        {
            textTempoRecorde.text += "0" + PlayerScript.timeSecondsRecord.ToString();
        }
        else
        {
            textTempoRecorde.text += PlayerScript.timeSecondsRecord.ToString();
        }

        textVersion.text = "v" + Application.version;
    }

    public void Sair()
    {
#if !UNITY_EDITOR && (UNITY_STANDALONE || UNITY_WEBGL)
        Application.Quit();
#else
        EditorApplication.isPlaying = false;
#endif
    }
}
