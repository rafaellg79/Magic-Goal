using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Flipper : MonoBehaviour {

	Image image;
	Card card;

	public AnimationCurve curve;
	public float duration, time;

	void Start () {
		duration = 0.5f;
		time = 0.0f;
		image = GetComponent<Image> ();
		card = GetComponent<Card> ();

		//Cria uma curva que desce do 1 até o 0 e volta pro 1
		curve = new AnimationCurve();
		curve.AddKey(0.0f, 1.0f);
		curve.AddKey(0.5f, 0.0f);
		curve.AddKey(1.0f, 1.0f);
	}

	//Gira a carta
	public void Flip (Sprite front, Sprite back) {
		StopCoroutine(Flipping(front, back));
		StartCoroutine(Flipping(front, back));
	}

	//Gira a carta
	IEnumerator Flipping(Sprite front, Sprite back){
		image.sprite = front;//Imagem inicial

		/**Enquanto o tempo for menor que 1 muda a largura da carta de baseado na curva
			enquanto o tempo for menor que 0.5 diminui a largura até ficar invisivel
			em seguida aumenta ate voltar a largura original
		  */
		while (time <= 1.0f) {
			//Divide o tempo passado para termos a duracao desejada
			time += Time.deltaTime / duration;

			Vector3 localScale = transform.localScale;
			localScale.x = curve.Evaluate(time);
			transform.localScale = localScale;

			//muda a face da carta
			if (time >= 0.5f) {
				image.sprite = back;
			}
			
			yield return new WaitForFixedUpdate();
		}

		time = 0.0f;
		if (!card.face)
			card.Toogle (true);
		else
			card.Toogle (false);
	}
}
