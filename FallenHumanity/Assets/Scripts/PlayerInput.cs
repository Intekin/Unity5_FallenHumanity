using UnityEngine;
using System.Collections;

public class PlayerInput : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetButtonUp("CharacterScreen"))
		{
			Debug.Log("CharacterScreen");
			Messenger.Broadcast("CharacterScreen");	
		}

		if(Input.GetButtonUp("Menu"))
		{
			Messenger.Broadcast("DisplayMenu");
		}
		
	}
}
