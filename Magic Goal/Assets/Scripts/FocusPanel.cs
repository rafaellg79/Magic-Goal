using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class FocusPanel : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler {

	float y0, c, duration, time;
	
	public void OnPointerEnter(PointerEventData eventData) {
		StopCoroutine(GainFocus());
		StartCoroutine(GainFocus());
	}

	IEnumerator GainFocus() {
		Vector3 up = transform.localScale;
		while (time <= 1.0f){
			time += Time.deltaTime / duration;
			up.y = Mathf.Lerp(y0, y0 + c, time);
			transform.localScale = up;
			yield return new WaitForFixedUpdate();
		}
	}

	public void OnPointerExit(PointerEventData eventData) {
		StopAllCoroutines();
		StartCoroutine(LoseFocus());
	}

	IEnumerator LoseFocus(){
		Vector3 down = transform.localScale;
		while (time >= 0.0f)
		{
			time -= Time.deltaTime / duration;
			down.y = Mathf.Lerp(y0, y0 + c, time);
			transform.localScale = down;
			yield return new WaitForFixedUpdate();
		}
	}

	void Start () {
		c = 7.0f;
		duration = 0.5f;
		time = 0.0f;
		y0 = transform.localScale.y;
	}
	
	void Update () {
	
	}
}
