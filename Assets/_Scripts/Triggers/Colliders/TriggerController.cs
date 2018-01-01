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


    /// <summary>
    /// Resets the dictionary to all triggers being inactive.
    /// </summary>
    public void ResetDictionary() {
        foreach (KeyValuePair<string,bool> key in triggerStates) {
            triggerStates[key.Key] = false;
        }
    }

    /// <summary>
    /// Checks if the trigger is active or not.
    /// </summary>
    /// <param name="uuid"></param>
    /// <returns></returns>
    public bool CheckActive(string uuid) {
        if (triggerStates.ContainsKey(uuid))
            return triggerStates[uuid];

        triggerStates.Add(uuid, true);
        return true;
    }

    /// <summary>
    /// Sets the state och the trigger with the given uuid.
    /// </summary>
    /// <param name="uuid"></param>
    /// <param name="state"></param>
    public void SetActive(string uuid, bool state) {
        if (triggerStates.ContainsKey(uuid))
            triggerStates[uuid] = state;
        else
            triggerStates.Add(uuid, state);
    }


    // SAVING AND LOADING

    /// <summary>
    /// Stores the data in a save class for saving into xml.
    /// </summary>
    /// <returns></returns>
    public TriggerSaveClass SaveTriggers() {
        TriggerSaveClass saveData = new TriggerSaveClass();

        int size = triggerStates.Count;
        saveData.uuids = new string[size];
        saveData.states = new bool[size];
        int index = 0;
        foreach(KeyValuePair<string,bool> trigger in triggerStates) {
            saveData.uuids[index] = trigger.Key;
            saveData.states[index] = trigger.Value;
            index++;
        }

        return saveData;
    }

    /// <summary>
    /// Loads the trigger data from the save class.
    /// </summary>
    /// <param name="saveData"></param>
    public void LoadTriggers(TriggerSaveClass saveData) {

        triggerStates = new Dictionary<string, bool>();

        for (int i = 0; i < saveData.states.Length; i++) {
            triggerStates.Add(saveData.uuids[i],saveData.states[i]);
        }
    }
}


/// <summary>
/// Save class for the trigger information list.
/// </summary>
public class TriggerSaveClass {
    public string[] uuids;
    public bool[] states;
}