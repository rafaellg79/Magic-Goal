using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Card : MonoBehaviour {

	public Sprite front, back;
	public bool face;

	void Start () {
		face = true;
	}

	public void Toogle(bool showFront){
		if (showFront) {
			GetComponent<Image>().sprite = front;
			face = true;
		} else {
			GetComponent<Image>().sprite = back;
			face = false;
		}
	}
}
