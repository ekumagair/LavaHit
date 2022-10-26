using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BolaDeLavaScript : MonoBehaviour
{
    public GameObject chama;

    Rigidbody rb;
    JogadorScript scriptDojogador;
    public bool impulsionado = false;

    void Start()
    {
        scriptDojogador = GameObject.FindGameObjectWithTag("Player").GetComponent<JogadorScript>();
        rb = GetComponent<Rigidbody>();
        rb.velocity = Vector3.up * Random.Range(9f, 12f) + Vector3.forward * scriptDojogador.velocidade;
        chama.SetActive(false);
    }

    void Update()
    {
        // Destruir o objeto se ele cair do mundo.
        if(transform.position.y < -10)
        {
            Destroy(gameObject);
        }
    }

    public void Clicou()
    {
        rb.velocity += Vector3.forward * (100f * DetectorDeClique.tempoSegurandoMouse);
        chama.SetActive(true);
        impulsionado = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Deletar")
        {
            Destroy(gameObject, 3f);
        }
    }
}
