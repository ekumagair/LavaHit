using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LavaBall : MonoBehaviour
{
    public GameObject chama;
    public GameObject ativarFisicaTrigger;
    public GameObject criarNoImpacto;
    public bool impulsionado = false;

    private Rigidbody _rigidBody;
    private PlayerScript _playerScript;
    private float _impulseStrength;

    void Start()
    {
        _playerScript = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerScript>();
        _rigidBody = GetComponent<Rigidbody>();
        _rigidBody.velocity = Vector3.up * Random.Range(9f, 12f) + Vector3.forward * _playerScript.velocidade;

        chama.SetActive(false);
    }

    public void Clicked()
    {
        // Bola foi clicada. Mudar velocidade com base no tempo que o jogador segurou o botão do mouse.
        _rigidBody.velocity += Vector3.forward * (120f * ClickManager.tempoSegurandoMouse);
        _impulseStrength = _rigidBody.velocity.magnitude;
        chama.SetActive(true);
        impulsionado = true;

        AudioManager.Instance.PlaySFXAtPosition(AudioManager.SFX.ImpactStone, transform.position);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Deletar")
        {
            Destroy(gameObject, 3f);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Obstaculo" && impulsionado == true)
        {
            //Debug.Log("Colidiu com obstáculo");

            collision.gameObject.GetComponent<Rigidbody>().useGravity = true;
            collision.gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
            collision.gameObject.GetComponent<Rigidbody>().velocity += Vector3.forward;

            Instantiate(criarNoImpacto, transform.position, transform.rotation);

            if (_impulseStrength > 80)
            {
                // Se a bola for mais rápida, é mais efetiva.
                Instantiate(ativarFisicaTrigger, transform.position, new Quaternion(0, 0, 0, 0));
                AudioManager.Instance.PlaySFXAtPosition(AudioManager.SFX.StonesFalling, transform.position);
                _impulseStrength = 0f;

                if (PlayerScript.takingDamage == false)
                {
                    _playerScript.scriptCameraShake.ShakeCamera(0.15f, 0.15f);
                }
            }
        }
    }
}
