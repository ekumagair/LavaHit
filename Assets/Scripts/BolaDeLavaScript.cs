using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BolaDeLavaScript : MonoBehaviour
{
    public GameObject chama;
    public GameObject ativarFisicaTrigger;
    public GameObject criarNoImpacto;
    public AudioClip[] impulsoClip;
    public AudioClip[] impactoClip;

    Rigidbody rb;
    AudioSource audioSource;
    JogadorScript scriptDojogador;
    float velocidadeDeImpulso;
    public bool impulsionado = false;

    void Start()
    {
        scriptDojogador = GameObject.FindGameObjectWithTag("Player").GetComponent<JogadorScript>();
        rb = GetComponent<Rigidbody>();
        rb.velocity = Vector3.up * Random.Range(9f, 12f) + Vector3.forward * scriptDojogador.velocidade;
        audioSource = GetComponent<AudioSource>();
        chama.SetActive(false);
    }

    public void Clicou()
    {
        // Bola foi clicada. Mudar velocidade com base no tempo que o jogador segurou o botão do mouse.
        rb.velocity += Vector3.forward * (120f * DetectorDeClique.tempoSegurandoMouse);
        velocidadeDeImpulso = rb.velocity.magnitude;
        chama.SetActive(true);
        impulsionado = true;

        audioSource.PlayOneShot(impulsoClip[Random.Range(0, impulsoClip.Length)]);
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
                // Se a bola for mais rápida, é mais efetiva.
                Instantiate(ativarFisicaTrigger, transform.position, new Quaternion(0, 0, 0, 0));
                audioSource.PlayOneShot(impactoClip[Random.Range(0, impactoClip.Length)], 0.1f);
                velocidadeDeImpulso = 0f;

                if(JogadorScript.levandoDano == false)
                {
                    scriptDojogador.scriptCameraShake.ShakeCamera(0.15f, 0.15f);
                }
            }
        }
    }
}
