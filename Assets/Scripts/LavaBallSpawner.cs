using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LavaBallSpawner : MonoBehaviour
{
    public GameObject objetoBola;
    public float tempoMin, tempoMax;
    public float xMin, xMax;

    private PlayerScript _playerScript;

    void Start()
    {
        _playerScript = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerScript>();
        StartCoroutine(Criar());
    }

    private IEnumerator Criar()
    {
        // Criar bolas de lava, com base no tempo aleatório e na velocidade do jogador.
        yield return new WaitForSeconds(Random.Range(tempoMin, tempoMax) / (_playerScript.velocidade / 9));

        if (PlayerScript.takingDamage == false && PlayerScript.closeToBarrier == false && PlayerScript.playing == true && _playerScript.vida > 0)
        {
            Instantiate(objetoBola, transform.position + (new Vector3(Random.Range(xMin, xMax), 0, 0)), transform.rotation);
        }

        StartCoroutine(Criar());
    }
}
