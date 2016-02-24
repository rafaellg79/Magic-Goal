using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class DropZone : MonoBehaviour, IPointerEnterHandler, IDropHandler, IPointerExitHandler {

	public void OnPointerEnter(PointerEventData eventData){
		if(eventData.pointerDrag == null) { 
			return;
		}
		Draggable card = eventData.pointerDrag.GetComponent<Draggable>();
		if (card != null)
		{
			card.phParent = this.transform;
		}
	}

	public void OnPointerExit(PointerEventData eventData){
		if (eventData.pointerDrag == null){
			return;
		}
		Draggable card = eventData.pointerDrag.GetComponent<Draggable>();
		if (card != null && card.phParent == transform){
			card.phParent = card.parent;
		}
	}

	public void OnDrop(PointerEventData eventData) {
		Draggable card = eventData.pointerDrag.GetComponent<Draggable>();
		if (card != null)
		{
			card.parent = this.transform;
		}
	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
