using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class DeathScreen : MonoBehaviour
{
    [SerializeField] private GameObject video;

    [Header("In Between Variables")]
    [SerializeField] private GameObject hearts;
    [SerializeField] private Sprite heartImg;
    [SerializeField] private Sprite crossImg;
    [SerializeField] private Image heart1;
    [SerializeField] private Image heart2;
    [SerializeField] private Image heart3;

    [Header("True Death Variables")]
    [SerializeField] private GameObject youDied;
    [SerializeField] private Button restartBtn;
    [SerializeField] private Button menuBtn;



    void Start()
    {
        video.SetActive(false);
        hearts.SetActive(true);
        youDied.SetActive(false);
    }


    private void OnEnable()
    {
        GameEvents.onPlayerDeath += EnableScreen;
        restartBtn.onClick.AddListener(Restart);
        menuBtn.onClick.AddListener(Menu);
    }


    private void OnDisable()
    {
        GameEvents.onPlayerDeath -= EnableScreen;
        restartBtn.onClick.RemoveAllListeners();
        menuBtn.onClick.RemoveAllListeners();
    }


    private void EnableScreen()
    {
        video.SetActive(true);
        StartCoroutine(UpdateScreen());
    }

    IEnumerator UpdateScreen()
    {
        yield return new WaitForSeconds(1);
        switch (GameManager.Instance.gameData.lives)
        {
            case 2:
            heart3.sprite = crossImg;
            break;
            case 1:
            heart2.sprite = crossImg;
            break;
            case 0:
            heart1.sprite = crossImg;
            break;
        }
        yield return new WaitForSeconds(3);
        if (GameManager.Instance.gameData.lives <= 0) Death();
        else
        video.SetActive(false);
    }


    private void Death()
    {
        GameEvents.onPlayerDeath -= EnableScreen;
        // Enable Death Objects
        youDied.SetActive(true);
        hearts.SetActive(false);
    }


    private void Restart()
    {
        GameManager.Instance.gameData = new();
        EnemyManager.Instance.enemyList = new();
        SceneManager.LoadSceneAsync((int)Globals.SceneIndex.Game, LoadSceneMode.Single);
        GameManager.Instance.currentScene = Globals.SceneIndex.Game;
    }


    private void Menu()
    {
        SceneManager.LoadSceneAsync((int)Globals.SceneIndex.MainMenu, LoadSceneMode.Single);
        GameManager.Instance.currentScene = Globals.SceneIndex.MainMenu;
    }
}
