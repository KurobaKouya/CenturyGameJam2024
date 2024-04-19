using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private Button StartBtn;
    [SerializeField] private Button TutBtn;
    [SerializeField] private Button QuitBtn;
    [SerializeField] private Tutorial tutGO;
    
    private void OnEnable()
    {
        StartBtn.onClick.AddListener(StartGame);
        TutBtn.onClick.AddListener(ToggleTutorial);
        QuitBtn.onClick.AddListener(Quit);
    }


    private void OnDisable()
    {
        StartBtn.onClick.RemoveAllListeners();
        TutBtn.onClick.RemoveAllListeners();
        QuitBtn.onClick.RemoveAllListeners();
    }


    private void StartGame()
    {
        // Switch to Menu/Game Scene
        GameManager.Instance.gameData = new();
        SceneManager.LoadSceneAsync((int)Globals.SceneIndex.Game, LoadSceneMode.Single);
        GameManager.Instance.currentScene = Globals.SceneIndex.Game;
    }


    private void ToggleTutorial()
    {
        tutGO.Toggle();
    }


    private void Quit()
    {
        Application.Quit();
    }
}
