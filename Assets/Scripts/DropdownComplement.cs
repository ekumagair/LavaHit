using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(TMP_Dropdown))]
public class DropdownComplement : MonoBehaviour
{
    public AudioManager.SFX sfxOnClick = AudioManager.SFX.ButtonMechanical;

    public TMP_Dropdown Dropdown { private set; get; }

    void Awake()
    {
        Dropdown = GetComponent<TMP_Dropdown>();
    }

    void Start()
    {
        Dropdown.onValueChanged.AddListener(OnValueChange);
    }

    void OnDestroy()
    {
        Dropdown.onValueChanged.RemoveListener(OnValueChange);
    }

    private void OnValueChange(int value)
    {
        OnClick();
    }

    public void OnClick()
    {
        AudioManager.Instance.PlaySFX(sfxOnClick);
    }
}
