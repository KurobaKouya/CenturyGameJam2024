using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHPUI : MonoBehaviour
{
    public Slider slider;
    void Update()
    {
        slider.value = GameManager.Instance.gameData.playerHealth / 100f;
    }
}
