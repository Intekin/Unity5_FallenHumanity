using UnityEngine;
using System.Collections;
using System;

public class CharacterGenerator : MonoBehaviour {
	private PlayerCharacter _player;
	
	private const int STARTING_POINTS = 5;
	private const int MIN_ATTRIBUTE_VALUE = 1;
	private const int STARTING_VALUE = 10;
	private int pointsLeft;
	
	private const int OFFSET = 5;
	private const int LINE_HIGHT = 20;
	
	private const int STAT_LABEL_WIDTH = 100;
	private const int BASE_LABEL_WIDTH = 30;
	
	private const int BUTTON_WIDTH = 20;
	private const int BUTTON_HIGHT = 20;
	private const int STAT_STARTING_POS = 40;
	
	public GUIStyle myStyle; 	//custimizing a single element.
	public GUISkin mySkin;		//customizing all elements
	
	public GameObject playerPrefab;
	
	public float delayTimer = 0.25f;
	
	// Use this for initialization
	void Start () {
		
		//GameObject pc = Instantiate(playerPrefab, Vector3.zero, Quaternion.identity)as GameObject;
		GameObject pc = playerPrefab;
		pc.name = "pc";
		
		_player = pc.GetComponent<PlayerCharacter>();
		
		pointsLeft = STARTING_POINTS;
	
		for(int i = 0; i < Enum.GetValues(typeof(AttributeName)).Length; i++)
		{
			_player.GetPrimaryAttribute(i).BaseValue = STARTING_VALUE;
		}
		_player.StatUpdate();
	}
	
	// Update is called once per frame
	void Update () {
		_player.StatUpdate();
	}
	
	void OnGUI()
	{
		DisplayName();
		DisplayPointsLeft();
		DisplayAttributes();
		DisplayVitals();
		DisplaySkills();
		
		if(!(_player.Name == "" || pointsLeft > 0))
			DisplayCreateButton();
		else
			DisplayButtonOverlay();
	}
	
	private void DisplayName()
	{
		GUI.Label(new Rect(10,10,50,25), "Name:");
		_player.Name = GUI.TextField(new Rect(65,10,100,25), _player.Name);
	}
	
	private void DisplayAttributes()
	{
		for(int i = 0; i < Enum.GetValues(typeof(AttributeName)).Length; i++)
		{
			GUI.Label(new Rect(	OFFSET, 
								STAT_STARTING_POS + (i * LINE_HIGHT), 
								STAT_LABEL_WIDTH, 
								LINE_HIGHT
				), ((AttributeName)i).ToString());
			
			GUI.Label(new Rect(	STAT_LABEL_WIDTH + OFFSET, 
								STAT_STARTING_POS + (i * LINE_HIGHT), 
								BASE_LABEL_WIDTH, 
								LINE_HIGHT
				), _player.GetPrimaryAttribute(i).AdjustedBaseValue.ToString());
			
			if(GUI.Button(new Rect(	OFFSET + STAT_LABEL_WIDTH + BASE_LABEL_WIDTH, 
									STAT_STARTING_POS + (i * BUTTON_HIGHT), 
									BUTTON_WIDTH, 	
									BUTTON_HIGHT), "-"))
			{
				if(_player.GetPrimaryAttribute(i).BaseValue > MIN_ATTRIBUTE_VALUE)
				{
					_player.GetPrimaryAttribute(i).BaseValue--;
					pointsLeft++;
					_player.StatUpdate();
				}
			}
			if(GUI.Button(new Rect(	OFFSET + STAT_LABEL_WIDTH + BASE_LABEL_WIDTH + BUTTON_WIDTH, 
									STAT_STARTING_POS + (i * BUTTON_HIGHT), 
									BUTTON_WIDTH, 
									BUTTON_HIGHT), "+"))
			{
				if(pointsLeft > 0)
				{
					_player.GetPrimaryAttribute(i).BaseValue++;
					pointsLeft--;
					_player.StatUpdate();
				}
			}
		}		
	}
	
	private void DisplayVitals()
	{
		for(int i = 0; i < Enum.GetValues(typeof(VitalName)).Length; i++)
		{
			GUI.Label(new Rect(	OFFSET, 
								STAT_STARTING_POS + ((i + Enum.GetValues(typeof(AttributeName)).Length)* LINE_HIGHT), 
								STAT_LABEL_WIDTH, 
								LINE_HIGHT
				), ((VitalName)i).ToString());
			
			GUI.Label(new Rect(	STAT_LABEL_WIDTH + OFFSET, 
								STAT_STARTING_POS + ((i + Enum.GetValues(typeof(AttributeName)).Length)* LINE_HIGHT), 
								BASE_LABEL_WIDTH, 
								LINE_HIGHT
				), _player.GetVital(i).AdjustedBaseValue.ToString());
		}		
	}
	
	private void DisplaySkills()
	{
		for(int i = 0; i < Enum.GetValues(typeof(SkillName)).Length; i++)
		{
			GUI.Label(new Rect(	OFFSET + STAT_LABEL_WIDTH + BASE_LABEL_WIDTH + BUTTON_WIDTH*2 + OFFSET*2, 
								STAT_STARTING_POS + (i * LINE_HIGHT), 
								STAT_LABEL_WIDTH, 
								LINE_HIGHT
				), ((SkillName)i).ToString());
			
			GUI.Label(new Rect(	OFFSET + STAT_LABEL_WIDTH + BASE_LABEL_WIDTH + BUTTON_WIDTH*2 + STAT_LABEL_WIDTH + OFFSET*2, 
								STAT_STARTING_POS + (i * LINE_HIGHT), 
								BASE_LABEL_WIDTH, 
								LINE_HIGHT
				), _player.GetSkills(i).AdjustedBaseValue.ToString());
		}		
	}
	
	private void DisplayPointsLeft()
	{
		GUI.Label(new Rect(250,10,100,25), "Points Left: " + pointsLeft.ToString());
	}
	
	private void DisplayButtonOverlay()
	{
		GUI.Label(new Rect(Screen.width/2 - 50, STAT_STARTING_POS + (10*LINE_HIGHT), STAT_LABEL_WIDTH, LINE_HIGHT),"Enter Name", "Button");
	}
	
	private void DisplayCreateButton()
	{
		if(GUI.Button(new Rect(Screen.width/2 - 50,	STAT_STARTING_POS + (10*LINE_HIGHT), STAT_LABEL_WIDTH, LINE_HIGHT),"Create"))
		{
			//GameSettings gsScript = GameObject.Find("__GameSettings").GetComponent<GameSettings>();
			
			UpdateCurrentVitalValue();
			
			//gsScript.SaveCharacterData();
			
			Application.LoadLevel("HexWorld_01");
		}
	}
	
	private void UpdateCurrentVitalValue()
	{
		for(int i = 0; i < Enum.GetValues(typeof(VitalName)).Length; i++)
		{
			_player.GetVital(i).CurrentValue = _player.GetVital(i).AdjustedBaseValue;	
		}
	}
}
