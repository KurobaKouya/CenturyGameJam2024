using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraHandler : MonoBehaviour
{
    [Header("Variables")]
    [SerializeField] private Camera mainCamera = null;
    [SerializeField] private GameObject target = null;
    [SerializeField] private Vector3 offset;
    [SerializeField] private bool peekEnabled = true;
    [SerializeField] private float maxPeekDistance = 20f;


    private void Start()
    {
        if (mainCamera == null) mainCamera = Camera.main;
    }


    private void Update()
    {
        if (target == null) return;
        
        // Camera follow Player
        transform.position = target.transform.position + offset;


        if (peekEnabled)
        {
            Ray cameraRay = Camera.main.ScreenPointToRay(Input.mousePosition);
            Plane groundPlane = new(Vector3.up, Vector3.zero);
            if (groundPlane.Raycast(cameraRay, out float rayLength))
            {
                Vector3 pointToLook = cameraRay.GetPoint(rayLength);
                pointToLook.x = Mathf.Clamp(pointToLook.x, -maxPeekDistance, maxPeekDistance);
                pointToLook.z = Mathf.Clamp(pointToLook.z, -maxPeekDistance, maxPeekDistance);
                pointToLook.y = offset.y;
                mainCamera.transform.position = Vector3.Lerp(mainCamera.transform.position, pointToLook, 1f);
            }
        }
    }


    private void OnEnable()
    {
        GlobalEvents.onToggleCameraPeek += (bool enable) => peekEnabled = enable;
    }   


    private void OnDisable()
    {
        GlobalEvents.onToggleCameraPeek -= (bool enable) => peekEnabled = enable;
    } 
}
