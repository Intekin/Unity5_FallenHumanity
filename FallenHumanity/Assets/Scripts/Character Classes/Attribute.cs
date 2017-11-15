/// <summary>
/// Attribute.cs
/// Christoffer Gustafsson
/// 05/08-2013
/// 
/// This is the class for all the character attributes in-game
/// </summary>
public class Attribute : BaseStat {
	new public const int STARTING_EXP_COST = 50;	//this is the starting cost of all my attributes
	
	/// <summary>
	/// Initializes a new instance of the <see cref="Attribute"/> class.
	/// </summary>
	public Attribute() 			
	{
		ExpToLevel = STARTING_EXP_COST;
		LevelModifier = 1.05f;
	}
}

/// <summary>
/// This is a list of all the attributes that we will have in-game for our characters.
/// </summary>
public enum AttributeName
{
	Strength, 
	Constitution,
	Agility,
	Speed,
	Concentration,
	Intelligence,
	Willpower,
	Charisma
}
