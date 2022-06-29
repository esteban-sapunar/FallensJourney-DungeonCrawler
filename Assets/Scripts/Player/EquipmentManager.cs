using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipmentManager : MonoBehaviour
{
	//Singleton
	public static EquipmentManager instance;

	void Awake(){
		instance = this;
	}

	//Valuesf
	public Equipment[] currentEquipment;

	//Delegate
	public delegate void OnEquipChanged(Equipment newItem,Equipment oldItem);
	public OnEquipChanged OnEquipChangedCallback;


	//Basic Functions
	public void Init(){
		int numSlots = System.Enum.GetNames(typeof(EquipmentSlot)).Length;
		currentEquipment = new Equipment[numSlots];
	}

	//Methods
	public void Equip (Equipment newItem){
		int slotIndex = (int)newItem.equipSlot;
		Equipment oldItem = null;
		if(currentEquipment[slotIndex] != null){
			oldItem =currentEquipment[slotIndex];
			Inventory.instance.Add(currentEquipment[slotIndex]);
		}
		currentEquipment[slotIndex] = newItem;
		if(OnEquipChangedCallback != null){
			OnEquipChangedCallback.Invoke(newItem,oldItem);	
		}
	}
	public void Unequip (int slotIndex){
		if(currentEquipment[slotIndex] != null){
			Equipment oldItem = currentEquipment[slotIndex];
			Inventory.instance.Add(oldItem);
			currentEquipment[slotIndex] = null;
			if(OnEquipChangedCallback != null){
				OnEquipChangedCallback.Invoke(null,oldItem);	
			}
		}
	}
}
