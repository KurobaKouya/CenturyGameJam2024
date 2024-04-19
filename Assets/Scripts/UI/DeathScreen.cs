using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DeathScreen : MonoBehaviour
{
    [SerializeField] private GameObject video;
    [SerializeField] private Sprite heartImg;
    [SerializeField] private Sprite crossImg;
    [SerializeField] private Image heart1;
    [SerializeField] private Image heart2;
    [SerializeField] private Image heart3;



    void Start()
    {
        video.SetActive(false);
    }


    private void OnEnable()
    {
        GameEvents.onPlayerDeath += EnableScreen;
    }


    private void OnDisable()
    {
        GameEvents.onPlayerDeath -= EnableScreen;
    }


    private void EnableScreen()
    {
        if (GameManager.Instance.gameData.lives > 0)
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
        }
        yield return new WaitForSeconds(3);
        video.SetActive(false);
    }
}
