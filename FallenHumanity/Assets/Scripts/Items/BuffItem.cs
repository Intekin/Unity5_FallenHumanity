using UnityEngine;
using System;
using System.Collections;

public class BuffItem : Item {
	private int[] _buffMod;
	private BaseStat[] _stat;
	
	private Hashtable _buffs;
	
	public BuffItem()
	{
		_buffs = new Hashtable();
	}
	
	public BuffItem(Hashtable ht)
	{
		_buffs = ht;
	}
	
	public void AddBuff(BaseStat stat, int mod)
	{
		try{
			_buffs.Add(stat.Name, mod);
		}
		catch(Exception e)
		{
			Debug.LogWarning(e.ToString());	
		}
	}
	
	public void RemoveBuff(BaseStat stat)
	{
		_buffs.Remove(stat.Name);
	}
	
	public void BuffCount()
	{
		
	}
	
	public Hashtable GetBuffs()
	{
		return _buffs;
	}
}
