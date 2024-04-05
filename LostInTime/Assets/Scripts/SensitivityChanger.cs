using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SensitivityChanger : MonoBehaviour
{
    private void Start()
    {
        Slider slider = GetComponent<Slider>();
        slider.value = GameManager.instance.GetCurrentMouseValue();
    }

    public void SetSensitivity(float x)
    {
        GameManager.instance.SetMouseSensitivity(x);
    }
}
