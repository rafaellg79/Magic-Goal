using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Card : MonoBehaviour {

	public Sprite front, back;
	public bool face;

	void Start () {
		face = false;
		Toogle(face);
	}

	//Troca a face da carta entre a frente e as costas
	public void Toogle(bool showFront){
		if (showFront) {
			GetComponent<Image>().sprite = front;
		} else {
			GetComponent<Image>().sprite = back;
		}
		face = showFront;
	}

	void update() {	}
}
