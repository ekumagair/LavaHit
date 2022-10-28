using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class JogadorScript : MonoBehaviour
{
    public float velocidade = 8f;
    public int vida;
    public CameraShake scriptCameraShake;
    public LayerMask layerBarreira;
    public float distanciaMinimaDaBarreira;
    public GameObject canvasObject;
    public GameObject objetoTriggerAtivarFisica;
    public GameObject vidaIcone;
    public GameObject vidaEfeito;
    public GameObject danoOverlay;
    public GameObject danoEfeito;

    [Header("Fim de jogo")]
    public GameObject textFimDeJogo;
    public GameObject fadeIn;

    [Header("Criação do cenário")]
    public GameObject chaoPrefab;
    public GameObject[] chaoAtual;
    public float proximaDistancia = 0;
    public float distanciaIntervalo;
    public int distanciaDeCriacao = 2;

    [Header("Som")]
    public AudioClip audioBateu;

    AudioSource audioSource;

    bool invulneravel = false;
    public static bool levandoDano = false;
    public static bool pertoDeBarreira = false;
    public static bool perdeu = false;
    public static int jogouQuantasVezes = 0;
    public static int tempoSegundos = 0;
    public static int tempoMinutos = 0;
    public static int tempoSegundosRecorde = 0;
    public static int tempoMinutosRecorde = 0;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        invulneravel = false;
        pertoDeBarreira = false;
        perdeu = false;
        tempoSegundos = 0;
        tempoMinutos = 0;
        StartCoroutine(AumentarVelocidade());
        StartCoroutine(Temporizador());
    }

    void Update()
    {
        // Ir para frente.
        if (CanvasScript.jogando == true && vida > 0)
        {
            transform.Translate(Vector3.forward * velocidade * Time.deltaTime);
        }

        // Gerar cenário.
        if(transform.position.z > proximaDistancia)
        {
            for (int i = 0; i < distanciaDeCriacao; i++)
            {
                chaoAtual[i] = Instantiate(chaoPrefab, new Vector3(0, 0, proximaDistancia + (distanciaIntervalo * (i + 1))), transform.rotation);
                //Destroy(chaoAtual[i], (distanciaIntervalo * distanciaDeCriacao) / (velocidade / 2));
            }
            proximaDistancia += distanciaIntervalo;
        }

        // Não gerar bolas de lava se estiver muito perto de uma barreira.
        if(Physics.Raycast(transform.position, transform.forward, distanciaMinimaDaBarreira, layerBarreira))
        {
            pertoDeBarreira = true;
        }
        else
        {
            pertoDeBarreira = false;
            Debug.DrawLine(transform.position, transform.position + transform.forward * distanciaMinimaDaBarreira, Color.white, 0.2f);
        }

        // Perder.
        if(vida <= 0 && perdeu == false)
        {
            perdeu = true;
            StartCoroutine(Perdeu());
        }
    }

    IEnumerator AumentarVelocidade()
    {
        // Aumentar a velocidade automaticamente com o tempo.
        while (CanvasScript.jogando == false)
        {
            // Esperar até o jogador clicar no botão de iniciar.
            yield return new WaitForEndOfFrame();
        }

        yield return new WaitForSeconds(10f);

        if (levandoDano == false)
        {
            velocidade += 2;
            Debug.Log("Aumentou a velocidade para " + velocidade);
        }

        StartCoroutine(AumentarVelocidade());
    }

    IEnumerator Temporizador()
    {
        // Contar tempo.
        yield return new WaitForSeconds(1);

        if(CanvasScript.jogando == true)
        {
            if (levandoDano == false && invulneravel == false)
            {
                tempoSegundos++;
            }

            if(tempoSegundos >= 60)
            {
                tempoSegundos = 0;
                tempoMinutos++;
            }
        }

        StartCoroutine(Temporizador());
    }

    IEnumerator Perdeu()
    {
        perdeu = true;
        CanvasScript.jogando = false;

        int tempoTotal = tempoSegundos + (tempoMinutos * 60);
        int tempoTotalRecorde = tempoSegundosRecorde + (tempoMinutosRecorde * 60);

        if (tempoTotal > tempoTotalRecorde)
        {
            tempoMinutosRecorde = tempoMinutos;
            tempoSegundosRecorde = tempoSegundos;

            PlayerPrefs.SetInt("recorde_minutos", tempoMinutosRecorde);
            PlayerPrefs.SetInt("recorde_segundos", tempoSegundosRecorde);
            PlayerPrefs.Save();
        }

        Instantiate(textFimDeJogo, canvasObject.transform);

        yield return new WaitForSeconds(4f);

        Instantiate(fadeIn, canvasObject.transform);

        yield return new WaitForSeconds(1.1f);

        SceneManager.LoadScene("Jogo");
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Obstaculo" && collision.gameObject.GetComponent<Rigidbody>().constraints == RigidbodyConstraints.FreezeAll)
        {
            Instantiate(objetoTriggerAtivarFisica, transform.position, transform.rotation);
            if (levandoDano == false && invulneravel == false)
            {
                StartCoroutine(LevarDano());
            }
        }
    }

    IEnumerator LevarDano()
    {
        levandoDano = true;
        invulneravel = true;
        Debug.Log("Levou dano");
        DetectorDeClique.tempoSegurandoMouse = 0f;
        scriptCameraShake.ShakeCamera(1.5f, 0.3f);
        velocidade *= 0.4f;
        vida--;

        Destroy(Instantiate(vidaEfeito, vidaIcone.transform), 1.4f);
        Destroy(Instantiate(danoOverlay, canvasObject.transform), 1.5f);
        Instantiate(danoEfeito, transform.position + (transform.forward * 2), new Quaternion(0, 0, 0, 0));
        audioSource.PlayOneShot(audioBateu);

        yield return new WaitForSeconds(1f);

        levandoDano = false;

        yield return new WaitForSeconds(2f);

        velocidade *= 1.5f;

        yield return new WaitForSeconds(1f);

        invulneravel = false;
    }
}
