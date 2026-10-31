using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CanvasScript : MonoBehaviour
{
    public GameObject menuRoot;
    public GameObject gameplayRoot;
    public GameObject fadeOut;

    public TextMeshProUGUI textTempoRecorde;
    public TextMeshProUGUI textTempo;
    public TextMeshProUGUI textVida;

    PlayerScript scriptJogador;

    public static bool jogando = false;

    void Start()
    {
        scriptJogador = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerScript>();
        jogando = false;
        gameplayRoot.SetActive(false);

        MostrarRecorde();

        if (PlayerScript.jogouQuantasVezes > 0)
        {
            Destroy(Instantiate(fadeOut, transform), 1f);
        }

        // Carregar recorde salvo.
        if (PlayerPrefs.HasKey("recorde_minutos"))
        {
            PlayerScript.tempoMinutosRecorde = PlayerPrefs.GetInt("recorde_minutos");
            PlayerScript.tempoSegundosRecorde = PlayerPrefs.GetInt("recorde_segundos");
            PlayerPrefs.Save();
        }
    }

    void Update()
    {
        if (jogando == true)
        {
            textVida.text = scriptJogador.vida.ToString();
            textTempo.text = PlayerScript.tempoMinutos.ToString() + " : ";

            if (PlayerScript.tempoSegundos < 10)
            {
                textTempo.text += "0" + PlayerScript.tempoSegundos.ToString();
            }
            else
            {
                textTempo.text += PlayerScript.tempoSegundos.ToString();
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
                PlayerScript.tempoMinutosRecorde = 0;
                PlayerScript.tempoSegundosRecorde = 0;
                PlayerPrefs.SetInt("recorde_minutos", 0);
                PlayerPrefs.SetInt("recorde_segundos", 0);
                PlayerPrefs.Save();
                MostrarRecorde();
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

    public void Sair()
    {
#if !UNITY_EDITOR && (UNITY_STANDALONE || UNITY_WEBGL)
        Application.Quit();
#else
        EditorApplication.isPlaying = false;
#endif
    }

    private void MostrarRecorde()
    {
        textTempoRecorde.text = "Tempo recorde: " + PlayerScript.tempoMinutosRecorde + " : ";

        if (PlayerScript.tempoSegundosRecorde < 10)
        {
            textTempoRecorde.text += "0" + PlayerScript.tempoSegundosRecorde.ToString();
        }
        else
        {
            textTempoRecorde.text += PlayerScript.tempoSegundosRecorde.ToString();
        }
    }

    private IEnumerator IniciarCoroutine()
    {
        yield return new WaitForSeconds(0.2f);

        menuRoot.SetActive(false);
        gameplayRoot.SetActive(true);
        jogando = true;
        PlayerScript.jogouQuantasVezes++;
    }
}
