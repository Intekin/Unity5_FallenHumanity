using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MobGenerator : MonoBehaviour {
	public enum State{
		Idle,
		Inintalize,
		Setup,
		SpawnMob
	}
	public GameObject[] mobPrefabs;		//an array to hold all the mob Prefabs that will spawn.
	public GameObject[] spawnPoints;	//an array to hold all the spawn points.
	
	public State state;
	
	void Awake()
	{
	state = MobGenerator.State.Inintalize;	
	}
	
	// Use this for initialization
	IEnumerator Start () {
		
		while(true)
		{
			switch(state)
			{
			case State.Inintalize:
				Initialize();
				break;
				
			case State.Setup:
				Setup();
				break;
					
			case State.SpawnMob:
				SpawnMob();
				break;
			}
			
			yield return 0;
		}
	}
	
	private void Initialize()
	{
		Debug.Log("***We are in the Initialize Function***");
		
		if(!CheckForMobPrefabs() && !CheckForSpawnPoints())
			return;
		
		state = MobGenerator.State.Setup;
	}
	
	private void Setup()
	{
		Debug.Log("***We are in the Setup Function***");
		state = MobGenerator.State.SpawnMob;
	}
	
	private void SpawnMob()
	{
		Debug.Log("***SpawnMob***");
		
		GameObject[] gos = AvailableSpawnPoints();
		
		for(int i = 0; i < gos.Length; i++)
		{
			GameObject go = Instantiate(mobPrefabs[Random.Range(0, mobPrefabs.Length)],
				gos[i].transform.position, Quaternion.identity) as GameObject;
			
			go.transform.parent = gos[i].transform;
		}
		
		state = MobGenerator.State.Idle;
	}
	
	//Check to see that we have at least one mob
	private bool CheckForMobPrefabs()
	{
		if(mobPrefabs.Length > 0)
			return true;
		else
			return false;
	}
	
	//Check to see that we have at least one spawnpoint to spawning.
	private bool CheckForSpawnPoints()
	{
		if(spawnPoints.Length > 0)
			return true;
		else
			return false;
	}
	
	//generate a list for available spewnpoints that does not have a child to it.
	private GameObject[] AvailableSpawnPoints()
	{
		List<GameObject> goSP = new List<GameObject>();
		
		for(int i = 0; i < spawnPoints.Length; i++)
		{
			if(spawnPoints[i].transform.childCount == 0)
			{
				goSP.Add(spawnPoints[i]);
			}
		}
		return goSP.ToArray();
	}
}
