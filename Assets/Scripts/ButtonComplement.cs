using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonComplement : MonoBehaviour
{
    public AudioManager.SFX sfxOnClick = AudioManager.SFX.ButtonMechanical;

    public Button Button { private set; get; }

    void Awake()
    {
        Button = GetComponent<Button>();
    }

    void Start()
    {
        Button.onClick.AddListener(OnClick);
    }

    void OnEnable()
    {
        Button.animator.Play(Button.animationTriggers.normalTrigger);
    }

    void OnDestroy()
    {
        Button.onClick.RemoveListener(OnClick);
    }

    private void OnClick()
    {
        AudioManager.Instance.PlaySFX(sfxOnClick);
    }
}
