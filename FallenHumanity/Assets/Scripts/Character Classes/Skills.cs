/// <summary>
/// Skills.cs
/// 06/08-2013
/// Christoffer Gustafsson
/// 
/// This class contains all the extra functions for the Skill.
/// </summary>
public class Skills : ModifiedStat 
{
	private bool _known;			//a boolean verible to toggle if the skill known for the character
	
	/// <summary>
	/// Initializes a new instance of the <see cref="Skills"/> class.
	/// </summary>
	public Skills()
	{		
		_known = false;
		ExpToLevel = 25;
		LevelModifier = 1.1f;
	}
	
	/// <summary>
	/// Gets or sets a value indicating whether this <see cref="Skills"/> is known.
	/// </summary>
	/// <value>
	/// <c>true</c> if known; otherwise, <c>false</c>.
	/// </value>
	public bool Known
	{
		get{ return _known;}
		set{ _known = value;}
	}
}

/// <summary>
/// A list of skills that the player can learn.
/// </summary>
public enum SkillName
{
	MeleeWeapon,
	ShortSword,
	LongSword,
	Mace,
	Flail,
	LightArmor,
	MediumArmor,
	HeavyArmor,
	Forgeing,
	WeaponSmith,
	ArmorSmith,
	Sneaking,
	Lockpicking,
	Barter,
	Speech,
	Bow,
	Crossbow,
	MagicAttunement,
	WindMagic,
	FireMagic,
	IceMagic,
	EarthMagic,
	ArcaneMagic,
	SummoningMagic,
	IllusionMagic
}
