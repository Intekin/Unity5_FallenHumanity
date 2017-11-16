//Check to see if we have some saved data in the playerprefs
//check the version of the saved Data
//if the saved version of the data is not the current version.
//do something
//else if the saved version is the current version
//Check to see if they have a character saved - check for character name

using UnityEngine.SceneManagement;
using UnityEngine;
using System.Collections;


public class MainMenu : MonoBehaviour {
	public const float VERSION = 0.002f;
	public bool clearPrefs = false;
	
	private string _levelToLoad = "";
										  
	//private string _characterGeneration = GameSettings.levelNames[1];
	private string _fistLevel = GameSettings.levelNames[2];
	
	private bool _hasCharacter = false;
	

	// Use this for initialization
	void Start () {
//		if(clearPrefs)
//			PlayerPrefs.DeleteAll();
//		
//		
//		if(PlayerPrefs.HasKey("version"))
//		{
//			Debug.Log("There is a version key");
//			if(PlayerPrefs.GetFloat("version")!=VERSION)
//			{
//				Debug.Log("Saved Version is not the same");	
//				//uppgrade player prefs here
//			}
//			else
//			{
//				Debug.Log("Saved version is the same");	
//				if(PlayerPrefs.HasKey("PlayerName"))
//				{
//					Debug.Log("There is a player name tag");
//					if(PlayerPrefs.GetString("PlayerName")=="")
//					{
//						Debug.Log("Player name is empty");
//					}
//					else
//					{
//						Debug.Log("Player has a name");
//						_hasCharacter = true;
//					}
//				}
//				else
//				{
//					Debug.Log("There is no player name key");	
//				}
//			}
//		}
//		else
//		{
//			//NewCharacter();
//		}
//	
	}
	
	// Update is called once per frame
	void Update () {
		if(_levelToLoad == "")
			return;
		SceneManager.LoadScene(_levelToLoad);
	}
	
	void OnGUI()
	{

		//Continue();
		NewCharacter();
		//LoadCharacter();
		//Options();
		Quit();	
		GameVersion();
		
	}
	
	void Continue()
	{
		
		if(PlayerPrefs.GetString("PlayerName")=="")
		{
			GUI.Label(new Rect(10,10,110,25),"Continue", "Button");
		}
		else
		{
			if(GUI.Button(new Rect(10,10,110,25),"Continue"))
				_levelToLoad = _fistLevel;
		}
	}
	
	void NewCharacter()
	{
		if(GUI.Button(new Rect((Screen.width/2) - 100,(Screen.height/2) + 40,110,25),"New Game"))
		{
			_levelToLoad = _fistLevel;
		}
	}
	
	void LoadCharacter()
	{
		if(GUI.Button(new Rect(10,70,110,25),"Load Character"))
		{
			_levelToLoad = _fistLevel;
		}
	}
	
	void Options()
	{
		if(GUI.Button(new Rect(10,100,110,25),"Options"))
		{
			Application.Quit();
		}
	}
	
	void Quit()
	{
		if(GUI.Button(new Rect((Screen.width/2) - 100,(Screen.height/2) + 130, 110, 25),"Quit"))
		{
			Application.Quit();
		}
	}
	
	void GameVersion()
	{
		GUI.Label(new Rect(Screen.width - 200, Screen.height - 25, 200, 25), "**Closed Beta** ver: " + VERSION.ToString());
	}
}
