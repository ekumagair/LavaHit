using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioObject[] audioObjects;

    public static AudioManager Instance { private set; get; }

    public enum SFX
    {
        None = -1,
        StonesFalling = 0,
        ButtonMechanical = 1,
        DroppingRocks = 2,
        ImpactStone = 3,
    }

    void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }

    public void PlaySFX(SFX sfx)
    {
        if (sfx == SFX.None)
        {
            return;
        }

        PlaySFXAtPosition(sfx, Camera.main.transform.position);
    }

    public void PlaySFXAtPosition(SFX sfx, Vector3 pos)
    {
        var sndInstance = Instantiate(audioObjects[(int)sfx]);
        sndInstance.transform.SetParent(transform);
        sndInstance.transform.position = pos;
        sndInstance.PlayAudio();
    }
}
