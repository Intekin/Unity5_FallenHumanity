/// <summary>
/// VitalBar.cs
/// Christoffer Gustafsson
/// 05/08-2013
/// 
/// this class is responsible for displaying the vitalbar.
/// </summary>
using UnityEngine;
using System.Collections;

public class VitalBar : MonoBehaviour {
	public bool _isPlayerHealthBar;		//is it is player health or mob health.
	private int _maxBarLength; 				//the length of vitalbar when on 100%
	private int _currentBarLength;			//the current length of the vitalBar
	private GUITexture _display;

	// Use this for initialization
	void Awake()
	{
		_display = gameObject.GetComponent<GUITexture>();	
	}
	
	void Start ()
	{		
		_maxBarLength = (int)_display.pixelInset.width;
		
		_currentBarLength = _maxBarLength;
		_display.pixelInset = CalculatePosition();
		
		OnEnable();
	}
	
	//This is called when the GameObject is enabled
	public void OnEnable()
	{		
		if(_isPlayerHealthBar)
			Messenger<int, int>.AddListener("player health update", OnChangeHealthBarSize);
		else
			ToggleDisplay(false);
			Messenger<int, int>.AddListener("mob health update", OnChangeHealthBarSize);
			Messenger<bool>.AddListener("show mob vitalbars", ToggleDisplay);
	}
	
	//This is called when the GameObject is disabled
	public void OnDisable()
	{
		if(_isPlayerHealthBar)
			Messenger<int, int>.RemoveListener("player health update", OnChangeHealthBarSize);
		else 
		{
			Messenger<int, int>.RemoveListener("mob health update", OnChangeHealthBarSize);
			Messenger<bool>.RemoveListener("show mob vitalbars", ToggleDisplay);
		}
	}
	
	//this method will calculate the total size of the healthbar.
	public void OnChangeHealthBarSize(int currentHealth, int maxHealth)
	{
		_currentBarLength = (int)(((float)currentHealth / (float)maxHealth) * _maxBarLength);	//calculate the current bar length.
		
		_display.pixelInset = CalculatePosition();
	}
	
	//Setting the healthbar to signify the player or mob.
	public void SetPlayerHealth(bool b)
	{
		_isPlayerHealthBar = b;
	}
	
	private Rect CalculatePosition()
	{
		float yPos = Screen.height - _display.pixelInset.height - 10;
		
		if(!_isPlayerHealthBar)
		{
			float xPos = (Screen.width - (_currentBarLength + 10));
			return new Rect(xPos, yPos, _currentBarLength, _display.pixelInset.height);
		}
		
		return new Rect(10 , yPos, _currentBarLength, _display.pixelInset.height);
	}
	
	private void ToggleDisplay(bool show)
	{
		_display.enabled = show;	
	}
}
