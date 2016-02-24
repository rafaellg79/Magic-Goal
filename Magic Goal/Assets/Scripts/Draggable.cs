using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Draggable : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler{

	public Transform parent = null;
	public Transform phParent = null;

	GameObject placeholder = null;
	Vector2 offset;

	public void OnBeginDrag(PointerEventData eventData){
		placeholder = new GameObject();
		placeholder.transform.SetParent(transform.parent);
		LayoutElement layout = placeholder.AddComponent<LayoutElement>();
		layout.preferredWidth = this.GetComponent<LayoutElement>().preferredWidth;
		layout.preferredHeight = this.GetComponent<LayoutElement>().preferredHeight;
		layout.flexibleHeight = 0;
		layout.flexibleWidth = 0;
		placeholder.transform.SetSiblingIndex(transform.GetSiblingIndex());

		offset = new Vector2(transform.position.x, transform.position.y) - eventData.position;
		parent = transform.parent;
		phParent = transform.parent;
		transform.SetParent( transform.parent.parent );
		GetComponent<CanvasGroup>().blocksRaycasts = false;
	}

	public void OnDrag(PointerEventData eventData){
		if(placeholder.transform.parent != phParent) {
			placeholder.transform.SetParent(phParent);
		}
		transform.position = eventData.position + offset;
		int index = phParent.childCount;

		for(int i = 0; i < phParent.childCount; i++) {
			if(transform.position.x < phParent.GetChild(i).position.x) {
				index = i;
				if(placeholder.transform.GetSiblingIndex() < index) {
					index--;
				}
				break;
			}
		}
		placeholder.transform.SetSiblingIndex(index);
	}

	public void OnEndDrag(PointerEventData eventData){
		transform.SetParent(parent);
		transform.SetSiblingIndex(placeholder.transform.GetSiblingIndex());
		GetComponent<CanvasGroup>().blocksRaycasts = true;

		Destroy(placeholder);
	}

	// Use this for initialization
	void Start(){ }

	// Update is called once per frame
	void Update(){ }
}
