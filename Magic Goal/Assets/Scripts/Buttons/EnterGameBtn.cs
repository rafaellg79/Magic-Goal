using UnityEngine;
using System.Collections;

public class EnterGameBtn : MonoBehaviour {

	public GameObject ServerConnection;
	public GameObject Container;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetMouseButtonDown(0))
		{
			Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

			if (gameObject.GetComponent<Collider2D>().OverlapPoint(mousePosition))
			{
				Container.SetActive (false);
				ServerConnection.SetActive (true);
				ServerConnection.SendMessage ("ConnectToServer");
			}
		}
	}
}
