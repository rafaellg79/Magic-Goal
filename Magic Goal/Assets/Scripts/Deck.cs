using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using System;

public class Deck : MonoBehaviour {

	public GameObject hand;
	List<Card> deck;
	public Sprite[] sprites;
	bool pushing;
	float duration;

	// Use this for initialization
	void Start () {
		duration = 0.5f;
		deck = new List<Card>();
		pushing = false;
		for (int i = 0; i < 20; i++) {
			deck.Add(CreateCard(sprites[i], sprites[i+20]).GetComponent<Card>());
		}
		Shuffle();
		//Ler cartas do arquivo de entrada talvez separar em método

	}

	//embaralhando através do algoritmo de Fisher-Yates
	public void Shuffle() {
		System.Random rnd = new System.Random();
		for (int i = deck.Count - 1; i > 0; i--) {
			int j = rnd.Next(0, i);
			Card temp = deck[j];
			deck[j] = deck[i];
			deck[i] = temp;
		}
	}

	public void Push() {
		StopCoroutine(Pushing());
		StartCoroutine(Pushing());
	}

	IEnumerator Pushing()
	{
		//Imperde que outra carta seja puxada
		pushing = true;

		//Puxa uma carta do topo
		Card card = deck[deck.Count - 1];
		deck.RemoveAt(deck.Count - 1);

		//Deixa a imagem invisivel
		if(deck.Count == 0) {
			GetComponent<Image>().color = new Color(0.0f, 0.0f, 0.0f, 0.0f);
		}

		//Cria um objeto para ocupar o espaco na mao e definir o vetor distancia do deck ate a mao
		GameObject placeholder = CreatePlaceholder();
		LayoutElement layout = placeholder.GetComponent<LayoutElement>();
		float time = 0.0f, w = layout.preferredWidth;
		layout.preferredWidth = 0.0f;
		placeholder.name = "placeholder";
		placeholder.transform.SetParent(hand.transform);
		Vector2 p0 = new Vector2(0.0f, 0.0f), p = new Vector2(0.0f, 0.0f);
		yield return new WaitForEndOfFrame();
		
		Vector2 destiny = placeholder.transform.position - transform.position;
		card.transform.SetParent(hand.transform.parent);
		while (time <= 1.0f) {
			time += Time.deltaTime / duration;
			//Aumenta a largura do placeholder ate ocupar o espaco da carta
			layout.preferredWidth = w * time;
			//Calcula a nova posicao da carta
			p.x = Mathf.Lerp(0.0f, destiny.x, time);
			p.y = Mathf.Lerp(0.0f, destiny.y, time);
			//translada a carta baseada no vetor diferenca da posicao atual e da proxima posicao
			card.transform.Translate(p - p0);
			//atualiza o vetor posicao inicial
			p0 = p;
			yield return new WaitForFixedUpdate();
		}
		//Destroi o placeholder e seta como pai a mao do jogador
		Destroy(placeholder);
		card.transform.SetParent(hand.transform);
		pushing = false;
		//Destroi esse objeto
		if(deck.Count == 0) {
			Destroy(gameObject);
		}
	}
	
	// Update is called once per frame
	void OnGUI () {
		if(GUI.Button(new Rect(10, 10, 100, 20), "deck") && !pushing && deck.Count != 0) {
			Push();
		}
	}
	
	//Cria uma carta com as faces 
	GameObject CreateCard(Sprite front, Sprite back) {
		//Objeto a ser retornado
		GameObject placeholder = new GameObject();

		//Ajusta o layout
		LayoutElement layout = placeholder.AddComponent<LayoutElement>();
		layout.preferredWidth = GetComponent<LayoutElement>().preferredWidth;
		layout.preferredHeight = GetComponent<LayoutElement>().preferredHeight;
		layout.flexibleHeight = 0;
		layout.flexibleWidth = 0;

		//Setando posicao do objeto sobre o deck
		placeholder.transform.position = transform.position;
		placeholder.GetComponent<RectTransform>().sizeDelta = new Vector2(layout.preferredWidth, layout.preferredHeight);
		
		//Adicionando carta e imagem ao objeto
		Image img = placeholder.AddComponent<Image>();
		Card card = placeholder.AddComponent<Card>();
		card.front = front;
		img.sprite = card.back = back;

		return placeholder;
	}
	
	//Cria um placeholder
	GameObject CreatePlaceholder() {
		GameObject placeholder = new GameObject();

		//Ajusta o layout
		LayoutElement layout = placeholder.AddComponent<LayoutElement>();
		layout.preferredWidth = GetComponent<LayoutElement>().preferredWidth;
		layout.preferredHeight = GetComponent<LayoutElement>().preferredHeight;
		layout.flexibleHeight = 0;
		layout.flexibleWidth = 0;

		return placeholder;
	}
}
