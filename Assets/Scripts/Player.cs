using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Rigidbody rb;
    public Vector2 movementDir = Vector2.zero;
    private float movementSpeed = 10f;


    // Player Setup + 
    private void OnEnable()
    {
        GlobalEvents.Instance.PlayerEnabled(this);
        if (!rb) rb = GetComponent<Rigidbody>();
    }


    public void UpdateLoop()
    {
        rb.velocity = new Vector3(movementDir.x * movementSpeed, 0, movementDir.y * movementSpeed);
        // if (movementDir != Vector2.zero) 
        LookAtCursor();
    }


    // Rotation for 3D model
    private void LookAt()
    {
        float currentRotation = transform.rotation.eulerAngles.y;
        float targetRotation = Quaternion.LookRotation(rb.velocity, Vector3.up).eulerAngles.y;
        float nextRotation = Mathf.LerpAngle(targetRotation, currentRotation, 0.7f);
        Vector3 nextDirection = new(Mathf.Sin(nextRotation * Mathf.Deg2Rad), 0, Mathf.Cos(nextRotation * Mathf.Deg2Rad));
        
        if (movementDir.sqrMagnitude > 0.1f && nextDirection.sqrMagnitude > 0.1f)
            rb.rotation = Quaternion.LookRotation(nextDirection, Vector3.up);
        else
            rb.angularVelocity = Vector3.zero;
    }


    private void LookAtCursor()
    {
        Ray cameraRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        Plane groundPlane = new(Vector3.up, Vector3.zero);
        if (groundPlane.Raycast(cameraRay, out float rayLength))
        {
            Vector3 pointToLook = cameraRay.GetPoint(rayLength);
            Debug.DrawLine(cameraRay.origin, pointToLook, Color.yellow);

            transform.LookAt(new Vector3(pointToLook.x, transform.position.y, pointToLook.z));
        }
    }
}
