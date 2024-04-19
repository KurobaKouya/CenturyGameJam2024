using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayAudioComponent : MonoBehaviour
{

    public List<AudioClipInstance> audioClip = new List<AudioClipInstance>();

    // Start is called before the first frame update
    public void PlayAudio(string name)
    {
        foreach (var item in audioClip)
        {
            Debug.Log("player: ");
            if (item.audioClip.name == name)
            {
                Debug.Log("playerer: ");
                AudioManager.instance.PlaySourceAudio(item);
            }
        }
        Debug.Log("playaasa: ");
    }

    
}
