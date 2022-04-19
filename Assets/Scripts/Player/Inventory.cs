using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
	#region Singleton
	public static Inventory instance;
	void Awake(){
		if(instance != null){
			Debug.LogWarning("More than one instace of Inventory found!");
			return;
		}
		instance = this;
	}
	#endregion
	//Delegate
	public delegate void OnItemChanged();
	public OnItemChanged OnItemChangedCallback;

	//Values
	public int space = 20;
	public List<Item> items = new List<Item>();

	//Methods
	public bool Add (Item item){
		if(items.Count < space){
			items.Add(item);
			if(OnItemChangedCallback != null){
				OnItemChangedCallback.Invoke();	
			}
			return true;
		}
		else{
			Debug.Log("Not Enough Space");
			return false;
		}
		
	}

	public void Remove (Item item){
		if(OnItemChangedCallback != null){
			OnItemChangedCallback.Invoke();	
		}
		items.Remove(item);
	}
}
