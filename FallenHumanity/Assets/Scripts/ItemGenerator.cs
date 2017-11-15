using UnityEngine;
using System.Collections;

public static class ItemGenerator{
	public const int BASE_MELEE_RANGE = 1;
	public const int BASE_RANGED_RANGE = 5;
	
	private static string MELEE_WEAPON_PATH = "ItemIcons/Weapon/Melee/";
	private static string DEFAULT_ICON_PATH = "NoIcon";
	
	public static Item CreateItem()
	{
		Item item = CreateWeapon();
					
		item.Value = Random.Range(1,100);
		item.Rarity = RarityTypes.Common;
		item.MaxDurability = Random.Range(50, 61);
		item.CurrentDurability = item.MaxDurability;
		
		return item;
	}
	private static Weapon CreateWeapon()
	{
		Weapon weapon = CreateMeleeWeapon();
		
		return weapon;
		
	}
	private static Weapon CreateMeleeWeapon()
	{
		Weapon meleeWeapon = new Weapon();
		
		string[] weaponNames = new string[3];
		weaponNames[0] = "ShortSword";
		weaponNames[1] = "LongSword";
		weaponNames[2] = "Warhammer";
		
		meleeWeapon.Name = weaponNames[Random.Range(0, weaponNames.Length)];
		
		meleeWeapon.MaxDamage = Random.Range(10,20);
		meleeWeapon.DamageVariance = Random.Range(.2f, .6f);
		meleeWeapon.TypeOfDamage = DamageType.Slash;
		meleeWeapon.MaxRange = BASE_MELEE_RANGE;
		
		meleeWeapon.Icon = (Texture2D)Resources.Load(MELEE_WEAPON_PATH + meleeWeapon.Name);
		
		if(meleeWeapon.Icon == null)	
		{
			Debug.LogWarning("Icon not found: " + meleeWeapon.Name);
			meleeWeapon.Icon = (Texture2D)Resources.Load("NoIcon");
		}
				
		return meleeWeapon;
	}

}

public enum ItemType
{
	Armor,
	Weapon,
	Potion
}