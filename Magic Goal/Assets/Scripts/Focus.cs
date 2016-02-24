using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Focus : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler{

	//Vector3 localScale;
	Vector3 original;
	LayoutElement layout;

	public void OnPointerEnter(PointerEventData eventData){

		RectTransform rectTransform = GetComponent<RectTransform>();
		Vector3 localScale = original = rectTransform.localScale;
		localScale.x *= 2;
		localScale.y *= 2;
		rectTransform.localScale = localScale;
		//layout = GetComponent<LayoutElement>();
		//layout.preferredHeight = localScale.y * 2;
		//layout.preferredWidth = localScale.x * 2;
	}

	public void OnPointerExit(PointerEventData eventData){
		//layout.preferredHeight = localScale.y;
		//layout.preferredWidth = localScale.x;
		RectTransform rectTransform = GetComponent<RectTransform>();
		rectTransform.localScale = original;
	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
