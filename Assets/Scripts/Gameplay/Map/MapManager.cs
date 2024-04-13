using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MapManager : MonoBehaviour
{
    public Camera cam;
    public Vector2 minPos;
    public Vector2 maxPos;

    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(new Vector3(minPos.x, minPos.y), new Vector3(maxPos.x, minPos.y));
        Gizmos.DrawLine(new Vector3(minPos.x, minPos.y), new Vector3(minPos.x, maxPos.y));
        Gizmos.DrawLine(new Vector3(minPos.x, maxPos.y), new Vector3(maxPos.x, maxPos.y));
        Gizmos.DrawLine(new Vector3(maxPos.x, minPos.y), new Vector3(maxPos.x, maxPos.y));
    }

    public bool CheckWithinMap()
    {
        return !(GetMouseWorldPosition().x < minPos.x || GetMouseWorldPosition().x > maxPos.x || GetMouseWorldPosition().y < minPos.y || GetMouseWorldPosition().y > maxPos.y) /*&& EventSystem.current.IsPointerOverGameObject()*/;
    }

    public Vector3 GetMouseWorldPosition()
    {
        if (cam)
            return cam.ScreenToWorldPoint(Input.mousePosition);
        else
            return Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }

}
