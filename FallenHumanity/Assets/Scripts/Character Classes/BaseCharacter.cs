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
		GetVital((int)VitalName.Health).AddModifier(new ModifyingAttribute(GetPrimaryAttribute((int)AttributeName.Strength), 1f));
        GetVital((int)VitalName.Health).AddModifier(new ModifyingAttribute(GetPrimaryAttribute((int)AttributeName.Defense), 2f));

        //mana
        GetVital((int)VitalName.Mana).AddModifier(new ModifyingAttribute(GetPrimaryAttribute((int)AttributeName.Magic), 1f));
        GetVital((int)VitalName.Mana).AddModifier(new ModifyingAttribute(GetPrimaryAttribute((int)AttributeName.MagicDefense), 1f));
    }
	
	private void SetupSkillModifiers()
	{
		//Weapon skills
		GetSkills((int)SkillName.MeleeWeapon).AddModifier(new ModifyingAttribute(GetPrimaryAttribute((int)AttributeName.Strength), 1.5f));
		GetSkills((int)SkillName.RangedWeapon).AddModifier(new ModifyingAttribute(GetPrimaryAttribute((int)AttributeName.Agility), 1.5f));
				
		//Crafting skills
		GetSkills((int)SkillName.WeaponCrafting).AddModifier(new ModifyingAttribute(GetPrimaryAttribute((int)AttributeName.Strength), .50f));		
		GetSkills((int)SkillName.ArmorCrafting).AddModifier(new ModifyingAttribute(GetPrimaryAttribute((int)AttributeName.Strength), .50f));	
		GetSkills((int)SkillName.ItemCrafting).AddModifier(new ModifyingAttribute(GetPrimaryAttribute((int)AttributeName.Strength), .50f));
        GetSkills((int)SkillName.Alchemy).AddModifier(new ModifyingAttribute(GetPrimaryAttribute((int)AttributeName.Magic), .50f));		

		//Magic skills
		GetSkills((int)SkillName.WindMagicResist).AddModifier(new ModifyingAttribute(GetPrimaryAttribute((int)AttributeName.MagicDefense), .05f));
		
		GetSkills((int)SkillName.FireMagicResist).AddModifier(new ModifyingAttribute(GetPrimaryAttribute((int)AttributeName.MagicDefense), .05f));
		
		GetSkills((int)SkillName.IceMagicResist).AddModifier(new ModifyingAttribute(GetPrimaryAttribute((int)AttributeName.MagicDefense), .05f));
		
		GetSkills((int)SkillName.EarthMagicResist).AddModifier(new ModifyingAttribute(GetPrimaryAttribute((int)AttributeName.MagicDefense), .05f));
		
		GetSkills((int)SkillName.LightningMagicResist).AddModifier(new ModifyingAttribute(GetPrimaryAttribute((int)AttributeName.MagicDefense), .05f));
		
	}
	
	public void StatUpdate()
	{
		for(int i = 0; i < _vital.Length; i++)
			_vital[i].Update();
		
		for(int i = 0; i < _skills.Length; i++)
			_skills[i].Update();
	}
}
