using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MapManager : MonoBehaviour
{
    public Camera mapUICam;
    public Camera renderMinimapCam;
    [SerializeField] Vector3 minPos;
    [SerializeField] Vector3 maxPos;
    [SerializeField] Canvas mapCanvas;
    [SerializeField] RectTransform mapRect;
    [SerializeField] Player player;
   
    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(new Vector3(minPos.x, minPos.y) + transform.position, new Vector3(maxPos.x, minPos.y) + transform.position);
        Gizmos.DrawLine(new Vector3(minPos.x, minPos.y) + transform.position, new Vector3(minPos.x, maxPos.y)+ transform.position);
        Gizmos.DrawLine(new Vector3(minPos.x, maxPos.y) + transform.position, new Vector3(maxPos.x, maxPos.y) + transform.position);
        Gizmos.DrawLine(new Vector3(maxPos.x, minPos.y) + transform.position, new Vector3(maxPos.x, maxPos.y) + transform.position);
    }

    private void Start()
    {
        minPos += transform.position;
        maxPos += transform.position;
        if (player == null)
            player = GameManager.Instance.player;

        
        
    }

    public bool CheckWithinMap()
    {
        return !(GetMouseWorldPosition().x < minPos.x || GetMouseWorldPosition().x > maxPos.x || GetMouseWorldPosition().y < minPos.y || GetMouseWorldPosition().y > maxPos.y) /*&& EventSystem.current.IsPointerOverGameObject()*/;
    }
    public (Vector2, Vector2) GetMinMax()
    {
        return (minPos, maxPos);
    }

    public Vector2 GetMapSize()
    {
        Vector2 size = new Vector2(maxPos.x - minPos.x, maxPos.y - minPos.y);
        
        return size;
    }
    
    public Vector3 MapToWorldPosition()
    {


        Vector3 topRightScreenPos = new Vector3(5, 5, 0);
        //get size of camera
        Vector3 cameraSIzeScreen = new Vector3(renderMinimapCam.pixelHeight, renderMinimapCam.pixelWidth);
        Debug.Log("cam: " + cameraSIzeScreen);

        //get minimap size

        Vector3 screenSpacePosOfUI = RectTransformUtility.WorldToScreenPoint(mapUICam, mapRect.position);
        Debug.Log("cam1: " + screenSpacePosOfUI);

        //get minimap bottom left
        Vector3 botLeft = RectTransformUtility.WorldToScreenPoint(mapUICam, mapRect.rect.min);
        Debug.Log("cam2: " + botLeft);

        Vector3 sizeInScreenSpace =new Vector3(screenSpacePosOfUI.x - botLeft.x, screenSpacePosOfUI.y - botLeft.y) * 2;
        Debug.Log("cam3: " + sizeInScreenSpace);
        //get percentage and multiply to scale
        Vector3 scale = new Vector3(cameraSIzeScreen.x / sizeInScreenSpace.x, cameraSIzeScreen.y / sizeInScreenSpace.y);
        Debug.Log("camsscale: " + scale);
        //mousePos offset
        Vector3 mousePos = Input.mousePosition - botLeft;
        Debug.Log("cammmousePos: " + Input.mousePosition);
        //multiply mouse pos in world by scale

        Debug.Log("camsmousebefore: " + mousePos);
        mousePos.Scale(scale);
        Debug.Log("cammouse: " + mousePos);
        //return mousePos
        return mapUICam.ScreenToWorldPoint(mousePos);

        Debug.Log("Map0.1:" + topRightScreenPos);

        

        
        mousePos.x -= mapUICam.WorldToScreenPoint(minPos).x;
        mousePos.y -= mapUICam.WorldToScreenPoint(minPos).y;
        Debug.Log("MapMouse:" + mousePos);
        Vector2 mapUIToScreen = (mapUICam.WorldToScreenPoint(maxPos) - mapUICam.WorldToScreenPoint(minPos));

        float ratioX = topRightScreenPos.x / GetMapSize().x;
        float ratioY = topRightScreenPos.y / GetMapSize().y;
        Debug.Log("MouseRectSize: " + (mapUICam.WorldToScreenPoint(maxPos) - mapUICam.WorldToScreenPoint(minPos)));
        Debug.Log("Map0.2:" + ratioX + " + " + ratioY);
        //Vector3 mouseRealPosOnScreen = new Vector3(mousePos.x * ratioX, mousePos.y * ratioY);

        Vector2 pos = new Vector2(GetMouseWorldPosition().x * ratioX, GetMouseWorldPosition().y * ratioY);

        //Ray maxRay = Camera.main.ScreenPointToRay(Vector3.one);
        //Ray minRay = Camera.main.ScreenPointToRay(Vector3.zero);

        Vector3 playerPos = player.transform.position;
        return pos;

        

        
        
        



    }

    public void GetMap()
    {
        RaycastHit hit;
        Ray ray = renderMinimapCam.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit))
        {
            Transform objectHit = hit.transform;
            //Debug.Log(hit.point);

        }
    }

    public Vector3 GetMouseWorldPosition()
    {
        if (mapUICam)
            return mapUICam.ScreenToWorldPoint(Input.mousePosition);
        else
            return Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }

}
