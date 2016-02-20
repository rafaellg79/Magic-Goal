using UnityEngine;
using System.Collections;

public class NetworkingManager : MonoBehaviour {

	void Start () {
		PhotonNetwork.PhotonServerSettings.ServerAddress = getINIDefinition ("SERVERADRESS");
		PhotonNetwork.ConnectUsingSettings("v4.2");
	}

	void Update () {
	
	}

	private const string roomName = "RoomName";
	private RoomInfo[] roomsList;

	void OnGUI()
	{
		if (!PhotonNetwork.connected)
		{
			GUILayout.Label(PhotonNetwork.connectionStateDetailed.ToString());
		}
		else if (PhotonNetwork.room == null)
		{
			// Create Room
			if (GUI.Button(new Rect(100, 100, 250, 100), "Start Server"))
				PhotonNetwork.CreateRoom(roomName);

			// Join Room
			if (roomsList != null)
			{
				for (int i = 0; i < roomsList.Length; i++)
				{
					if (GUI.Button(new Rect(100, 250 + (110 * i), 250, 100), "Join " + roomsList[i].name))
						PhotonNetwork.JoinRoom(roomsList[i].name);
				}
			}
		}
	}

	void OnReceivedRoomListUpdate()
	{
		roomsList = PhotonNetwork.GetRoomList();
	}
	void OnJoinedRoom()
	{
		Debug.Log("Connected to Room");
	}

	private string getINIDefinition(string INIDefinition){
		int counter = 0;
		string line;
		System.IO.StreamReader file = new System.IO.StreamReader("Assets/Conexao.ini");
		string Definition;
		char[] separators = {'='};
		int j = 0, k = 1;
		while((line = file.ReadLine()) != null)
		{
			string[] Def = line.Split (separators);
			if(Def[j].Equals(INIDefinition)){
				return Def [k];
				file.Close();
			}
			j++;
			k++;
		}
		return "";
		file.Close();
	}
}
