using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vault : MonoBehaviour
{
	//Delegate
	public delegate void OnVaultItemChanged();
	public OnVaultItemChanged OnVaultItemChangedCallback;

	//Values
	public int space = 16;
	public List<Item> items = new List<Item>();
	

	public static Vault instance;
	void Awake(){
		if(instance != null){
			Debug.LogWarning("More than one instace of Vault found!");
			return;
		}
		instance = this;
	}


	//Methods
	public bool Add (Item item){
		if(items.Count < space){
			items.Add(item);
			if(OnVaultItemChangedCallback != null){
				OnVaultItemChangedCallback.Invoke();	
			}
			return true;
		}
		else{
			Debug.Log("Not Enough Space");
			return false;
		}
		
	}

	public void Remove (Item item){
		items.Remove(item);
		if(OnVaultItemChangedCallback != null){
			OnVaultItemChangedCallback.Invoke();	
		}
	}
}