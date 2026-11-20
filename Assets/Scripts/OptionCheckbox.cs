using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OptionCheckbox : MonoBehaviour
{
    [Header("Properties")]
    public AudioManager.SFX sfxOnClick = AudioManager.SFX.ButtonMechanical;

    [Header("Components")]
    public Toggle checkbox;

    void Start()
    {
        checkbox.onValueChanged.AddListener(OnValueChanged);
    }

    void OnDestroy()
    {
        checkbox.onValueChanged.RemoveListener(OnValueChanged);
    }

    private void OnValueChanged(bool value)
    {
        
    }

    public void SetValue(bool value)
    {
        checkbox.isOn = value;
    }

    public void OnClickSound()
    {
        AudioManager.Instance.PlaySFX(sfxOnClick);
    }
}
