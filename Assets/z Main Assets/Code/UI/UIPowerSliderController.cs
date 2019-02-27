using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIPowerSliderController : MonoBehaviour
{
    [SerializeField]
    private Slider powerSlider;

    public static UIPowerSliderController instance;

    private void Awake()
    {
        instance = this;
    }

    public void UpdatePowerBar(float powerLevel)
    {
        powerSlider.value = powerLevel;
    }
}
