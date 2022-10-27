using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class JogadorScript : MonoBehaviour
{
    public float velocidade = 8f;
    public int vida;
    public CameraShake scriptCameraShake;
    public LayerMask layerBarreira;
    public float distanciaMinimaDaBarreira;
    public GameObject objetoTriggerAtivarFisica;

    [Header("Criação do cenário")]
    public GameObject chaoPrefab;
    public GameObject[] chaoAtual;
    public float proximaDistancia = 0;
    public float distanciaIntervalo;
    public int distanciaDeCriacao = 2;

    public static bool levandoDano = false;
    public static bool pertoDeBarreira = false;

    void Start()
    {
        StartCoroutine(AumentarVelocidade());
    }

    void Update()
    {
        if (CanvasScript.jogando == true && vida > 0)
        {
            transform.Translate(Vector3.forward * velocidade * Time.deltaTime);
        }

        if(transform.position.z > proximaDistancia)
        {
            for (int i = 0; i < distanciaDeCriacao; i++)
            {
                chaoAtual[i] = Instantiate(chaoPrefab, new Vector3(0, 0, proximaDistancia + (distanciaIntervalo * (i + 1))), transform.rotation);
                Destroy(chaoAtual[i], (distanciaIntervalo * distanciaDeCriacao) / (velocidade / 2));
            }
            proximaDistancia += distanciaIntervalo;
        }

        if(Physics.Raycast(transform.position, transform.forward, distanciaMinimaDaBarreira, layerBarreira))
        {
            pertoDeBarreira = true;
        }
        else
        {
            pertoDeBarreira = false;
            Debug.DrawLine(transform.position, transform.position + transform.forward * distanciaMinimaDaBarreira, Color.white, 0.2f);
        }
    }

    IEnumerator AumentarVelocidade()
    {
        while (CanvasScript.jogando == false)
        {
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

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Obstaculo" && collision.gameObject.GetComponent<Rigidbody>().constraints == RigidbodyConstraints.FreezeAll)
        {
            Instantiate(objetoTriggerAtivarFisica, transform.position, transform.rotation);
            if (levandoDano == false)
            {
                StartCoroutine(LevarDano());
            }
        }
    }

    IEnumerator LevarDano()
    {
        levandoDano = true;
        Debug.Log("Levou dano");
        scriptCameraShake.ShakeCamera(1.5f, 0.4f);
        velocidade *= 0.4f;
        vida--;

        yield return new WaitForSeconds(1f);

        levandoDano = false;

        yield return new WaitForSeconds(2f);

        velocidade *= 1.5f;
    }
}
