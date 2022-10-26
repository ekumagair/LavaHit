using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JogadorScript : MonoBehaviour
{
    public float velocidade = 8f;
    public GameObject chaoPrefab;
    public GameObject[] chaoAtual;

    public float proximaDistancia = 0;
    public float distanciaIntervalo;
    public int distanciaDeCriacao = 2;

    void Update()
    {
        transform.Translate(Vector3.forward * velocidade * Time.deltaTime);

        if(transform.position.z > proximaDistancia)
        {
            for (int i = 0; i < distanciaDeCriacao; i++)
            {
                chaoAtual[i] = Instantiate(chaoPrefab, new Vector3(0, 0, proximaDistancia + (distanciaIntervalo * (i + 1))), transform.rotation);
                Destroy(chaoAtual[i], (distanciaIntervalo * distanciaDeCriacao) / velocidade);
            }
            proximaDistancia += distanciaIntervalo;
        }
    }
}
