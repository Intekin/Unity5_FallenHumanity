using UnityEngine;
using System.Collections;

public class Mob: BaseCharacter {
	
	public int currentHealth;
	public int maxHealth;

	// Use this for initialization
	void Start () {
		
		Name = "Renegade Alien"; 
	
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	public void DisplayHealth()
	{
		//Messenger<int, int>.Broadcast("mob health update", currentHealth, maxHealth);
	}
}
