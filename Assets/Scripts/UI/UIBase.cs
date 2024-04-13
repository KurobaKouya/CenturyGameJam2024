using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIBase : MonoBehaviour
{
    private CanvasGroup canvasGroup;
    protected bool isToggled = false;

    
    private void Start()
    {
        if (canvasGroup == null) canvasGroup = GetComponent<CanvasGroup>();
        canvasGroup.alpha = 0f;
        canvasGroup.blocksRaycasts = false;
    }


    public virtual void Toggle() 
    {
        isToggled = !isToggled;
        // Time.timeScale = isToggled ? 0f : 1f;
        canvasGroup.alpha = isToggled ? 1f : 0f;
        canvasGroup.blocksRaycasts = isToggled;
    }
}
