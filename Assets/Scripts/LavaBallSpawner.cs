using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LavaBallSpawner : MonoBehaviour
{
    public GameObject objetoBola;
    public float tempoMin, tempoMax;
    public float xMin, xMax;

    PlayerScript scriptJogador;

    void Start()
    {
        scriptJogador = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerScript>();
        StartCoroutine(Criar());
    }

    private IEnumerator Criar()
    {
        // Criar bolas de lava, com base no tempo aleatório e na velocidade do jogador.
        yield return new WaitForSeconds(Random.Range(tempoMin, tempoMax) / (scriptJogador.velocidade / 9));

        if (PlayerScript.levandoDano == false && PlayerScript.pertoDeBarreira == false && CanvasScript.jogando == true && scriptJogador.vida > 0)
        {
            Instantiate(objetoBola, transform.position + (new Vector3(Random.Range(xMin, xMax), 0, 0)), transform.rotation);
        }

        StartCoroutine(Criar());
    }
}
