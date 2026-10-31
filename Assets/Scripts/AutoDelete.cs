using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoDelete : MonoBehaviour
{
    public float posZDeleteOffset = 0;

    private Transform _playerTransform;

    void Start()
    {
        _playerTransform = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }

    void Update()
    {
        // Destruir o objeto se ele estiver atrás do jogador;
        if (transform.position.z < _playerTransform.position.z - posZDeleteOffset)
        {
            Destroy(gameObject);
        }

        // Destruir o objeto se ele cair do mundo.
        if (transform.position.y < -10)
        {
            Destroy(gameObject);
        }
    }
}
