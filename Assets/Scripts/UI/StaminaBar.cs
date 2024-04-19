using System.Collections;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class StaminaBar : MonoBehaviour
{
    [SerializeField] private float hideSpeed = 1;
    [SerializeField] private float showSpeed = 0.5f;
    [SerializeField] private Image mask;
    private CanvasGroup canvasGroup = null;
    private bool isHidden = true;


    private void OnEnable()
    {
        if (canvasGroup == null) canvasGroup = GetComponent<CanvasGroup>();
        mask.fillAmount = 100;
        canvasGroup.alpha = 0;
    }


    private void Update()
    {
        if (GameManager.Instance.currentScene != Globals.SceneIndex.Game) return;
        if (mask == null) return;

        mask.fillAmount = GameManager.Instance.player.stamina / 100;
        
        if (GameManager.Instance.player.stamina >= 100) HideUI();
        else canvasGroup.DOFade(1, showSpeed);
    }


    private void HideUI()
    {
        canvasGroup.DOFade(0, hideSpeed);
        if (canvasGroup.alpha <= 0.1f) canvasGroup.alpha = 0;
    }
}
