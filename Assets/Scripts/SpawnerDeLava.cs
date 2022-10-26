using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerDeLava : MonoBehaviour
{
    public GameObject objetoBola;
    public float tempoMin, tempoMax;
    public float xMin, xMax;

    void Start()
    {
        StartCoroutine(Criar());
    }

    IEnumerator Criar()
    {
        yield return new WaitForSeconds(Random.Range(tempoMin, tempoMax));

        Instantiate(objetoBola, transform.position + (new Vector3(Random.Range(xMin, xMax), 0, 0)), transform.rotation);

        StartCoroutine(Criar());
    }
}
