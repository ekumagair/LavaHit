using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerScript : MonoBehaviour
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

    [Header("Criação do cenário")]
    public GameObject chaoPrefab;
    public GameObject[] chaoAtual;
    public float proximaDistancia = 0;
    public float distanciaIntervalo;
    public int distanciaDeCriacao = 2;

    public static bool playing = false;
    public static bool lost = false;
    public static bool takingDamage = false;
    public static bool closeToBarrier = false;
    public static int gamesAmount = 0;
    public static int timeSeconds = 0;
    public static int timeMinutes = 0;
    public static int timeSecondsRecord = 0;
    public static int timeMinutesRecord = 0;

    private bool _invulnerable = false;

    void Start()
    {
        _invulnerable = false;
        closeToBarrier = false;
        lost = false;
        timeSeconds = 0;
        timeMinutes = 0;
        StartCoroutine(IncreaseSpeed());
        StartCoroutine(Timer());
    }

    void Update()
    {
        // Ir para frente.
        if (playing == true && vida > 0)
        {
            transform.Translate(Vector3.forward * velocidade * Time.deltaTime);
        }

        // Gerar cenário.
        if (transform.position.z > proximaDistancia)
        {
            for (int i = 0; i < distanciaDeCriacao; i++)
            {
                chaoAtual[i] = Instantiate(chaoPrefab, new Vector3(0, 0, proximaDistancia + (distanciaIntervalo * (i + 1))), transform.rotation);
                //Destroy(chaoAtual[i], (distanciaIntervalo * distanciaDeCriacao) / (velocidade / 2));
            }
            proximaDistancia += distanciaIntervalo;
        }

        // Não gerar bolas de lava se estiver muito perto de uma barreira.
        if (Physics.Raycast(transform.position, transform.forward, distanciaMinimaDaBarreira, layerBarreira))
        {
            closeToBarrier = true;
        }
        else
        {
            closeToBarrier = false;
            //Debug.DrawLine(transform.position, transform.position + transform.forward * distanciaMinimaDaBarreira, Color.white, 0.2f);
        }

        // Perder.
        if (vida <= 0 && lost == false)
        {
            lost = true;
            StartCoroutine(Lost());
        }
    }

    private IEnumerator IncreaseSpeed()
    {
        // Aumentar a velocidade automaticamente com o tempo.
        while (playing == false)
        {
            // Esperar até o jogador clicar no botão de iniciar.
            yield return new WaitForEndOfFrame();
        }

        yield return new WaitForSeconds(10f);

        if (takingDamage == false)
        {
            velocidade += 2;
            //Debug.Log("Aumentou a velocidade para " + velocidade);
        }

        StartCoroutine(IncreaseSpeed());
    }

    private IEnumerator Timer()
    {
        // Contar tempo.
        yield return new WaitForSeconds(1);

        if (playing == true)
        {
            if (takingDamage == false && _invulnerable == false)
            {
                timeSeconds++;
            }

            if (timeSeconds >= 60)
            {
                timeSeconds = 0;
                timeMinutes++;
            }
        }

        StartCoroutine(Timer());
    }

    private IEnumerator Lost()
    {
        lost = true;
        playing = false;

        int tempoTotal = timeSeconds + (timeMinutes * 60);
        int tempoTotalRecorde = timeSecondsRecord + (timeMinutesRecord * 60);

        if (tempoTotal > tempoTotalRecorde)
        {
            timeMinutesRecord = timeMinutes;
            timeSecondsRecord = timeSeconds;

            PlayerPrefs.SetInt("recorde_minutos", timeMinutesRecord);
            PlayerPrefs.SetInt("recorde_segundos", timeSecondsRecord);
            PlayerPrefs.Save();
        }

        Instantiate(textFimDeJogo, canvasObject.transform);

        yield return new WaitForSeconds(4f);

        CanvasManager.Instance.FadeIn();

        yield return new WaitForSeconds(1.1f);

        SceneManager.LoadScene("Jogo");
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Obstaculo" && collision.gameObject.GetComponent<Rigidbody>().constraints == RigidbodyConstraints.FreezeAll)
        {
            Instantiate(objetoTriggerAtivarFisica, transform.position, transform.rotation);
            if (takingDamage == false && _invulnerable == false)
            {
                StartCoroutine(TakeDamage());
            }
        }
    }

    private IEnumerator TakeDamage()
    {
        //Debug.Log("Levou dano");

        takingDamage = true;
        _invulnerable = true;
        
        ClickManager.tempoSegurandoMouse = 0f;
        scriptCameraShake.ShakeCamera(1.5f, 0.3f);
        velocidade *= 0.4f;
        vida--;

        Destroy(Instantiate(vidaEfeito, vidaIcone.transform), 1.4f);
        Destroy(Instantiate(danoOverlay, canvasObject.transform), 1.5f);
        Instantiate(danoEfeito, transform.position + (transform.forward * 2), new Quaternion(0, 0, 0, 0));

        AudioManager.Instance.PlaySFX(AudioManager.SFX.DroppingRocks);

        yield return new WaitForSeconds(1f);

        takingDamage = false;

        yield return new WaitForSeconds(2f);

        velocidade *= 1.5f;

        yield return new WaitForSeconds(1f);

        _invulnerable = false;
    }
}
