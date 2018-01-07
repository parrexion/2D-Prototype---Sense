using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class DialogueAdvanceFrame : MonoBehaviour, IPointerClickHandler {

    public IntVariable currentFrame;
	public UnityEvent dialogueClickEvent;

    void IPointerClickHandler.OnPointerClick(PointerEventData eventData) {
        dialogueClickEvent.Invoke();
    }

    void Update() {
        if (Input.GetKeyDown(KeyCode.S)){
            currentFrame.value += 1000;
            dialogueClickEvent.Invoke();
        }
    }
}
