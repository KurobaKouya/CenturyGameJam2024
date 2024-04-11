using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraHandler : MonoBehaviour
{
    [Header("Variables")]
    [SerializeField] private GameObject target = null;
    [SerializeField] private Vector3 offset;



    private void Update()
    {
        if (target == null) return;
        transform.position = target.transform.position + offset;
    }
}
