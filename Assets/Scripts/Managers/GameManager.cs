using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    [Header("Variables")]
    [SerializeField] private Player player = null;


    public void Init()
    {

    }


    public void UpdateLoop()
    {
        if (player) player.UpdateLoop();
    }


    #region GlobalEvents
    private void OnEnable()
    {
        GlobalEvents.onPlayerEnabled += (sender) => player = sender;
    }


    private void OnDisable()
    {
        GlobalEvents.onPlayerEnabled -= (sender) => player = sender;
    }
    #endregion
}
