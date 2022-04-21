using UnityEngine;
using UnityEngine.UI;

public class EquipSlot : MonoBehaviour
{
	//Values
    public Equipment item;
    public Image icon;

    //Methods
    public void AddEquipment(Equipment newItem){
    	item = newItem;
    	icon.sprite = item.icon;
    	icon.enabled = true;
    }
    public void ClearEquipment(){
    	item = null;
    	icon.sprite = null;
    	icon.enabled = false;
    }
    public void OnRemoveEquipment(){
    	EquipmentManager.instance.Unequip((int)item.equipSlot);
    }
    public void UseEquipment(){
    	if(item != null){
    		item.Use();
    	}
    }
    public void OpenEquipment(){
    	if(item != null){
    		Debug.Log("Open "+item.name);
    	}
    }
}
