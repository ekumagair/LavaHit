using System.Collections;
using System.Collections.Generic;
using TMPro;
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

    JogadorScript scriptJogador;

    public static bool jogando = false;

    void Start()
    {
        scriptJogador = GameObject.FindGameObjectWithTag("Player").GetComponent<JogadorScript>();
        jogando = false;
        gameplayRoot.SetActive(false);

        MostrarRecorde();

        if(JogadorScript.jogouQuantasVezes > 0)
        {
            Destroy(Instantiate(fadeOut, transform), 1f);
        }

        // Carregar recorde salvo.
        if(PlayerPrefs.HasKey("recorde_minutos"))
        {
            JogadorScript.tempoMinutosRecorde = PlayerPrefs.GetInt("recorde_minutos");
            JogadorScript.tempoSegundosRecorde = PlayerPrefs.GetInt("recorde_segundos");
            PlayerPrefs.Save();
        }
    }

    void Update()
    {
        if(jogando == true)
        {
            textVida.text = scriptJogador.vida.ToString();
            textTempo.text = JogadorScript.tempoMinutos.ToString() + " : ";

            if(JogadorScript.tempoSegundos < 10)
            {
                textTempo.text += "0" + JogadorScript.tempoSegundos.ToString();
            }
            else
            {
                textTempo.text += JogadorScript.tempoSegundos.ToString();
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
            if(Input.GetKeyDown(KeyCode.Delete))
            {
                JogadorScript.tempoMinutosRecorde = 0;
                JogadorScript.tempoSegundosRecorde = 0;
                PlayerPrefs.SetInt("recorde_minutos", 0);
                PlayerPrefs.SetInt("recorde_segundos", 0);
                PlayerPrefs.Save();
                MostrarRecorde();
            }

            // Sair do jogo com esc.
            if(Input.GetKeyDown(KeyCode.Escape))
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
        Application.Quit();
    }

    void MostrarRecorde()
    {
        textTempoRecorde.text = "Tempo recorde: " + JogadorScript.tempoMinutosRecorde + " : ";
        if (JogadorScript.tempoSegundosRecorde < 10)
        {
            textTempoRecorde.text += "0" + JogadorScript.tempoSegundosRecorde.ToString();
        }
        else
        {
            textTempoRecorde.text += JogadorScript.tempoSegundosRecorde.ToString();
        }
    }

    IEnumerator IniciarCoroutine()
    {
        yield return new WaitForSeconds(0.2f);
        menuRoot.SetActive(false);
        gameplayRoot.SetActive(true);
        jogando = true;
        JogadorScript.jogouQuantasVezes++;
    }
}
