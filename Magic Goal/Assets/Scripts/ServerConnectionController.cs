using UnityEngine;
using System.Collections;

public class ServerConnectionController : MonoBehaviour {

	private bool isConnecting;
	public GameObject Text;
	public GameObject Shadow;

	void Start () {
		isConnecting = true;
	}

	void Update () {
		if(isConnecting){
			if(Text.GetComponent<TextMesh>().text != PhotonNetwork.connectionStateDetailed.ToString()){
				Text.GetComponent<TextMesh>().text = PhotonNetwork.connectionStateDetailed.ToString();
				Shadow.GetComponent<TextMesh>().text = PhotonNetwork.connectionStateDetailed.ToString();
			}
			if (PhotonNetwork.connected) {
				Text.GetComponent<TextMesh> ().text = "Connected";
				Shadow.GetComponent<TextMesh>().text = "Connected";
				isConnecting = false;
				StartCoroutine (Wait (3));
			}
		}
	}

	IEnumerator Wait(int seconds){
		yield return new WaitForSeconds (seconds);
		gameObject.SetActive (false);
		Application.LoadLevel (1);
	}

	public void ConnectToServer(){
		PhotonNetwork.PhotonServerSettings.ServerAddress = getINIDefinition ("SERVERADRESS");
		PhotonNetwork.ConnectUsingSettings("v4.2");
	}

	private string getINIDefinition(string INIDefinition){
		int counter = 0;
		string line;
		System.IO.StreamReader file = new System.IO.StreamReader("Assets/Client.ini");
		string Definition;
		char[] separators = {'='};
		while((line = file.ReadLine()) != null)
		{
			string[] Def = line.Split (separators);
			if(Def[0].Equals(INIDefinition)){
				return Def [1];
			}
		}
		return "";
	}
}
