using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class OptionDropdown : MonoBehaviour
{
    [Header("Components")]
    public TMP_Dropdown dropdown;

    public void SetValue(int index)
    {
        dropdown.SetValueWithoutNotify(index);
    }
}
