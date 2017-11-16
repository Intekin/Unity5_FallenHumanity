//Class to controll the different types of containers. 

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[RequireComponent (typeof(AudioSource))]

public class Container: MonoBehaviour {
	public enum State
	{
		Open,
		Close,
		InProgress
	}
	public State state;
	
	public AudioClip openSound;
	public AudioClip closeSound;
	AudioSource audioSource;
	
	public string ContainerName = "Container";
	public bool randomLoot = true;
	public int maxNumberOfItems = 5;
	public int numberOfItems = 3;
	
	public GameObject[] parts;
	
	private Color[] _defaultColors;	
	private bool _isActive = false;
	private bool _used = false;
	private int _timer = 3;
	
	private string _empty = "";
	
	
	public List<Item> containerLoot = new List<Item>();

	// Use this for initialization
	void Start () 
	{
		audioSource = GetComponent<AudioSource> (); 
		state = Container.State.Close;
		
		if(randomLoot)
			numberOfItems = Random.Range(1, maxNumberOfItems);			
		
		_defaultColors = new Color[parts.Length];
		
		if(parts.Length >0)
			for(int i = 0; i < _defaultColors.Length; i++)
				_defaultColors[i] = parts[i].GetComponent<Renderer>().material.GetColor("_Color");
	
		
	}
	
	void Update()
	{
		if(_timer > 0) //Timer to disable the highlight.
		{
			_timer--;
			if(_timer == 0)
			{
				_isActive = false;
				Highlight(false);
				Close();
			}
		}
	}
	
	
	void OnGUI()
	{
		if(_isActive)
		{
			GUI.Label(new Rect((Screen.width - 100) /2, (Screen.height-60)/2, 100,30), ContainerName + _empty );	
		}
	}
	
	public void ContainerEnter()
	{
		_timer = 3;
		Highlight(true);
		_isActive = true;
	}
	
	public void Activate()
	{

		if(state == Container.State.Close)
		{
			Debug.Log("Open");
			Open();
		}
		else if(state == Container.State.Open)
		{
			Debug.Log("Close");
			Close();
		}
		else 
			return;
	}
	
	private void Highlight(bool glow)
	{
		if(glow)
		{
			if(parts.Length >0)
				for(int i = 0; i < _defaultColors.Length; i++)
					parts[i].GetComponent<Renderer>().material.SetColor("_Color", Color.red);
			//Debug.Log("Higligth");
		}
		else
		{
			if(parts.Length >0)
				for(int i = 0; i < _defaultColors.Length; i++)
					parts[i].GetComponent<Renderer>().material.SetColor("_Color", _defaultColors[i]);
		}
	}
	
	private void Open()
	{
		myGUI.container = this;
		
		if(!_used)
			PopulateChest(numberOfItems);
		
		audioSource.PlayOneShot(openSound);
		
		state = Container.State.Open;
		
		Messenger.Broadcast("DisplayLoot");
	}
	
	private void PopulateChest(int x)
	{
		for(int i = 0; i < x; i++)
		{
			containerLoot.Add(ItemGenerator.CreateItem());
		}
		
		_used = true;
	}
	
	private void Close()
	{
		audioSource.PlayOneShot(closeSound);
		state = Container.State.Close;

		if(containerLoot.Count == 0 && _used)
			_empty = "(empty)";
	}
}
