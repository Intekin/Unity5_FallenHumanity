using UnityEngine;
using System.Collections;

public class PlayerHealth : MonoBehaviour {
	public int maxHealth = 100;
	public int currentHelth = 100;
	
	public float healthBarLength;
	
	// Use this for initialization
	void Start () 
	{
		healthBarLength = Screen.width /2;
	
	}
	
	// Update is called once per frame
	void Update () 
	{
		AdjustCurrentHealth(0);
	}
	
	void OnGUI() 
	{
		GUI.Box(new Rect(10, 10, healthBarLength, 20 ), currentHelth + "/" + maxHealth);
		
	}
	
	public void AdjustCurrentHealth(int adj)
	{
		currentHelth += adj;
		
		if(currentHelth < 0)
			currentHelth = 0;
		
		if(currentHelth > maxHealth)
			currentHelth = maxHealth;
		
		if(maxHealth < 1)
			maxHealth = 1;
		
		healthBarLength = (Screen.width/2) * currentHelth / (float)maxHealth;
	}
}