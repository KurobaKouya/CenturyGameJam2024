using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[Serializable]
public class NamedEvent
{
    public string name;
    public UnityEvent unityEvent;
}
public class EmptyEvent : MonoBehaviour
{
    public List<NamedEvent> namedEvents;

    public void PlayEvent(string eventName)
    {
        for (int i = 0; i < namedEvents.Count; i++)
        {
            if (namedEvents[i].name == eventName)
            {
                namedEvents[i].unityEvent?.Invoke();
                break;
            }
        }
    }

}
