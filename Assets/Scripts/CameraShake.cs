using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    private Transform cameraParent;

    private float shakeDuracao = 1f;
    private float shakeQuantidade = 0.5f;

    private float shakeTempoAtual = 0;

    void Start()
    {
        cameraParent = transform.parent;
    }

    void Update()
    {
        CheckEffect();
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

    void CheckEffect()
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
