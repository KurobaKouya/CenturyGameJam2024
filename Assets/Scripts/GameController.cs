using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    private GameManager gameManager;
    private InputManager inputManager;
    private EnemyManager enemyManager;


    private void Awake()
    {
        // Persist through scenes
        DontDestroyOnLoad(this);

        // Obtain references
        gameManager = GetComponentInChildren<GameManager>();
        inputManager = GetComponentInChildren<InputManager>();
        enemyManager = GetComponentInChildren<EnemyManager>();


        // Initialize
        // ...
        gameManager.Init();
        inputManager.Init();
        enemyManager.Init();



        // Switch to Menu/Game Scene
        SceneManager.LoadSceneAsync((int)Globals.SceneIndex.Game, LoadSceneMode.Single);
    }


    private void Update()
    {
        gameManager.UpdateLoop();
        inputManager.UpdateLoop();
        enemyManager.UpdateLoop();
    }
}
