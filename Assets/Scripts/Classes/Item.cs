using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName ="Inventory/Item")]
public class Item : ScriptableObject
{
	//Values
	new public string name = "New Item";
	public Sprite icon = null;
	public string description;
	public int value = 0;
	public int tier = 0;
	public int healValue = 0;

	//Methods
	public virtual void Use(){
		if(healValue > 0){
			PlayerStats.instance.Heal(healValue);
			RemoveFromInventory();
		}	
	}
	public void RemoveFromInventory(){
		Inventory.instance.Remove(this);
	}
	public virtual void UnEquip(){
		//just for equipment functions
	}

}
