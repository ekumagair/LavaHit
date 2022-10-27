using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerDeLava : MonoBehaviour
{
    public GameObject objetoBola;
    public float tempoMin, tempoMax;
    public float xMin, xMax;

    JogadorScript scriptJogador;

    void Start()
    {
        scriptJogador = GameObject.FindGameObjectWithTag("Player").GetComponent<JogadorScript>();
        StartCoroutine(Criar());
    }

    IEnumerator Criar()
    {
        yield return new WaitForSeconds(Random.Range(tempoMin, tempoMax));

        if (JogadorScript.levandoDano == false && JogadorScript.pertoDeBarreira == false && CanvasScript.jogando == true && scriptJogador.vida > 0)
        {
            Instantiate(objetoBola, transform.position + (new Vector3(Random.Range(xMin, xMax), 0, 0)), transform.rotation);
        }

        StartCoroutine(Criar());
    }
}
