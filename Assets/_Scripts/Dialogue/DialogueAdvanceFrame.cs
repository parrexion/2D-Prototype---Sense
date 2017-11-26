using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class DialogueAdvanceFrame : MonoBehaviour, IPointerClickHandler {

	public UnityEvent dialogueClickEvent;

    void IPointerClickHandler.OnPointerClick(PointerEventData eventData) {
		
        dialogueClickEvent.Invoke();
    }
}
