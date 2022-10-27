using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BolaDeLavaScript : MonoBehaviour
{
    public GameObject chama;
    public GameObject ativarFisicaTrigger;
    public GameObject criarNoImpacto;

    Rigidbody rb;
    JogadorScript scriptDojogador;
    float velocidadeDeImpulso;
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
        rb.velocity += Vector3.forward * (120f * DetectorDeClique.tempoSegurandoMouse);
        velocidadeDeImpulso = rb.velocity.magnitude;
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

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Obstaculo" && impulsionado == true)
        {
            Debug.Log("Colidiu com obstáculo");
            collision.gameObject.GetComponent<Rigidbody>().useGravity = true;
            collision.gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
            collision.gameObject.GetComponent<Rigidbody>().velocity += Vector3.forward;

            Instantiate(criarNoImpacto, transform.position, transform.rotation);

            if (velocidadeDeImpulso > 80)
            {
                Instantiate(ativarFisicaTrigger, transform.position, new Quaternion(0, 0, 0, 0));
            }
        }
    }
}
