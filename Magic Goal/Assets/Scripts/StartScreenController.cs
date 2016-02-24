using UnityEngine;
using System.Collections;
using UnityEngine.UI;


public class StartScreenController : MonoBehaviour {

	public GameObject NameInput;
	public GameObject Fade;

	// Use this for initialization
	void Start () {
		Fade.SetActive (true);
		if(getINIDefinition("PLAYERNAME") != "")
		{
			NameInput.GetComponent<InputField>().text = getINIDefinition("PLAYERNAME");
		}
	}
	
	// Update is called once per frame
	void Update () {
		
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
				file.Close();
			}
		}
		return "";
		file.Close();
	}
}
