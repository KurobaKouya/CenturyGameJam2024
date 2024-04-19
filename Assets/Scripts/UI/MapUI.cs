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
        GameEvents.onPlayerHit += DisableMap;
    }


    private void OnDisable()
    {
        UIEvents.onToggleMap -= Toggle;
        GameEvents.onPlayerHit -= DisableMap;
    }

    public override void Toggle()
    {
        base.Toggle();
        GameManager.Instance.inMap = isToggled;
    }
    void DisableMap()
    {
        SetActive(false);
        GameManager.Instance.inMap = isToggled;
    }

    private void Update()
    {
        slider.value = GameManager.Instance.gameData.inkAmount / Globals.maxInk;
        
    }


}
