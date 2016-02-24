using UnityEngine;
using System.Collections;

public class Test : MonoBehaviour {

	Card card;
	Flipper flipper;

	void Start () {
		card = GetComponent<Card> ();
		flipper = GetComponent<Flipper> ();
	}

	void Update() {
		if(Input.GetMouseButtonDown(0)){
			if (card.face)
			{
				flipper.Flip(card.front, card.back);
			}
			else {
				flipper.Flip(card.back, card.front);
			}

		}
	}

	/*void OnMouseDown() {
		if (card.face){
			flipper.Flip(card.front, card.back);
		}
		else {
			flipper.Flip(card.back, card.front);
		}

	}*/

	/*void OnGUI(){
		if (GUI.Button(new Rect(10, 10, 100, 20), "Front") && !card.face)
			flipper.Flip(card.back, card.front);
		if (GUI.Button(new Rect(10, 30, 100, 20), "Back") && card.face)
			flipper.Flip(card.front, card.back);
	}*/
}
