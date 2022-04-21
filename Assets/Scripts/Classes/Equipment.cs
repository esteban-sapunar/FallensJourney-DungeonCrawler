using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Equipment", menuName ="Inventory/Equipment")]
public class Equipment : Item
{
	//Values
	public int attackModifier;
	public int armorModifier;
	public int defenseModifier;
	public int weightModifier;

	public EquipmentSlot equipSlot;

	//Methods
	public override void Use(){
		Debug.Log("Equiping "+name);
		EquipmentManager.instance.Equip(this);
		RemoveFromInventory();
	}

}
public enum EquipmentSlot {Head,Chest,Legs,Feet,Weapon,Shield}
