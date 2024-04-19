using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Tutorial : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private CanvasGroup canvasGroup;
    [SerializeField] private TextMeshProUGUI row1Text;
    [SerializeField] private TextMeshProUGUI row2Text;
    [SerializeField] private Image row1Image;
    [SerializeField] private Image row2Image;
    [SerializeField] private Button nextBtn;
    [SerializeField] private Button prevBtn;


    [Header("Images to replace")]
    [SerializeField] private Sprite page0row1;
    [SerializeField] private Sprite page0row2;
    [SerializeField] private Sprite page1row1;
    [SerializeField] private Sprite page1row2;
    [SerializeField] private Sprite page2row1;
    [SerializeField] private Sprite page2row2;
    [SerializeField] private Sprite page3row1;
    [SerializeField] private Sprite page3row2;
    [SerializeField] private Sprite page4row1;
    [SerializeField] private Sprite page4row2;


    private int pageNo = 0;
    private bool isToggled = false;
    

    private void Next()
    {
        pageNo += 1;
        switch (pageNo)
        {
            case 1:
            row1Text.text = "USE THE MAP (M) TO DRAW OUT YOUR PATHS";
            row1Image.sprite = page1row1;
            row2Text.text = "PATHS ARE SAFE AREAS WHERE MONSTERS CANNOT ENTER";
            row2Image.sprite = page1row2;
            break;
            case 2:
            row1Text.text = "WHEN YOU VENTURE OUT OF THE PATH, MONSTERS WILL START ATTACKING YOU";
            row1Image.sprite = page2row1;
            row2Text.text = "USE THE AXE AND KILL THEM TO GAIN INK TO DRAW MORE PATHS";
            row2Image.sprite = page2row2;
            break;
            case 3:
            row1Text.text = "IF YOU GET HIT BY THE MONSTER, YOUR FLASHLIGHT BATTERY DEPLETES";
            row1Image.sprite = page3row1;
            row2Text.text = "STAYING IN THE PATHS WILL RECHARGE YOUR FLASHLIGHT";
            row2Image.sprite = page3row2;
            break;
            case 4: 
            row1Text.text = "ONCE FLASHLIGHT DEPLETES, YOU WILL START TO LOSE VISION WHEN MONSTERS ATTACK YOU";
            row1Image.sprite = page4row1;
            row2Text.text = "YOU WILL DIE ONCE YOUR VISION IS GONE";
            row2Image.sprite = page4row2;
            break;
            case 5:
            Toggle();
            break;
        }
    }


    private void Prev()
    {
        pageNo -= 1;
        switch (pageNo)
        {
            case 0:
            row1Text.text = "YOUR GOAL IS TO COLLECT 5 RELICS";
            row1Image.sprite = page0row1;
            row2Text.text = "RELICS MUST BE PLACED IN THE SUMMONING CIRCLE";
            row2Image.sprite = page0row2;
            break;
            case 1:
            row1Text.text = "USE THE MAP (M) TO DRAW OUT YOUR PATHS";
            row1Image.sprite = page1row1;
            row2Text.text = "PATHS ARE SAFE AREAS WHERE MONSTERS CANNOT ENTER";
            row2Image.sprite = page1row2;
            break;
            case 2:
            row1Text.text = "WHEN YOU VENTURE OUT OF THE PATH, MONSTERS WILL START ATTACKING YOU";
            row1Image.sprite = page2row1;
            row2Text.text = "USE THE AXE AND KILL THEM TO GAIN INK TO DRAW MORE PATHS";
            row2Image.sprite = page2row2;
            break;
            case 3:
            row1Text.text = "IF YOU GET HIT BY THE MONSTER, YOUR FLASHLIGHT BATTERY DEPLETES";
            row1Image.sprite = page3row1;
            row2Text.text = "STAYING IN THE PATHS WILL RECHARGE YOUR FLASHLIGHT";
            row2Image.sprite = page3row2;
            break;
            case -1:
            Toggle();
            break;
        }
    }


    private void Start()
    {
        canvasGroup.alpha = 0f;
        canvasGroup.blocksRaycasts = false;
        canvasGroup.interactable = false;
    }


    public void Toggle()
    {
        isToggled = !isToggled;
        canvasGroup.alpha = isToggled ? 1f : 0f;
        canvasGroup.blocksRaycasts = isToggled;
        canvasGroup.interactable = isToggled;
        
        pageNo = 0;
        row1Text.text = "YOUR GOAL IS TO COLLECT 5 RELICS";
        row1Image.sprite = page0row1;
        row2Text.text = "RELICS MUST BE PLACED IN THE SUMMONING CIRCLE";
        row2Image.sprite = page0row2;
    }


    private void OnEnable()
    {
        nextBtn.onClick.AddListener(Next);
        prevBtn.onClick.AddListener(Prev);
    }


    private void OnDisable()
    {
        nextBtn.onClick.RemoveAllListeners();
        prevBtn.onClick.RemoveAllListeners();
    }
}
