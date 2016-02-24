using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Flipper : MonoBehaviour {

	Image image;
	Card card;

	public AnimationCurve curve;
	public float duration;

	void Start () {
		duration = 0.5f;
		image = GetComponent<Image> ();
		card = GetComponent<Card> ();
	}

	public void Flip (Sprite front, Sprite back) {
		StopCoroutine(Flipping(front, back));
		StartCoroutine(Flipping(front, back));
	}

	IEnumerator Flipping(Sprite front, Sprite back){
		GameObject placeholder = new GameObject();
		placeholder.transform.SetParent(transform.parent);
		LayoutElement layout = placeholder.AddComponent<LayoutElement>();
		layout.preferredWidth = this.GetComponent<LayoutElement>().preferredWidth;
		layout.preferredHeight = this.GetComponent<LayoutElement>().preferredHeight;
		layout.flexibleHeight = 0;
		layout.flexibleWidth = 0;
		placeholder.transform.SetSiblingIndex(transform.GetSiblingIndex());
		
		placeholder.transform.SetParent(transform.parent);
		transform.SetParent(transform.parent.parent);
		image.sprite = front;
		float time = 0.0f;

		while (time <= 1.0f) {
			time += Time.deltaTime / duration;

			Vector3 localScale = transform.localScale;
			localScale.x = curve.Evaluate(time);
			transform.localScale = localScale;

			if (time >= 0.5f) {
				image.sprite = back;
			}

			yield return new WaitForFixedUpdate();
		}

		if (!card.face)
			card.Toogle (true);
		else
			card.Toogle (false);
		transform.SetParent(placeholder.transform.parent);
		transform.SetSiblingIndex(placeholder.transform.GetSiblingIndex());
		Destroy(placeholder);
	}
}
