using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class FlashlightUI : MonoBehaviour
{
    public Slider slider;
    public Light light;

    private void Update()
    {
        slider.value = GameManager.Instance.gameData.flashlightPower / 100f;
        light.intensity = slider.value;
    }
}
