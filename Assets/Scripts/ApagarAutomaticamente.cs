using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ApagarAutomaticamente : MonoBehaviour
{
    public float posZDeleteOffset = 0;

    Transform jogadorTransform;

    void Start()
    {
        jogadorTransform = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }

    void Update()
    {
        // Destruir o objeto se ele estiver atrás do jogador;
        if(transform.position.z < jogadorTransform.position.z - posZDeleteOffset)
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
