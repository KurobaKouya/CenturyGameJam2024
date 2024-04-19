using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.Events;

public class MiniMapController : MonoBehaviour, IPointerClickHandler, IPointerMoveHandler, IPointerUpHandler
{
    public Camera miniMapCam;
    public UnityEvent<Vector3> onClickOnMinimap;
    public UnityEvent<Vector3> onPointerUpMinimap;
    public float distanceUpdate = 10;
    public AudioClipInstance drawSFX;

    public Vector3 prevLocation;
    public void OnPointerMove(PointerEventData eventData)
    {
        if (Input.GetMouseButton(0))
        {
            MinimapToWorld(eventData);
            AudioManager.instance.PlaySourceAudio(drawSFX);
        }
    }
    public void OnPointerClick(PointerEventData eventData)
    {
        

        MinimapToWorld(eventData);
    }
    public void OnPointerUp(PointerEventData eventData)
    {


        onPointerUpMinimap?.Invoke(eventData.position); 
    }

    void MinimapToWorld(PointerEventData eventData)
    {
        Vector2 curosr = new Vector2(0, 0);

        if (RectTransformUtility.ScreenPointToLocalPointInRectangle(GetComponent<RawImage>().rectTransform,
            eventData.position, eventData.pressEventCamera, out curosr))
        {

            Texture texture = GetComponent<RawImage>().texture;
            Rect rect = GetComponent<RawImage>().rectTransform.rect;

            float coordX = Mathf.Clamp(0, (((curosr.x - rect.x) * texture.width) / rect.width), texture.width);
            float coordY = Mathf.Clamp(0, (((curosr.y - rect.y) * texture.height) / rect.height), texture.height);

            float calX = coordX / texture.width;
            float calY = coordY / texture.height;


            curosr = new Vector2(calX, calY);
            if (prevLocation  != null && Vector2.Distance(prevLocation, curosr) <= distanceUpdate)
            {
                CastRayToWorld(curosr);
            }

            prevLocation = curosr;
        }
    }
    private void CastRayToWorld(Vector2 vec)
    {
        Ray MapRay = miniMapCam.ScreenPointToRay(new Vector2(vec.x * miniMapCam.pixelWidth,
            vec.y * miniMapCam.pixelHeight));

        RaycastHit miniMapHit;

        if (Physics.Raycast(MapRay, out miniMapHit, Mathf.Infinity))
        {
            Debug.Log("miniMapHit: " + miniMapHit.point);
            onClickOnMinimap.Invoke(miniMapHit.point);
        }

    }

    
}

