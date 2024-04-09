using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Rigidbody2D rb;
    public Vector2 movementDir = Vector2.zero;
    private float movementSpeed = 10f;


    // Player Setup + 
    private void OnEnable()
    {
        GlobalEvents.Instance.PlayerEnabled(this);
        if (!rb) rb = GetComponent<Rigidbody2D>();
    }


    public void UpdateLoop()
    {
        rb.velocity = new Vector2(movementDir.x * movementSpeed, movementDir.y * movementSpeed);
    }
}
