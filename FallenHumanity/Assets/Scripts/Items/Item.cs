using UnityEngine;

public class Item {
	private string _name;
	private int _price;
	private RarityTypes _rarity;
	private int _currentDur;
	private int _maxDur;
	private Texture2D _icon;
	
	public Item()
	{
		_name = "Need Name";
		_price = 0;
		_rarity = RarityTypes.Common;
		_maxDur = 50;
		_currentDur = _maxDur;
	}
	
	public Item( string name, int price, RarityTypes rare, int maxDur, int curDur)
	{
		_name = name;
		_price = price;
		_rarity = rare;
		_maxDur = maxDur;
		_currentDur = curDur;
	}
	
	public string Name
	{
		get{ return _name;}
		set{ _name = value;}
	}
	
	public int Value
	{
		get{ return _price;}
		set{ _price = value;}
	}
	
	public RarityTypes Rarity
	{
		get{ return _rarity;}
		set{ _rarity = value;}
	}
	
	public int MaxDurability
	{
		get{return _maxDur;}
		set{_maxDur = value;}
	}
	
	public int CurrentDurability
	{
		get{ return _currentDur;}
		set{_currentDur = value;}
	}
	
	public Texture2D Icon
	{
		get{ return _icon;}
		set{ _icon = value;}
	}
	
	public virtual string ToolTip()
	{
		return Name + "\n" + 
				"Durability: " + CurrentDurability + "/" + MaxDurability +"\n" +
				"Value: " + Value + "\n";
				
	}
}


public enum RarityTypes {
	Common,
	Uncommon,
	Rare,
	Unique,
	Legendary
}