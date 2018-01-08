// using System.Collections;
// using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(TriggerController))]
public class TriggerControllerEditor : Editor {

    string chapter;
    Constants.SCENE_INDEXES cArea;
    Constants.SCENE_INDEXES pArea;

    public override void OnInspectorGUI () {
        DrawDefaultInspector();
        GUILayout.Space(20);
        chapter = EditorGUILayout.TextField("Current Chapter", chapter);
        cArea = (Constants.SCENE_INDEXES)EditorGUILayout.EnumPopup("Current Area", (Constants.SCENE_INDEXES)cArea);
        pArea = (Constants.SCENE_INDEXES)EditorGUILayout.EnumPopup("Player Area", (Constants.SCENE_INDEXES)pArea);
        GUILayout.Space(20);
        if (GUILayout.Button("Reactivate", GUILayout.Height(50))){

            TriggerController controller = target as TriggerController;
            controller.currentChapter.value = chapter;
            controller.currentArea.value = (int)cArea;
            controller.playerArea.value = (int)pArea;
            controller.ReactivateTriggers();
            Debug.Log("Reactivating triggers");
        }
    }
}