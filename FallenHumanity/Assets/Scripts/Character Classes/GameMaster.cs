using UnityEngine;
using System.Collections;

public class GameMaster : MonoBehaviour {
	public GameObject playerCharacter;
	public GameObject gameSettings;
	
	public Vector3 spawnXOffset;

	private GameObject _pc;
	private PlayerCharacter _pcScript;
	
	public Vector3 spawnPoint;		//This is the place i want the player to spawn.
	
	
	// Use this for initialization
	void Start () {
		
		GameObject go;
			
		
		if(go = GameObject.Find(GameSettings.PlayerSpawnPoint))
		{
			Debug.Log(go.transform.position);
			Debug.Log("Spawn point found: " + GameSettings.PlayerSpawnPoint + ".  At position: " + GameObject.Find(GameSettings.PlayerSpawnPoint).transform.position);
		}
		else // Create a backup spawnpoint, if assigned spawnpoint is not found.
		{
			spawnPoint = new Vector3(0,2,0); 
			
			Debug.LogWarning("Could not find Player Spawn Point: " + GameSettings.PlayerSpawnPoint);
			
			go = new GameObject(GameSettings.PlayerSpawnPoint);
			Debug.Log("Created Player Spawn Point");

			go.transform.position = spawnPoint;
			Debug.Log("Created Player Spawn Point");
		}

		Vector3 localOffset = new Vector3(0,0,0.5f);
		Vector3 worldOffset = go.transform.rotation * localOffset;
		Vector3 spawnPosition = go.transform.position + worldOffset;

		_pc = Instantiate(playerCharacter, (spawnPosition), go.transform.rotation) as GameObject;

		_pc.name = "pc";


		
		_pcScript = _pc.GetComponent<PlayerCharacter>();

		LoadCharacter();
	}
	
	public void LoadCharacter()
	{
		GameObject gs = GameObject.Find("__GameSettings");
		
		if(gs == null)
		{
			GameObject gs1 = Instantiate(gameSettings, Vector3.zero, Quaternion.identity)as GameObject;
			gs1.name = "__GameSettings";
		}
		
		//GameSettings gsScript = GameObject.Find("__GameSettings").GetComponent<GameSettings>();
		
		//gsScript.LoadCharacterData();
	}
	
}
