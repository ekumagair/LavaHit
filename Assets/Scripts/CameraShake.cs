using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    Transform cameraParent;

    float shakeDuracao = 1f;
    float shakeQuantidade = 0.5f;

    float shakeTempoAtual = 0;

    void Start()
    {
        cameraParent = transform.parent;
    }

    void Update()
    {
        Efeito();
    }

    public void ShakeCamera(float duration, float amount)
    {
        if (shakeTempoAtual <= 0)
        {
            shakeDuracao = duration;
            shakeQuantidade = amount;
            shakeTempoAtual = shakeDuracao;
        }
    }

    void Efeito()
    {
        if (shakeTempoAtual > 0)
        {
            transform.position = cameraParent.position + Random.insideUnitSphere * shakeQuantidade;
            shakeTempoAtual -= Time.deltaTime;
        }
        else
        {
            shakeTempoAtual = 0f;
            transform.position = cameraParent.position;
        }
    }
}
