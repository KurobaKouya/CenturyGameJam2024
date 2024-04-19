using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseUI : UIBase
{
    [SerializeField] private Button resumeBtn;
    [SerializeField] private Button restartBtn;
    [SerializeField] private Button menuBtn;


    private void OnEnable()
    {
        UIEvents.onTogglePause += Toggle;
        resumeBtn.onClick.AddListener(Resume);
        restartBtn.onClick.AddListener(Restart);
        menuBtn.onClick.AddListener(ReturnToMenu);
    }


    private void OnDisable()
    {
        UIEvents.onTogglePause -= Toggle;
        resumeBtn.onClick.RemoveAllListeners();
        restartBtn.onClick.RemoveAllListeners();
        menuBtn.onClick.RemoveAllListeners();
    }


    public override void Toggle()
    {
        isToggled = true;
        canvasGroup.alpha = 1f;
        canvasGroup.blocksRaycasts = true;
    }


    private void Resume()
    {
        isToggled = false;
        canvasGroup.alpha = 0f;
        canvasGroup.blocksRaycasts = false;
    }


    private void Restart()
    {

    }


    private void ReturnToMenu()
    {
        SceneManager.LoadSceneAsync((int)Globals.SceneIndex.MainMenu, LoadSceneMode.Single);
        GameManager.Instance.currentScene = Globals.SceneIndex.MainMenu;
    }
}
