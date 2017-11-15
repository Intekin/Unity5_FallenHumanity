/// <summary>
/// Vital.sc
/// 06/08-2013
/// Christoffer Gustafsson
/// 
/// this class contains all the extra functions for characters vitals.
/// </summary>
public class Vital : ModifiedStat 
{
	private int _currentValue;	//this is the current value of this vital.
	
	/// <summary>
	/// Initializes a new instance of the <see cref="Vital"/> class.
	/// </summary>
	public Vital()
	{
		_currentValue = 0;
		ExpToLevel = 50;
		LevelModifier = 1.1f;
	}
	
	/// <summary>
	/// when getting the _currentValue, make sure it is not greater then the our AdjustedBaseValue.
	/// if it is make it the same as the AdjustedBaseValue.
	/// </summary>
	/// <value>
	/// The current value.
	/// </value>
	public int CurrentValue
	{
		get{ 
			if(_currentValue < AdjustedBaseValue)
				_currentValue = AdjustedBaseValue;
			
				return _currentValue; }
		set{ _currentValue = value;}
	}

}

/// <summary>
/// A list of the vitals a character have.
/// </summary>
public enum VitalName 
{
	Health,
	Stanima,
	Mana
}
