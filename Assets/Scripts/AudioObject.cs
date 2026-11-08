using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioObject : MonoBehaviour
{
    public AudioClip[] clips;

    private AudioSource _audioSource;

    void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    public void PlayAudio()
    {
        StartCoroutine(PlayAudioC());
    }

    private IEnumerator PlayAudioC()
    {
        if (clips == null || clips.Length < 1)
        {
            Destroy(gameObject);
            yield break;
        }

        _audioSource.clip = clips[Random.Range(0, clips.Length)];
        _audioSource.Play();

        yield return new WaitForSeconds(_audioSource.clip.length);

        Destroy(gameObject);
    }
}
