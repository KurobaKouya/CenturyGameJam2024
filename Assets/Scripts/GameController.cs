using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    private GameManager gameManager;
    private InputManager inputManager;


    private void Start()
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
        SceneManager.LoadSceneAsync((int)Globals.SceneIndex.Game, LoadSceneMode.Single);
    }


    private void Update()
    {
        gameManager.UpdateLoop();
        inputManager.UpdateLoop();
    }
}
