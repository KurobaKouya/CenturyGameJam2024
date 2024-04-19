using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class MapUI : UIBase
{
    [SerializeField] Slider slider;
    [SerializeField] AudioClipInstance openMapAud, closeMapAud;


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
        isToggled = !isToggled;
        canvasGroup.alpha = isToggled ? 1f : 0f;
        canvasGroup.blocksRaycasts = isToggled;
        canvasGroup.interactable = isToggled;
        GameManager.Instance.inMap = isToggled;
        AudioManager.instance.PlaySourceAudio(isToggled ? openMapAud : closeMapAud);
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
