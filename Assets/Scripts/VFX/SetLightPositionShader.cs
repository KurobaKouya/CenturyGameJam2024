using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetLightPositionShader : MonoBehaviour
{

    Renderer[] rend;
    // Start is called before the first frame update
    void Start()
    {
        rend = GetComponentsInChildren<Renderer>();
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < rend.Length; i++)
        {
            Vector3 pos = GameManager.Instance.player.transform.position;
            rend[i].material.SetVector("LightPosition", pos);
        }
    }
}
