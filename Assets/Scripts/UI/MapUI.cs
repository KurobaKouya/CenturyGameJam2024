using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class MapUI : UIBase
{
    [SerializeField] Slider slider;
    
    private void OnEnable()
    {
        UIEvents.onToggleMap += Toggle;
    }


    private void OnDisable()
    {
        UIEvents.onToggleMap -= Toggle;
    }


    private void Update()
    {
        slider.value = GameManager.Instance.gameData.inkAmount / Globals.maxInk;
    }


}
