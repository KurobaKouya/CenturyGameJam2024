using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraHandler : Singleton<CameraHandler>
{
    [Header("Variables")]
    public Camera cam = null;
    [SerializeField] private GameObject target = null;
    [SerializeField] private Vector3 offset;
    [SerializeField] private bool peekEnabled = true;
    [SerializeField] private float maxPeekDistance = 20f;
    [SerializeField] private float peekSmoothSpeed = 5f;


    private void Start()
    {
        if (cam == null) cam = Camera.main;
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
                pointToLook.x = Mathf.Clamp(pointToLook.x, -maxPeekDistance + transform.position.x, maxPeekDistance + transform.position.x);
                pointToLook.z = Mathf.Clamp(pointToLook.z, -maxPeekDistance + transform.position.z, maxPeekDistance + transform.position.z);
                pointToLook.y = offset.y;
                cam.transform.position = Vector3.Lerp(cam.transform.position, pointToLook, peekSmoothSpeed * Time.deltaTime);
            }
        }
        else cam.transform.position = Vector3.zero;
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
