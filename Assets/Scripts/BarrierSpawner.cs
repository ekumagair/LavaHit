using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarrierSpawner : MonoBehaviour
{
    public GameObject[] barreiraPrefab;
    public float distancia;
    public float delay;

    private GameObject _playerObj;
    private PlayerScript _playerScript;

    void Start()
    {
        _playerObj = GameObject.FindGameObjectWithTag("Player");
        _playerScript = _playerObj.GetComponent<PlayerScript>();
        StartCoroutine(Criar());
    }

    IEnumerator Criar()
    {
        while (PlayerScript.playing == false)
        {
            // Esperar até o jogador clicar no botão de iniciar.
            yield return new WaitForEndOfFrame();
        }

        Instantiate(barreiraPrefab[Random.Range(0, barreiraPrefab.Length)], new Vector3(0, 0, _playerObj.transform.position.z + distancia), transform.rotation);

        yield return new WaitForSeconds(delay / (_playerScript.velocidade / 3));

        StartCoroutine(Criar());
    }
}
