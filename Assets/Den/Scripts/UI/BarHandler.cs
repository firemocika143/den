using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BarHandler : MonoBehaviour
{
    [SerializeField]
    private Slider slider;

    private void Start()
    {
        slider = GetComponent<Slider>();
    }

    public void SetMax(int max, bool updateValue = true)
    {
        slider.maxValue = max;

        if (updateValue) slider.value = max;
    }

    public void SetValue(int val)
    {
        slider.value = val;
    }
}
