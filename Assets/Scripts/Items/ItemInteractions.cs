using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemInteractions : MonoBehaviour
{
	//Values
	public Item item;

	//Methods
	public void PickUp(){
		Debug.Log("Picking up "+item.name);
		bool picked = Inventory.instance.Add(item);
		if(picked){
			Debug.Log(item.name+" Picked Up.");
		}
	}
}
