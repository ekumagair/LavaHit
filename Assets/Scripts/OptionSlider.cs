using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class OptionSlider : MonoBehaviour
{
    [Header("Properties")]
    public AudioManager.SFX sfxOnDragEnd = AudioManager.SFX.ButtonMechanical;
    public float valueTextMultiplier;
    public string valueTextAppend;

    [Header("Components")]
    public TMP_Text valueText;
    public Slider slider;

    void Start()
    {
        slider.onValueChanged.AddListener(OnValueChanged);
    }

    void OnDestroy()
    {
        slider.onValueChanged.RemoveListener(OnValueChanged);
    }

    private void OnValueChanged(float value)
    {
        SetValueText();
    }

    public void SetValue(float value)
    {
        slider.value = value;
        SetValueText();
    }

    public void SetValueText()
    {
        valueText.text = Mathf.RoundToInt(slider.value * valueTextMultiplier).ToString() + valueTextAppend;
    }

    public void OnDragEnd()
    {
        AudioManager.Instance.PlaySFX(sfxOnDragEnd);
    }
}
