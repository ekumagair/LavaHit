using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    private Transform cameraParent;

    private float _shakeDuration = 1f;
    private float _shakeAmount = 0.5f;
    private float _shakeTimer = 0;

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
        if (_shakeTimer <= 0)
        {
            _shakeDuration = duration;
            _shakeAmount = amount;
            _shakeTimer = _shakeDuration;
        }
    }

    void CheckEffect()
    {
        if (_shakeTimer > 0)
        {
            transform.position = cameraParent.position + Random.insideUnitSphere * _shakeAmount;
            _shakeTimer -= Time.deltaTime;
        }
        else
        {
            _shakeTimer = 0f;
            transform.position = cameraParent.position;
        }
    }
}
