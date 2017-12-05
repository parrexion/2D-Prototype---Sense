using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TriggerController : MonoBehaviour {

#region Singleton
    
    public static TriggerController instance = null;
    void Awake() {
        if (instance != null)
            Destroy(gameObject);
        else {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

#endregion

    private Dictionary<string,bool> triggerStates = new Dictionary<string, bool>();


    public void ResetDictionary() {
        foreach (KeyValuePair<string,bool> key in triggerStates) {
            triggerStates[key.Key] = false;
        }
    }

    public bool CheckActive(string uuid) {
        if (triggerStates.ContainsKey(uuid))
            return triggerStates[uuid];

        triggerStates.Add(uuid, true);
        return true;
    }

    public void SetActive(string uuid, bool state) {
        if (triggerStates.ContainsKey(uuid))
            triggerStates[uuid] = state;
        else
            triggerStates.Add(uuid, state);
    }

}