using System.Collections.Generic;

public class PlayerCharacter : BaseCharacter {
	
	private static List<Item> _inventory = new List<Item>();
	
	private static Item _equipedWeapon;
	
	public static List<Item> Inventory
	{
		get{return _inventory;}	
	}
	
	public static Item EquipedWeapon
	{
		get{ return _equipedWeapon;}
		set{ _equipedWeapon = value;}
	}
	
	
}
