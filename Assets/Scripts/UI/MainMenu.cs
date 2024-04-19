using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private Button StartBtn;
    [SerializeField] private Button TutBtn;
    [SerializeField] private Button QuitBtn;
    
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
        SceneManager.LoadSceneAsync((int)Globals.SceneIndex.Game, LoadSceneMode.Single);
        GameManager.Instance.currentScene = Globals.SceneIndex.Game;
    }


    private void ToggleTutorial()
    {

    }


    private void Quit()
    {
        Application.Quit();
    }
}
