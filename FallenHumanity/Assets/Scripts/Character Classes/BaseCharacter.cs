using UnityEngine;
using System.Collections;
using System; //for enum

public class BaseCharacter : MonoBehaviour {
	
	private string _name;
	private int _level;
	private uint _freeExp;
	
	private Attribute[] _primaryAttribute;
	private Vital[] _vital;
	private Skills[] _skills;
	
	public void Awake() 
	{
		Debug.Log("Awake fired");
		
		_name = string.Empty;
		_level = 0;
		_freeExp = 0;
		
		_primaryAttribute = new Attribute[Enum.GetValues(typeof(AttributeName)).Length];
		_vital = new Vital[Enum.GetValues(typeof(VitalName)).Length];
		_skills = new Skills[Enum.GetValues(typeof(SkillName)).Length];
		
		SetupPrimaryAttributes();
		SetupVitals();
		SetupSkills();
		
	}
	
	public string Name
	{
		get { return _name;}
		set { _name = value;}
	}
	
	public int Level
	{
		get { return _level;}
		set { _level = value;}
	}
	
	public uint FreeExp
	{
		get {return _freeExp;}
		set { _freeExp = value;}
	}
	
	public void AddExp(uint exp)
	{
		_freeExp += exp;
		CalculateLevel();
	}
	
	public void CalculateLevel()
	{
		
	}
	
	private void SetupPrimaryAttributes()
	{
		for(int cnt = 0; cnt < _primaryAttribute.Length; cnt++)
		{
			_primaryAttribute[cnt] = new Attribute();
			_primaryAttribute[cnt].Name = ((AttributeName)cnt).ToString();
		}
	}
	
	private void SetupVitals()
	{
		for(int cnt = 0; cnt < _vital.Length; cnt++)
		{
			_vital[cnt] = new Vital();
		}
		SetupVitalModifiers();
	}
	
	private void SetupSkills()
	{
		for(int cnt = 0; cnt < _skills.Length; cnt++)
		{
			_skills[cnt] = new Skills();	
		}
		SetupSkillModifiers();
	}
	
	public Attribute GetPrimaryAttribute(int index)
	{
	 	return _primaryAttribute[index];
		
	}
	
	public Vital GetVital(int index)
	{
	 	return _vital[index];	
	}
	
	public Skills GetSkills(int index)
	{
	 	return _skills[index];	
	}
	
	private void SetupVitalModifiers()
	{
		//health
		GetVital((int)VitalName.Health).AddModifier(new ModifyingAttribute(GetPrimaryAttribute((int)AttributeName.Constitution), 5f));
		GetVital((int)VitalName.Health).AddModifier(new ModifyingAttribute(GetPrimaryAttribute((int)AttributeName.Strength), 2f));
		GetVital((int)VitalName.Health).AddModifier(new ModifyingAttribute(GetPrimaryAttribute((int)AttributeName.Willpower), 0.5f));
		//stanima
		GetVital((int)VitalName.Stanima).AddModifier(new ModifyingAttribute(GetPrimaryAttribute((int)AttributeName.Constitution), 1f));
		GetVital((int)VitalName.Stanima).AddModifier(new ModifyingAttribute(GetPrimaryAttribute((int)AttributeName.Agility), 3f));
		GetVital((int)VitalName.Stanima).AddModifier(new ModifyingAttribute(GetPrimaryAttribute((int)AttributeName.Speed), 2f));
		GetVital((int)VitalName.Stanima).AddModifier(new ModifyingAttribute(GetPrimaryAttribute((int)AttributeName.Strength), .5f));
		GetVital((int)VitalName.Stanima).AddModifier(new ModifyingAttribute(GetPrimaryAttribute((int)AttributeName.Willpower), .5f));
		//mana
		GetVital((int)VitalName.Mana).AddModifier(new ModifyingAttribute(GetPrimaryAttribute((int)AttributeName.Concentration), 2f));
		GetVital((int)VitalName.Mana).AddModifier(new ModifyingAttribute(GetPrimaryAttribute((int)AttributeName.Intelligence), 3f));
		GetVital((int)VitalName.Mana).AddModifier(new ModifyingAttribute(GetPrimaryAttribute((int)AttributeName.Willpower), 1f));
	}
	
	private void SetupSkillModifiers()
	{
		//Weapon skills
		GetSkills((int)SkillName.MeleeWeapon).AddModifier(new ModifyingAttribute(GetPrimaryAttribute((int)AttributeName.Strength), .50f));
		GetSkills((int)SkillName.MeleeWeapon).AddModifier(new ModifyingAttribute(GetPrimaryAttribute((int)AttributeName.Agility), .25f));
		
		GetSkills((int)SkillName.ShortSword).AddModifier(new ModifyingAttribute(GetPrimaryAttribute((int)AttributeName.Strength), .50f));
		
		GetSkills((int)SkillName.LongSword).AddModifier(new ModifyingAttribute(GetPrimaryAttribute((int)AttributeName.Strength), .50f));
		
		GetSkills((int)SkillName.Mace).AddModifier(new ModifyingAttribute(GetPrimaryAttribute((int)AttributeName.Strength), .50f));
		
		GetSkills((int)SkillName.Flail).AddModifier(new ModifyingAttribute(GetPrimaryAttribute((int)AttributeName.Strength), .50f));
		
		//Armor skills
		GetSkills((int)SkillName.LightArmor).AddModifier(new ModifyingAttribute(GetPrimaryAttribute((int)AttributeName.Strength), .50f));
		
		GetSkills((int)SkillName.MediumArmor).AddModifier(new ModifyingAttribute(GetPrimaryAttribute((int)AttributeName.Strength), .50f));
		
		GetSkills((int)SkillName.HeavyArmor).AddModifier(new ModifyingAttribute(GetPrimaryAttribute((int)AttributeName.Strength), .50f));
		
		//Crafting skills
		GetSkills((int)SkillName.Forgeing).AddModifier(new ModifyingAttribute(GetPrimaryAttribute((int)AttributeName.Strength), .50f));
		
		GetSkills((int)SkillName.WeaponSmith).AddModifier(new ModifyingAttribute(GetPrimaryAttribute((int)AttributeName.Strength), .50f));
		
		GetSkills((int)SkillName.ArmorSmith).AddModifier(new ModifyingAttribute(GetPrimaryAttribute((int)AttributeName.Strength), .50f));
		
		//Thief skills
		GetSkills((int)SkillName.Sneaking).AddModifier(new ModifyingAttribute(GetPrimaryAttribute((int)AttributeName.Strength), .50f));
		
		GetSkills((int)SkillName.Lockpicking).AddModifier(new ModifyingAttribute(GetPrimaryAttribute((int)AttributeName.Strength), .50f));
		
		//Social skills
		GetSkills((int)SkillName.Barter).AddModifier(new ModifyingAttribute(GetPrimaryAttribute((int)AttributeName.Strength), .50f));
		
		GetSkills((int)SkillName.Speech).AddModifier(new ModifyingAttribute(GetPrimaryAttribute((int)AttributeName.Willpower), .50f));
		
		//Bow skills
		GetSkills((int)SkillName.Bow).AddModifier(new ModifyingAttribute(GetPrimaryAttribute((int)AttributeName.Agility), .50f));
		GetSkills((int)SkillName.Bow).AddModifier(new ModifyingAttribute(GetPrimaryAttribute((int)AttributeName.Strength), .50f));
		
		GetSkills((int)SkillName.Crossbow).AddModifier(new ModifyingAttribute(GetPrimaryAttribute((int)AttributeName.Agility), .25f));
		GetSkills((int)SkillName.Crossbow).AddModifier(new ModifyingAttribute(GetPrimaryAttribute((int)AttributeName.Strength), .25f));
		GetSkills((int)SkillName.Crossbow).AddModifier(new ModifyingAttribute(GetPrimaryAttribute((int)AttributeName.Concentration), .10f));
		
		//Magic skills
		GetSkills((int)SkillName.MagicAttunement).AddModifier(new ModifyingAttribute(GetPrimaryAttribute((int)AttributeName.Willpower), .10f));
		GetSkills((int)SkillName.MagicAttunement).AddModifier(new ModifyingAttribute(GetPrimaryAttribute((int)AttributeName.Concentration), .20f));
		
		GetSkills((int)SkillName.WindMagic).AddModifier(new ModifyingAttribute(GetPrimaryAttribute((int)AttributeName.Willpower), .05f));
		
		GetSkills((int)SkillName.FireMagic).AddModifier(new ModifyingAttribute(GetPrimaryAttribute((int)AttributeName.Willpower), .05f));
		
		GetSkills((int)SkillName.IceMagic).AddModifier(new ModifyingAttribute(GetPrimaryAttribute((int)AttributeName.Willpower), .05f));
		
		GetSkills((int)SkillName.EarthMagic).AddModifier(new ModifyingAttribute(GetPrimaryAttribute((int)AttributeName.Willpower), .05f));
		
		GetSkills((int)SkillName.ArcaneMagic).AddModifier(new ModifyingAttribute(GetPrimaryAttribute((int)AttributeName.Willpower), .05f));
		
		GetSkills((int)SkillName.SummoningMagic).AddModifier(new ModifyingAttribute(GetPrimaryAttribute((int)AttributeName.Willpower), .05f));
		
		GetSkills((int)SkillName.IllusionMagic).AddModifier(new ModifyingAttribute(GetPrimaryAttribute((int)AttributeName.Willpower), .05f));
		
	}
	
	public void StatUpdate()
	{
		for(int i = 0; i < _vital.Length; i++)
			_vital[i].Update();
		
		for(int i = 0; i < _skills.Length; i++)
			_skills[i].Update();
	}
}
