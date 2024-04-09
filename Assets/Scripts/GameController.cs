using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    [Header("Managers")]
    [SerializeField] private GameManager gameManager;

    private void Start()
    {
        // Persist through scenes
        DontDestroyOnLoad(this);


        // Initialize things
        // ...
        if (gameManager != null) gameManager.Init();


        // Switch to Menu/Game Scene
        SceneManager.LoadSceneAsync((int)Globals.SceneIndex.Game, LoadSceneMode.Single);
    }
}
