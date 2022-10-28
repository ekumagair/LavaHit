using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CriarBarreiras : MonoBehaviour
{
    public GameObject[] barreiraPrefab;
    public float distancia;
    public float delay;

    GameObject jogador;
    JogadorScript jogadorScript;

    void Start()
    {
        jogador = GameObject.FindGameObjectWithTag("Player");
        jogadorScript = jogador.GetComponent<JogadorScript>();
        StartCoroutine(Criar());
    }

    IEnumerator Criar()
    {
        while(CanvasScript.jogando == false)
        {
            // Esperar até o jogador clicar no botão de iniciar.
            yield return new WaitForEndOfFrame();
        }

        Instantiate(barreiraPrefab[Random.Range(0, barreiraPrefab.Length)], new Vector3(0, 0, jogador.transform.position.z + distancia), transform.rotation);

        yield return new WaitForSeconds(delay / (jogadorScript.velocidade / 3));

        StartCoroutine(Criar());
    }
}
