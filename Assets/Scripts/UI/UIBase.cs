using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIBase : MonoBehaviour
{
    protected CanvasGroup canvasGroup;
    protected bool isToggled = false;

    
    private void Start()
    {
        if (canvasGroup == null) canvasGroup = GetComponent<CanvasGroup>();
        canvasGroup.alpha = 0f;
        canvasGroup.blocksRaycasts = false;
        canvasGroup.interactable = false;
    }


    public virtual void Toggle() 
    {
        isToggled = !isToggled;
        canvasGroup.alpha = isToggled ? 1f : 0f;
        canvasGroup.blocksRaycasts = isToggled;
        canvasGroup.interactable = isToggled;
        Time.timeScale = isToggled ? 0f : 1f;
    }

    public virtual void SetActive(bool state)
    {
        isToggled = state;
        canvasGroup.alpha = isToggled ? 1f : 0f;
        canvasGroup.blocksRaycasts = isToggled;
        canvasGroup.interactable = isToggled;
        Time.timeScale = isToggled ? 0f : 1f;
    }
}
