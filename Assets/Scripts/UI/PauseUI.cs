using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseUI : MonoBehaviour
{
    [SerializeField] private Button resumeBtn;
    [SerializeField] private Button restartBtn;
    [SerializeField] private Button menuBtn;
    [SerializeField] private GameObject holder;


    private void Start()
    {
        holder.SetActive(false);
    }


    private void OnEnable()
    {
        // UIEvents.onTogglePause += Toggle;
        resumeBtn.onClick.AddListener(ToggleUI);
        restartBtn.onClick.AddListener(Restart);
        menuBtn.onClick.AddListener(ReturnToMenu);
    }


    private void OnDisable()
    {
        // UIEvents.onTogglePause -= Toggle;
        resumeBtn.onClick.RemoveAllListeners();
        restartBtn.onClick.RemoveAllListeners();
        menuBtn.onClick.RemoveAllListeners();
    }


    private void Update()
    {
        if (GameManager.Instance.currentScene != Globals.SceneIndex.Game) return;
        if (Input.GetKeyDown(KeyCode.Escape)) ToggleUI();
    }


    private void ToggleUI()
    {
        // isToggled = !isToggled;
        // canvasGroup.alpha = isToggled ? 1f : 0f;
        // canvasGroup.blocksRaycasts = isToggled;
        // canvasGroup.interactable = isToggled;
        holder.SetActive(!holder.activeInHierarchy);
    }


    // public override void Toggle()
    // {
    //     isToggled = true;
    //     canvasGroup.alpha = 1f;
    //     canvasGroup.blocksRaycasts = true;
    //     canvasGroup.interactable = true;
    // }


    // private void Resume()
    // {
    //     // isToggled = false;
    //     // canvasGroup.alpha = 0f;
    //     // canvasGroup.blocksRaycasts = false;
    //     // ToggleUI();
    //     isToggled = false;
    //     canvasGroup.alpha = 0f;
    //     canvasGroup.blocksRaycasts = false;
    //     canvasGroup.interactable = false;
    // }


    private void Restart()
    {
        GameManager.Instance.gameData = new();
        EnemyManager.Instance.enemyList = new();
        SceneManager.LoadSceneAsync((int)Globals.SceneIndex.Game, LoadSceneMode.Single);
        GameManager.Instance.currentScene = Globals.SceneIndex.Game;
        holder.SetActive(false);
    }


    private void ReturnToMenu()
    {
        SceneManager.LoadSceneAsync((int)Globals.SceneIndex.MainMenu, LoadSceneMode.Single);
        GameManager.Instance.currentScene = Globals.SceneIndex.MainMenu;
        holder.SetActive(false);
    }
}
