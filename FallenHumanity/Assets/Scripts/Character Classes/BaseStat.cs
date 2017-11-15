/// <summary>
/// BaseStat.cs
/// Christoffer Gustafsson
/// 04/08-2013
/// 
/// This is the base stat class for all stats in game.
/// </summary>
public class BaseStat 
{
	public const int  STARTING_EXP_COST = 100; 	//publicly accessable starting exp.
	
	private int _baseValue;						//The base value of this stat
	private int _buffValue;						//The buff amount to the stat
	private int _expToLevel;					//The total amount of exp nedded to gain a skilllevel
	private float _levelModifier;				//The modifier applied to the experiance nedded to gain a skilllevel
	private string _name;
	
	
	/// <summary>
	/// Initializes a new instance of the <see cref="BaseStat"/> class.
	/// </summary>
	public BaseStat()
	{
		_baseValue = 0;
		_buffValue = 0;
		_levelModifier = 1.1f;
		_expToLevel = STARTING_EXP_COST;
		_name = "";
	}

#region Set and Get
	
	//Basic Setters and Getters
	public int BaseValue
	{
		get{ return _baseValue; }
		set{ _baseValue = value; }
	}
	
	public int BuffValue
	{
		get{ return _buffValue; }
		set{ _buffValue = value; }
	}
	
	public int ExpToLevel
	{
		get{ return _expToLevel; }
		set{ _expToLevel = value; }
	}
	
	public float LevelModifier
	{
		get{ return _levelModifier; }
		set{ _levelModifier = value; }
	}
	
	public string Name
	{
		get{return _name;}
		set{ _name = value;}
	}
	
#endregion
	
	/// <summary>
	/// Calculates the exp to level.
	/// </summary>
	/// <returns>
	/// The exp to level.
	/// </returns>
	private int CalculateExpToLevel()
	{
		return (int)(_expToLevel * _levelModifier);	
	}
	
	/// <summary>
	/// Assign the new value to _expToLevel and then increse the baseValue by one
	/// </summary>
	public void LevelUp()
	{
		_expToLevel = CalculateExpToLevel();
		_baseValue++;
	}
	/// <summary>
	/// Recalculate the adjusted base values and return it.
	/// </summary>
	/// <value>
	/// The adjusted base value.
	/// </value>
	public int AdjustedBaseValue
	{
		get {return _baseValue + _buffValue;}
	}
}