using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShakeEvent : MonoBehaviour
{
    [SerializeField] CameraShakeParameters shakeParameters;
    public void Shake()
    {
        CameraShakerManager.Instance.Shake(shakeParameters);
    }
}
