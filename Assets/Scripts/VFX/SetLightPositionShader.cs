using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SetLightPositionShader : MonoBehaviour
{

    Renderer[] rend;
    [SerializeField] Material lightMaterial;
    // Start is called before the first frame update
    void Start()
    {
        rend = GetComponentsInChildren<Renderer>();
        for (int i = 0; i < rend.Length; i++)
        {
            rend[i].material = lightMaterial;
        }
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < rend.Length; i++)
        {
            Vector3 pos = GameManager.Instance.player.transform.position;
            pos.y = rend[i].gameObject.transform.position.y;
            rend[i].material.SetVector("_LightPosition", pos);
            Debug.Log("SetLight: " + rend[i].name);
        }
    }
}
