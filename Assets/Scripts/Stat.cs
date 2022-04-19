using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Stat
{
	//Values
	[SerializeField]
	private int baseValue;
	private List<int> modifiers = new List<int>();

	//Methods
	public int GetValue(){
		int finalValue = baseValue;
		modifiers.ForEach(val => finalValue += val);
		return finalValue;
	}

	public void AddModifier (int modifier){
		if (modifier != 0){
			modifiers.Add(modifier);
		}
	}
	public void RemoveModifier (int modifier){
		if (modifier != 0){
			modifiers.Remove(modifier);
		}
	}
}
    
