using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawFog : MonoBehaviour
{
    [SerializeField] ObjectPoolScript pool;
    [SerializeField] MiniMapController controller;

    private void Start()
    {
        controller.onClickOnMinimap.AddListener(OnClickAndMove);

    }

    void OnClickAndMove(Vector3 pos)
    {
        GameObject o = pool.InstantiateObject(new Vector3(pos.x, transform.position.y, pos.z));
        o.transform.rotation = transform.rotation;
    }



}
