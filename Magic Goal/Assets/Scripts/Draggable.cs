using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Draggable : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler{

	public Transform parent = null;
	public Transform phParent = null;

	GameObject placeholder = null;
	Vector2 offset;

	public void OnBeginDrag(PointerEventData eventData){
		//Cria um placeholder
		placeholder = new GameObject();
		placeholder.transform.SetParent(transform.parent);

		//Ajusta o layout e coloca na posicao da carta que puxamos
		LayoutElement layout = placeholder.AddComponent<LayoutElement>();
		layout.preferredWidth = GetComponent<LayoutElement>().preferredWidth;
		layout.preferredHeight = GetComponent<LayoutElement>().preferredHeight;
		layout.flexibleHeight = 0;
		layout.flexibleWidth = 0;
		placeholder.transform.SetSiblingIndex(transform.GetSiblingIndex());

		//Cria o vetor distancia do centro da carta ate o mouse
		offset = new Vector2(transform.position.x, transform.position.y) - eventData.position;
		//Guarda o pai da carta e do placeholder e seta o pai da carta como o canvas
		parent = transform.parent;
		phParent = transform.parent;
		transform.SetParent( transform.parent.parent );
		//Serve para que a carta colida com outros panel(maos, tabuleiro...)
		GetComponent<CanvasGroup>().blocksRaycasts = false;
	}

	public void OnDrag(PointerEventData eventData){
		//Seta o pai do placeholder como o pai guardado
		if(placeholder.transform.parent != phParent) {
			placeholder.transform.SetParent(phParent);
		}
		//Atualiza a posicao da carta
		transform.position = eventData.position + offset;
		int index = phParent.childCount;

		//Localiza e atualiza a posicao do placeholder
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
		//Seta o pai da carta como o guardado e coloca a carta na posicao do placeholder
		transform.SetParent(parent);
		transform.SetSiblingIndex(placeholder.transform.GetSiblingIndex());
		GetComponent<CanvasGroup>().blocksRaycasts = true;

		//Destroi o placeholder
		Destroy(placeholder);
	}

	// Use this for initialization
	void Start(){ }

	// Update is called once per frame
	void Update(){ }
}
