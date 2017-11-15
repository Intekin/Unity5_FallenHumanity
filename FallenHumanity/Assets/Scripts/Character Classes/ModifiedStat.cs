/// <summary>
/// ModifiedStat.cs
/// Christoffer Gustafsson
/// 05/08-2013
/// 
/// This is the base class for all stats that will be modifiable by attributes.
/// </summary>

using System.Collections.Generic; //Generic was added for the use of List<>

public class ModifiedStat : BaseStat {

	private List<ModifyingAttribute> _mods; //A list of attributes that modifies this stat.
	private int _modValue;					//The amount added to the base value from the modifiers.
	
	/// <summary>
	/// Initializes a new instance of the <see cref="ModifiedStat"/> class.
	/// </summary>
	public ModifiedStat()
	{
		_mods = new List<ModifyingAttribute>();
		_modValue = 0;
	}
	
	/// <summary>
	/// Add a ModifiyingAttribute to our list of mods for this modifiedStat.
	/// </summary>
	/// <param name='mod'>
	/// Mod.
	/// </param>
	public void AddModifier(ModifyingAttribute mod)
	{
		_mods.Add(mod);
	}
	
	/// <summary>
	/// Reset the mod value to 0.
	/// Check to se if there is at least one modifying attribute in our list of mods.
	/// if we do, then iterate through the list and the AdjustedBaseValue * ratio to our _modValue.
	/// </summary>
	private void CalculateModValue()
	{
		_modValue = 0;	
		
		if(_mods.Count > 0)
		{
			foreach(ModifyingAttribute att in _mods)
				_modValue += (int)(att.attribute.AdjustedBaseValue * att.ratio);
		}	
	}
	
	/// <summary>
	/// This function is overriding the AdjustedBaseValue in the BaseStat.
	/// Calculate the adjusted base value from the BaseValue + BuffValue + _modValue.
	/// </summary>
	/// <value>
	/// The adjusted base value.
	/// </value>
	public new int AdjustedBaseValue
	{
		get {return BaseValue + BuffValue + _modValue;}
	}
	
	/// <summary>
	/// Update this instance.
	/// </summary>
	public void Update()
	{
		CalculateModValue();	
	}
	
	public string GetModifyingAttributeString()
	{
		string temp = "";
		
		for( int i = 0; i < _mods.Count; i++)
		{
			temp += _mods[i].attribute.Name;
			temp += "_";
			temp += _mods[i].ratio;
			
			if(i < _mods.Count - 1)
				temp += "|";
		}
		
		return temp;
	}
}


/// <summary>
/// A structure that will hold an Attribute and a ratio that will be added as a modifying attribute to our ModifiedStats.
/// </summary>
public struct ModifyingAttribute 
{
	public Attribute attribute;		//the attribute to be used as an modifier.
	public float ratio;				//the percent of the attributes AdjustedBaseValue that will be applied to the ModifiedStat.
	
	/// <summary>
	/// Initializes a new instance of the <see cref="ModifyingAttribute"/> struct.
	/// </summary>
	/// <param name='att'>
	/// Att. the attribute to use
	/// </param>
	/// <param name='rat'>
	/// Rat. the ratio to use
	/// </param>
	public ModifyingAttribute(Attribute att, float rat)
	{
		attribute = att;
		ratio = rat;
	}
}
