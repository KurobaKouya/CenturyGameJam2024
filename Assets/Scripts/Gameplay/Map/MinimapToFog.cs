using FischlWorks_FogWar;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using static UnityEditor.Progress;

[Serializable]
public class NoFogPosition
{
    public Vector3 position;
    public Vector2Int levelCoordinates;
    public float startSize, timeBeforeDecay, decayTime;
    public float currentSize;
    public float time;
    public GameObject gameObject;
    public NoFogPosition(Vector3 pos, float startSize, float timeBeforeDecay, float decayTime, GameObject gameObject)
    {
        position = pos;
        this.startSize = startSize;
        this.currentSize = startSize;
        this.timeBeforeDecay = timeBeforeDecay;
        this.decayTime = decayTime;
        time -= timeBeforeDecay;
        this.gameObject = gameObject;
        
    }
}


public class MinimapToFog : MonoBehaviour
{
    [SerializeField] csFogWar fogWar;
    [SerializeField] MiniMapController controller;
    [SerializeField] float startSize = 5;
    [SerializeField] float timeBeforeDecay = 1;
    [SerializeField] float decayTime = 10;
    [SerializeField] float updateTime;
    [SerializeField] ObjectPoolScript objectPool;

    private bool isDrawing = false;

    List<NoFogPosition> noFog = new List<NoFogPosition>();
    // Start is called before the first frame update
    void Start()
    {
        controller.onClickOnMinimap.AddListener(OnClickMap);
        fogWar.onUpdateField.AddListener(OnUpdate);
        controller.onPointerUpMinimap.AddListener(OnPointerUpMap);
        GameManager.Instance.gameData.inkAmount = 100f;
    }

    void OnClickMap(Vector3 position)
    {
        if (GameManager.Instance.gameData.inkAmount > 0)
        {
            NoFogPosition pos = new NoFogPosition(position, startSize, timeBeforeDecay, decayTime, objectPool.InstantiateObject(new Vector3(position.x, 0, position.z)));
            pos.levelCoordinates = fogWar.GetLevelCoordinates(pos.position);
            pos.gameObject.transform.localScale = new Vector3(pos.currentSize, pos.currentSize, pos.currentSize);
            noFog.Add(pos);
            GameManager.Instance.gameData.inkAmount -= Vector2.Distance(position, controller.prevLocation) * Globals.inkPerDistance;
            isDrawing = true;
        }
        else
        {
            isDrawing = false;
        }

    }
    void OnPointerUpMap(Vector3 vector)
    {
        isDrawing = false;
    }
    
    void OnUpdate()
    {
        for (int i = noFog.Count - 1; i >= 0; i--)
        {
            var item = noFog[i];
            item.time += Time.deltaTime / item.decayTime;
            fogWar.CreateSightFromPos(item.levelCoordinates, item.currentSize);
            

            if (item.time > 1)
            {
                objectPool.DestroyObject(item.gameObject);
                noFog.RemoveAt(i);
            }
            else if (item.time >= 0)
            {
                item.currentSize = Mathf.Lerp(item.startSize, 0, item.time);
                item.gameObject.transform.localScale = new Vector3(item.currentSize, item.currentSize, item.currentSize);
            }
        }

    }

    //IEnumerator Update()
    //{
    //    while (GameManager.Instance.player)
    //    {
    //        foreach (var item in noFog)
    //        {
    //            fogWar.CreateSightFromPos(item.position, item.);
    //        }
    //        yield return new WaitForSeconds(updateTime);
    //    }
    //}
    
}
