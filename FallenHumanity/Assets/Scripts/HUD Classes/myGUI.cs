using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class myGUI : MonoBehaviour {
	

	public float lootWindowHeight = 100;
	
	public float buttonWidth = 40;
	public float buttonHeight = 40;
	public float closeButtonSize = 20;
	private float _offset = 10f;	
	
	private bool _displayCharacterScreen = false;

	/**********************************
	* Loot Window Varibles
	**********************************/	
	
	private bool _displayLootWindow = false;
	private const int LOOT_WINDOW_ID = 0;
	private Rect _lootWindowRect = new Rect(300, 10, 170, 265);
	private Vector2 _lootWindowSlider = Vector2.zero;
	
	public static Container container;
	
	private string _toolTip = "";
	
	/**********************************
	* Inventory Window Varibles
	**********************************/
	
	private bool _displayInventoryWindow = false;
	private const int INVENTORY_WINDOW_ID = 1;
	private Rect _inventoryWindowRect = new Rect(10, 300, 170, 265);
	private int _inventoryRow = 6;
	private int _inventoryCol = 10;
	
	private float _doubleClickTimer = 0;
	private const float _DOUBLE_CLICK_TIMER_THRESHHOLD = 0.5f;
	private Item _selectedItem;
	
	/* Character window */
	
	private bool _displayCharacterWindow = false;
	private const int CHARACTER_WINDOW_ID = 2;
	private Rect _characterWindowRect = new Rect(10, 10, 200, 400);
	private int _charaterTab = 0;
	private string[] _characterTabNames = new string[] {"Equipment", "Attributes", "Skills"};

	/* Menu Window */

	private bool _displayMenuWindow = false;
	private const int MENU_WINDOW_ID = 2;
	private Rect _menuWindowRect = new Rect((Screen.width/2) - 100, (Screen.height/2) - 200, 200, 300);


	// Use this for initialization
	void Start() 
	{
        Cursor.lockState = CursorLockMode.Locked;
		RecalculateGUI();
		DontDestroyOnLoad(this);
	}
	
	private void OnEnable()
	{
		Messenger.AddListener("DisplayLoot", DisplayLoot);
		Messenger.AddListener("CharacterScreen", DisplayCharacterScreen);
		Messenger.AddListener("DisplayMenu", DisplayMenu);
	}
	
	private void OnDisable()
	{
		Messenger.RemoveListener("DisplayLoot", DisplayLoot);
		Messenger.RemoveListener("CharacterScreen", DisplayCharacterScreen);
		Messenger.RemoveListener("DisplayMenu", DisplayMenu);

	}
	
	void OnGUI()
	{
		if(_displayInventoryWindow)
		{
			_inventoryWindowRect = GUI.Window(INVENTORY_WINDOW_ID, _inventoryWindowRect, InventoryWindow, "Inventory");
		}
		
		if(_displayCharacterWindow)
		{
			_characterWindowRect = GUI.Window(CHARACTER_WINDOW_ID, _characterWindowRect, CharacterWindow, "Character");
		}
		
		if(_displayLootWindow)
		{
			_lootWindowRect = GUI.Window(LOOT_WINDOW_ID, _lootWindowRect, LootWindow, container.ContainerName);
		}

		if(_displayMenuWindow)
		{
			_menuWindowRect = GUI.Window(MENU_WINDOW_ID, _menuWindowRect, MenuWindow, "Menu");
		}
		
		if(_displayLootWindow || _displayInventoryWindow || _displayCharacterWindow || _displayMenuWindow)
			EnableMouseLook(false);
		else
			EnableMouseLook(true);
		
		DisplayToolTip();
	}

	private void MenuWindow(int id)
	{
		if(GUI.Button(new Rect(10, 20, 180, 20),"Return to Main Menu"))
		{
			DisplayMenu();
			//Application.LoadLevel("MainMenu");
		}
		if(GUI.Button(new Rect(10, 50, 180, 20),"QUIT"))
		{
			Application.Quit();			
		}
	}

	private void LootWindow(int id)
	{
		if(GUI.Button(new Rect(_lootWindowRect.width - closeButtonSize, 0, closeButtonSize, closeButtonSize), "x")|| Input.GetButtonUp("Activate"))
		{
			ClearWindow();
		}
		
		if(container == null)
			return;
		
		_lootWindowSlider = GUI.BeginScrollView(new Rect(_offset * 0.5f, 15, _lootWindowRect.width - 10, 70), _lootWindowSlider, new Rect(0, 0, (container.containerLoot.Count * buttonWidth) + _offset, buttonHeight + _offset));
		
		for(int i = 0; i < container.containerLoot.Count; i++)
		{
			if(GUI.Button(new Rect(buttonWidth * i, 0, buttonWidth, buttonHeight), new GUIContent(container.containerLoot[i].Icon, container.containerLoot[i].ToolTip())))
			{
				PlayerCharacter.Inventory.Add(container.containerLoot[i]);
				container.containerLoot.RemoveAt(i);
			}
		}
		
		GUI.EndScrollView();
		
		SetToolTip();
	}
	
	private void InventoryWindow(int id)
	{
		int i = 0;
		
		for(int y = 0; y < _inventoryRow; y++)
		{
			for(int x = 0; x < _inventoryCol; x++)
			{
				if( i < PlayerCharacter.Inventory.Count)
				{
					if(GUI.Button(new Rect(5 + (x*buttonWidth), 20 + (y * buttonHeight), buttonWidth, buttonHeight), new GUIContent(PlayerCharacter.Inventory[i].Icon, PlayerCharacter.Inventory[i].ToolTip())))
					{
						if((_doubleClickTimer != 0) && (_selectedItem != null))
						{
							if(Time.time - _doubleClickTimer < _DOUBLE_CLICK_TIMER_THRESHHOLD)
							{
								Debug.Log("Double Click" + PlayerCharacter.Inventory[i].Name); 
								
								if(PlayerCharacter.EquipedWeapon == null)
								{
									PlayerCharacter.EquipedWeapon = PlayerCharacter.Inventory[i];
									PlayerCharacter.Inventory.RemoveAt(i);
								}
								else
								{
									Item temp = PlayerCharacter.EquipedWeapon;	
									PlayerCharacter.EquipedWeapon = PlayerCharacter.Inventory[i];
									PlayerCharacter.Inventory[i] = temp;
								}
								
								_doubleClickTimer = 0;
								_selectedItem = null;
							}
						}
						else
						{
							_doubleClickTimer = Time.time;
							_selectedItem = PlayerCharacter.Inventory[i];
						}
					}
				}
				else 
				{
					GUI.Label(new Rect(5 + (x*buttonWidth), 20 + (y * buttonHeight), buttonWidth, buttonHeight),	(x + y * _inventoryCol).ToString(),"box");	
				}
				
				i++;
			}
		}
		SetToolTip();
		GUI.DragWindow();
	}
	
	private void CharacterWindow(int id)
	{
		_charaterTab = GUI.Toolbar(new Rect(5, 25, _characterWindowRect.width - 10, 50), _charaterTab, _characterTabNames);
		
		switch(_charaterTab)
		{
		case 0:
			DisplayEquipment();
			break;
		case 1:
			DisplayAttributes();
			break;
		case 2:
			DisplaySkills();
			break;
		}
		
		GUI.DragWindow();
	}
	
	private void DisplayLoot()
	{
		_displayLootWindow = true;	
	}

	private void DisplayMenu()
	{
		Debug.Log("Toggle: " + _displayMenuWindow);
		_displayMenuWindow = !_displayMenuWindow;
	}
	
	private void DisplayInventory()
	{
		_displayInventoryWindow = !_displayInventoryWindow;	
	}
	
	private void DisplayCharacter()
	{
		_displayCharacterWindow = !_displayCharacterWindow;	
	}
	
	private void DisplayCharacterScreen()
	{
		_displayCharacterScreen = !_displayCharacterScreen;
		
		if(_displayCharacterScreen)
		{
			_displayInventoryWindow = true;
			_displayCharacterWindow = true;
		}
		else
		{
			_displayInventoryWindow = false;
			_displayCharacterWindow = false;
		}
	}
	
	private void DisplayEquipment()
	{
		Debug.Log("Eqipment");
		if(PlayerCharacter.EquipedWeapon == null)
		{
			GUI.Label(new Rect(5, 100, 40, 40),"X");
		}
		else
		{
			if(GUI.Button(new Rect(5, 100, 40, 40), new GUIContent(PlayerCharacter.EquipedWeapon.Icon, PlayerCharacter.EquipedWeapon.ToolTip())))
			{
				PlayerCharacter.Inventory.Add(PlayerCharacter.EquipedWeapon);
				PlayerCharacter.EquipedWeapon = null;
			}
		}
		
		SetToolTip();
	}
	
	private void DisplaySkills()
	{
		Debug.Log("Skills");
	}
	
	private void DisplayAttributes()
	{
		Debug.Log("Attributes");
	}
	
	private void ClearWindow()
	{		
		container = null;
		_displayLootWindow = false;	
	}
	     
	private void EnableMouseLook(bool enable)
    {
   	 	GameObject pc = GameObject.FindWithTag("Player");
     
    	pc.transform.GetComponent<CharacterMotor>().enabled = enable;
    	pc.transform.GetComponent<MouseLook>().enabled = enable;
    	Camera.main.GetComponent<MouseLook>().enabled = enable;
		Cursor.lockState = CursorLockMode.None;
    }
		
	private void SetToolTip()
	{
		if(Event.current.type == EventType.Repaint && GUI.tooltip != _toolTip)
		{
			if(_toolTip != "")
				_toolTip = "";
			
			if(GUI.tooltip != "")
				_toolTip = GUI.tooltip;
		}
	}
	
	private void DisplayToolTip()
	{
		if(_toolTip != "")
		{
			GUI.Box(new Rect(Input.mousePosition.x,Screen.height - Input.mousePosition.y, 200, 100), _toolTip);
			GUI.depth = 0;
		}
			
	}
	
	private void RecalculateGUI()
	{
		_characterWindowRect = new Rect(_offset, _offset, (Screen.width/2) - (_offset*2), (Screen.height/2) - (_offset*2));
		_inventoryWindowRect = new Rect(_offset, (Screen.height/2)+_offset, Screen.width - (_offset*2), (Screen.height/2)-(_offset*2));
		
	}
}
