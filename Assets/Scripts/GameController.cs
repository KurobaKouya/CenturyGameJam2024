using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    private GameManager gameManager;
    private InputManager inputManager;


    private void Awake()
    {
        // Persist through scenes
        DontDestroyOnLoad(this);

        // Obtain references
        gameManager = GetComponentInChildren<GameManager>();
        inputManager = GetComponentInChildren<InputManager>();


        // Initialize
        // ...
        gameManager.Init();
        inputManager.Init();



        // Switch to Menu/Game Scene
        SceneManager.LoadSceneAsync((int)Globals.SceneIndex.MainMenu, LoadSceneMode.Single);
        GameManager.Instance.currentScene = Globals.SceneIndex.MainMenu;
    }


    private void Update()
    {
        gameManager.UpdateLoop();
        inputManager.UpdateLoop();
    }
}
